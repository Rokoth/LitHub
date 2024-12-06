using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitHubClient
{
    public class Account
    {
        public string Server { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string LocalDir { get; set; }
        public string Port { get; internal set; }
        public string Name { get; internal set; }
    }
}
