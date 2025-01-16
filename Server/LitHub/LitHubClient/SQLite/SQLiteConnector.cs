using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace LitHubClient
{
    public static class SQLiteConnector
    {
        private static string _dbFileName = "lithub.sqlite";
        private const string _dbDumpFileName = "lithub.dat";
        private static string dbFullPath;
        private static readonly string dbDumpFullPath = Path.Combine(Directory.GetCurrentDirectory(), _dbDumpFileName);

        private static readonly string dbDir = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), APP_FOLDER);

        private static SQLiteConnection connection;

        private const string APP_FOLDER = "LitHub";
        private const int MAX_TRIES = 3;

        public static bool Connected { get; private set; }
        public static bool Inited { get; private set; }

        public static void Init(string dbFileName, string password)
        {
            _dbFileName = dbFileName;

            dbFullPath = Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    APP_FOLDER), _dbFileName);

            if (!File.Exists(dbFullPath))
            {
                try
                {
                    CreateDataBase(password);
                    Inited = true;
                }
                catch
                {
                    Inited = false;
                }
            }
            else
            {
                Inited = ConnectDataBase(password);
            }
        }

        private static void CreateDataBase(string password)
        {
            if (!Directory.Exists(dbDir))
            {
                Directory.CreateDirectory(dbDir);
            }
            SQLiteConnection.CreateFile(dbFullPath);

            connection.Open();
            connection.ChangePassword(password);
            Connected = true;
            using (SQLiteCommand createCommand = new SQLiteCommand(connection))
            {
                createCommand.CommandText = SqlTextPrepare(string.Join(@"\r\n", File.ReadAllLines(dbDumpFullPath)),
                    new Dictionary<string, object>());
                createCommand.ExecuteNonQuery();
            }
        }

        public static void ExecuteQueryVoid(string text, Dictionary<string, object> parameters)
        {
            if (Connected)
            {
                using (var createCommand = new SQLiteCommand(SqlTextPrepare(text, parameters), connection))
                {
                    createCommand.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteQueryTable(string text, Dictionary<string, object> parameters)
        {
            DataTable ret = new DataTable();
            if (Connected)
            {
                using (var queryAdapter = new SQLiteDataAdapter(SqlTextPrepare(text, parameters), connection))
                {
                    queryAdapter.Fill(ret);
                }
            }
            return ret;
        }

        private static string SqlTextPrepare(string text, Dictionary<string, object> parameters)
        {
            var sqlQuery = text;
            foreach (var kvp in parameters)
            {
                string param;
                if (kvp.Value is Array)
                {
                    param = string.Join(", ", ((Array)kvp.Value).Cast<string>());
                }
                else
                {
                    param = kvp.Value.ToString();
                }
                sqlQuery.Replace(string.Format("{{0}}", kvp.Key), param);
            }
            return sqlQuery;
        }

        private static void Cancel()
        {
            if (connection?.State == ConnectionState.Open)
                connection.Close();
            Connected = false;
        }

        private static bool ConnectDataBase(string password)
        {
            var tries = 0;
            while (true)
            {
                if (tries < MAX_TRIES)
                {
                    connection = new SQLiteConnection("Data Source=" + dbFullPath + ";Version=3;");
                    try
                    {
                        connection.SetPassword(password);
                        connection.Open();
                        tries = 0;
                        Connected = true;
                        return true;
                    }
                    catch
                    {
                        tries++;
                        password = null;
                    }
                }
                else
                {
                    Cancel();
                    return false;
                }
            }
        }

        public static DataTable GetAccounts()
        {
            Query query = new Query(Query.QueryType.SELECT,
                "server",
                new Dictionary<string, string>() {
                    { "server_id", "ServerId" },{ "uri", "Server" },{ "port", "Port" },{ "name", "Name" },{ "login", "Login" },{ "password", "Password" }
                }, new WhereClause(WhereClause.LogicType.NONE));
            string queryText = QueryBuilder(query);
            return ExecuteQueryTable(queryText, new Dictionary<string, object>());
        }

        public static void AddAccount(string uri, string port, string name, string login, string password, string localDir)
        {
            Query query = new Query(Query.QueryType.INSERT,
                    "server",
                    new Dictionary<string, string>() {
                    { "uri", uri },{ "port", port },{ "name", name },{ "login", login },{ "password", password }
                    }, new WhereClause(WhereClause.LogicType.NONE));
            string queryText = QueryBuilder(query);
            ExecuteQueryVoid(queryText, new Dictionary<string, object>());
        }

        public static void UpdateAccount(string uri, string port, string name, string login, string password, string localDir)
        {
            Query query = new Query(Query.QueryType.UPDATE,
                    "server",
                    new Dictionary<string, string>() {
                    { "uri", uri },{ "port", port },{ "name", name },{ "login", login },{ "password", password }
                    }, new WhereClause(WhereClause.LogicType.NONE));
            string queryText = QueryBuilder(query);
            ExecuteQueryVoid(queryText, new Dictionary<string, object>());
        }

        public static string QueryBuilder(Query query)
        {
            switch (query.Querytype)
            {
                case Query.QueryType.INSERT:
                    return InsertQueryBuilder(query);
                case Query.QueryType.UPDATE:
                    return UpdateQueryBuilder(query);
                case Query.QueryType.SELECT:
                    return SelectQueryBuilder(query);
                case Query.QueryType.DELETE:
                    return DeleteQueryBuilder(query);
                default: return null;
            }
        }

        private static string SelectQueryBuilder(Query query)
        {
            return "SELECT " + string.Join(", ",
                   query.Fields.Select(s => "\"" + s.Key + "\" AS \"" + s.Value + "\"").ToArray()) +
                   "FROM \"" + query.Table + "\" " + query.Whereclause.Get() + ";";
        }

        private static string InsertQueryBuilder(Query query)
        {
            return "INSERT INTO \"" + query.Table + "\" (" +
                    string.Join(", ", query.Fields.Select(s => "\"" + s.Key + "\"").ToArray()) +
                   "VALUES (" +
                    string.Join(", ", query.Fields.Select(s => "\"" + s.Value + "\"").ToArray()) +
                    ");";
        }

        private static string UpdateQueryBuilder(Query query)
        {
            return "UPDATE \"" + query.Table + "\" SET " + string.Join(", ",
                   query.Fields.Select(s => "\"" + s.Key + "\" = \"" + s.Value + "\"").ToArray()) +
                   "\" " + query.Whereclause.Get() + ";";
        }

        private static string DeleteQueryBuilder(Query query)
        {
            return "DELETE " + "FROM \"" + query.Table + "\" " + query.Whereclause.Get() + ";";
        }

        public static Account GetAccount()
        {
            Account account = new Account();


            return account;
        }
    }
}
