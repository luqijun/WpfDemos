using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingWebsocket
{
    internal static class LogHelper
    {
        public static Action<string> _logHandler;

        public static void SetLogHandler(Action<string> handler)
        {
            _logHandler = handler;
        }

        public static void Info(string message)
        {
            _logHandler?.Invoke(message);
        }
    }
}
