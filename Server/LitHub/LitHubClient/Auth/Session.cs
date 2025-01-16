using System.Collections.Generic;
using Unity;

namespace LitHubClient.Auth
{
    public sealed class Session : ISession
    {
        private readonly IUnityContainer container;
        private SessionVariables _session;
        //private const string settingsFile = "settings.dat";

        public Session(IUnityContainer container)
        {
            this.container = container;
            CreateSessionVariables();
        }

        private void CreateSessionVariables()
        {
            var config = container.Resolve<IAppConfigManager>();
            _session = new SessionVariables()
            {
                UseLocalDatabase = config.UseLocalDatabase,
                UseLocalRepo = config.UseLocalRepo,
                UseRemoteRepo = config.UseRemoteRepo,
                
            };
            //if (File.Exists(settingsFile))
            //{
            //    using (var reader = new StreamReader(settingsFile))
            //    {
            //        _session = JObject.Parse(reader.ReadToEnd()).ToObject<SessionVariables>();
            //    }
            //}
            //else
            //{
            //    _session = new SessionVariables();
            //    using (var writer = new StreamWriter(settingsFile, false))
            //    {
            //        writer.Write(JObject.FromObject(_session).ToString());
            //    }
            //}
        }

        public bool UseLocalDataBase
        {
            get
            {
                return _session.UseLocalDatabase;
            }
        }

        public string LocalDataBasePassword
        {
            get
            {
                return _session.SqliteSession.Password;
            }
        }

        string ISession.LocalDataBasePassword { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        bool ISession.UseLocalDataBase { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string LocalDataBaseFile { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string ServerUrl { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string ServerPassword { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string ServerLogin { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        private class SessionVariables
        {
            public bool UseLocalDatabase { get; set; } = true;
            public bool UseLocalRepo { get; set; }
            public bool UseRemoteRepo { get; set; }
            public SqliteSession SqliteSession { get; set; } = new SqliteSession();
            public List<RemoteSession> RemoteSessions { get; set; } = new List<RemoteSession>();
        }

        private class SqliteSession : SessionBase
        {
            public string File { get; set; }
            public string Password { get; set; }
        }

        private class RemoteSession : SessionBase
        {
            public string Url { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
        }

        private class SessionBase
        {
            public bool IsAuth { get; set; }
            public List<string> RepoList { get; set; }
        }
    }
}
