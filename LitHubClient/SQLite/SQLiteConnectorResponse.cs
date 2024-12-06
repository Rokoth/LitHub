using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitHubClient.SQLite
{
    public class SQLiteConnectorResponse
    {
        public enum Response
        {
            OK,
            Error, 
            AuthRequired
        }
    }
}
