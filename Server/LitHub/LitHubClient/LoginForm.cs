//using LitHubClient.Auth;
using LitHubClient.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace LitHubClient
{
    public partial class LoginForm : Form
    {
        public string Login { get { return LoginTextBox.Text; } }
        public string Password { get { return ServerPasswordTextBox.Text; } }
        public string Server { get { return ServerTextBox.Text; } }

        private readonly List<Control> sortElements;

        private readonly ISession  _session;
        private readonly IUnityContainer _container;

        private enum ShowMode
        {
            Sqlite, Websocket
        }

        public LoginForm(ISession session, IUnityContainer container)
        {
            InitializeComponent();
            _container = container;
            _session = session;
            sortElements = new List<Control>() {
                Headerlabel,
                LocalPasswordLabel,
                UseLocalBaseCheckBox,
                ServerLabel,
                LoginLabel,
                ServerPasswordLabel };
            UseLocalBaseCheckBox.Checked = _session.UseLocalDataBase;
            LocalPasswordTextBox.Text = _session.LocalDataBasePassword;
        }

        private void SQLiteConnect()
        {
            if (_session.UseLocalDataBase && !SQLiteConnector.Inited)
            {
                SQLiteConnector.Init(_session.LocalDataBaseFile , _session.LocalDataBasePassword);
                if (!SQLiteConnector.Inited)
                {
                    ShowForm();
                }
                else
                {
                    WebSocketConnectAuto();
                }
            }
            else
            {
                WebSocketConnectAuto();
            }
        }

        private void WebSocketConnectAuto()
        {
            ServerTextBox.Text = _session.ServerUrl;
            ServerPasswordTextBox.Text = _session.ServerPassword;
            LoginTextBox.Text = _session.ServerLogin;
            WebSocketConnect();
        }

        private void WebSocketConnect()
        {
            Hide();
            if (_session.UseLocalDataBase)
            {
                if (SQLiteConnector.Inited)
                {
                    InitWebSocket();
                }
                else
                {
                    MessageBox.Show("Ошибка - не инициирована локальная база");
                    ShowForm();
                }
            }
            else
            {
                InitWebSocket();
            }

        }

        private void InitWebSocket()
        {
            if (!WebSocketConnector.Inited)
            {
                WebSocketConnector.Init(_container);
                if (!WebSocketConnector.Inited)
                {
                    MessageBox.Show("Не удалось подключиться к серверу, проверьте учётные данные и наличие соединения с Интернет");
                    ShowForm();
                }
                else
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void ShowForm()
        {
            if (_session.UseLocalDataBase && !SQLiteConnector.Inited)
            {
                ShowElements(ShowMode.Sqlite);
            }
            else if (!WebSocketConnector.Inited)
            {
                ShowElements(ShowMode.Websocket);
            }
            else
            {
                MessageBox.Show("Подключение установлено");
                Close();
            }
        }

        private void ShowElements(ShowMode showMode)
        {
            bool showGroup1 = showMode.Equals(ShowMode.Sqlite);
            bool showGroup2 = showMode.Equals(ShowMode.Websocket);

            LocalPasswordLabel.Visible = showGroup1;
            LocalPasswordTextBox.Visible = showGroup1;
            UseLocalBaseCheckBox.Visible = showGroup1;

            ServerLabel.Visible = showGroup2;
            ServerTextBox.Visible = showGroup2;
            LoginLabel.Visible = showGroup2;
            LoginTextBox.Visible = showGroup2;
            ServerPasswordLabel.Visible = showGroup2;
            ServerPasswordTextBox.Visible = showGroup2;
            int top = 5;
            foreach (Control control in sortElements.Where(s => s.Visible))
            {
                control.Top = top;
                top = control.Bottom + 5;
            }
            LocalPasswordTextBox.Top = LocalPasswordLabel.Top;
            ServerTextBox.Top = ServerLabel.Top;
            LoginTextBox.Top = LoginLabel.Top;
            ServerPasswordTextBox.Top = ServerPasswordLabel.Top;
            ConnectButton.Top = top;
            CancelButton.Top = top;
            Height = ConnectButton.Bottom + 5;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (_session.UseLocalDataBase && !SQLiteConnector.Inited)
            {
                _session.LocalDataBasePassword = LocalPasswordTextBox.Text;
                SQLiteConnect();
            }
            else if (!WebSocketConnector.Inited)
            {
                _session.ServerUrl = ServerTextBox.Text;
                _session.ServerLogin = LoginTextBox.Text;
                _session.ServerPassword = ServerPasswordTextBox.Text;
                WebSocketConnect();
            }
            else
            {
                MessageBox.Show("Подключение установлено");
                Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UseLocalBaseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _session.UseLocalDataBase = UseLocalBaseCheckBox.Checked;
        }
    }
}