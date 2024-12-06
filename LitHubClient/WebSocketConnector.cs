using LitHubClient.Auth;
using LitHubClient.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;
using System;
using System.Threading;
using Unity;
using WebSocket4Net;

namespace LitHubClient
{
    public static class WebSocketConnector
    {
        public enum Types
        {
            list, 
            echo,
            auth,
            getuser
        }

        private static bool connected;

        private static IUnityContainer _container;

        public static bool Inited { get; private set; }
        private static WebSocket websocket;
        private static Account account;

        public delegate void OnOpenHandler();
        public delegate void OnAuthHandler(bool auth);
        public delegate void OnListHandler(ListEventArgs e);
        public delegate void OnGetUserHandler(ListEventArgs e);
        public delegate void OnEchoHandler(EchoEventArgs e);
        public delegate void OnErrorHandler(WErrorEventArgs e);
        public delegate void OnCancelHandler();

        public static event OnOpenHandler OnOpen;
        public static event OnAuthHandler OnAuth;
        public static event OnListHandler OnGetList;
        public static event OnGetUserHandler OnGetUser;
        public static event OnEchoHandler OnEcho;
        public static event OnErrorHandler OnError;
        public static event OnCancelHandler OnCancel;

        private static AutoResetEvent autoEvent;
        private static AutoResetEvent authEvent;

        private static bool needAppClose;

        public static void Init(IUnityContainer container)
        {
            _container = container;
            account = new Account();
            if (SQLiteConnector.Inited)
            {
                account = SQLiteConnector.GetAccount();
            }
            else
            {
                account = GetAccountFromLoginForm(false);
            }
            if (!needAppClose) Connect();            
            if (connected) Auth();
        }

        private static void Auth()
        {
            authEvent = new AutoResetEvent(false);
            JObject o = new JObject
            {
                ["login"] = account.Login,
                ["password"] = account.Password
            };
            Send(o.ToString(), Types.auth);
            authEvent.WaitOne();
            if (!Inited)
            {
                string oldServer = account.Server;
                account = GetAccountFromLoginForm(true);
                if (!needAppClose)
                {
                    if(!oldServer.Equals(account.Server)) Connect();
                    Auth();
                }
            }
        }

        private static void Connect()
        {
            try
            {
                if (websocket != null && websocket.State == WebSocketState.Open)
                    websocket.Close();
                connected = false;
                websocket = new WebSocket(account.Server);
                websocket.Opened += Websocket_Opened;
                websocket.Error += Websocket_Error;
                websocket.Closed += Websocket_Closed;
                websocket.MessageReceived += Websocket_MessageReceived;
                websocket.Open();

                autoEvent = new AutoResetEvent(false);
                autoEvent.WaitOne(60000);
                if (!connected)
                {
                    Reconnect();
                }                
            }
            catch
            {
                Reconnect();
            }
        }

        private static void Reconnect()
        {            
            account = GetAccountFromLoginForm(true);
            if (!needAppClose)
            {
                Connect();
            }
        }

        private static void Websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            RequestHandler(e.Message);
        }

        private static Account GetAccountFromLoginForm(bool reconnect)
        {
            Account account = new Account();
            var loginform = _container.Resolve<LoginForm>();
            var result = loginform.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel || result == System.Windows.Forms.DialogResult.None)
            {
                needAppClose = true;
                OnCancel();
            }
            return account;
        }

        private static void RequestHandler(string str)
        {
            JObject o = JObject.Parse(str);
            if(!Enum.TryParse(o["type"].ToString(), out Types types))
            {
                //todo: log
            }
            
            switch(types)
            {
                case Types.list:
                    OnGetList(new ListEventArgs() { Data = o });
                    break;
                case Types.auth:
                    var auth = bool.Parse(o["auth"].ToString());
                    if (auth)
                    {
                        User.Auth = true;
                        User.Name = o["name"].ToString();
                        Inited = true;
                    }
                    authEvent.Set();
                    break;
                case Types.echo:
                    OnEcho(new EchoEventArgs() { Text = str });
                    break;

                case Types.getuser:
                    OnGetUser(new ListEventArgs() { Data = o });
                    break;
            }
        }

        private static void Websocket_Closed(object sender, EventArgs e)
        {
            connected = false;
            Connect();
        }

        private static void Websocket_Error(object sender, ErrorEventArgs e)
        {
            connected = false;
            Connect();
        }

        private static void Websocket_Opened(object sender, EventArgs e)
        {            
            connected = true;
            autoEvent.Set();
        }

        public static void Send(string text, Types type)
        {
            JObject o = new JObject
            {
                { "type", type.ToString() },
                { "text", text }
            };
            websocket.Send(o.ToString());
        }

        public static void Send<T>(Types type, T value)
        {
            var sendObject = new SendObject<T>()
            {
                Value = value,
                Type = type.ToString()
            };
            var json = JsonConvert.SerializeObject(sendObject);
            websocket.Send(json);
        }

        public static void GetUser(Guid id) => Send(Types.getuser, id);
    }

    public class SendObject<T>
    {
        public string Type { get; set; }
        public T Value { get; set; }
    }
}
