using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace UsingWebsocket
{
    public class WebSocketClient: ISocketClient //: MonoBehaviour
    {
        private WebSocket ws;

        private string url;

        public WebSocketState State => ws.ReadyState;


        public WebSocketClient(string url)
        {
            this.url = url;
        }

        public void Start()
        {
            // 连接 WebSocket 服务器
            ws = new WebSocket(this.url);
            ws.OnOpen += OnOpen;
            ws.OnMessage += OnMessage;
            ws.OnClose += OnClose;
            ws.Connect();
        }

        void OnDestroy()
        {
            // 断开 WebSocket 连接
            ws.Close();
            ws = null;
        }

        private void OnOpen(object sender, System.EventArgs e)
        {
            LogHelper.Info("WebSocket connected");
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            LogHelper.Info("WebSocket message received: " + e.Data);
        }

        private void OnClose(object sender, CloseEventArgs e)
        {
            LogHelper.Info("WebSocket disconnected");
        }

        public void SendMessage(string message)
        {
            // 发送消息到 WebSocket 服务器
            ws.Send(message);
        }
    }
}
