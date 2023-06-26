using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingWebsocket
{
    public interface ISocketClient
    {
        void Start();

        void SendMessage(string message);
    }
}
