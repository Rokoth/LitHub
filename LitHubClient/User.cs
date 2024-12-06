using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitHubClient
{
    public static class User
    {
        public static string Name { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static bool Auth { get; set; } = false;
    }
}
