using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitHubClient.Events
{
    public class WErrorEventArgs
    {
        public enum ErrorType {
            Init, Close, Other 
        }

        public string ErrorText { get; set; }
        public ErrorType Error { get; set; }
    }
}
