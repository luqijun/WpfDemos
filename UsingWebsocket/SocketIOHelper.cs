using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UsingWebsocket
{
    public class SocketIOClient : ISocketClient
    {
        private SocketIO client;

        private string url;

        public SocketIOClient(string url)
        {
            this.url = url;
        }

        public async void Start()
        {

            client = new SocketIO(this.url);


            client.On("receive_message", response =>
            {
                // You can print the returned data first to decide what to do next.
                // output: ["ok",{"id":1,"name":"tom"}]

                var decodedString = Regex.Unescape(response?.ToString());
                LogHelper.Info(decodedString);

                // Get the first data in the response
                //string text = response.GetValue<string>();
                // Get the second data in the response
                //var dto = response.GetValue<TestDTO>(1);

                // The socket.io server code looks like this:
                // socket.emit('hi', 'ok', { id: 1, name: 'tom'});
            });

            client.On("artwork_update", response =>
            {
                var decodedString = Regex.Unescape(response?.ToString());
                LogHelper.Info(decodedString);
            });


            client.OnConnected += async (sender, e) =>
            {
                LogHelper.Info("connected!!!");
                // Emit a string
                await client.EmitAsync("hi", "socket.io");

                // Emit a string and an object
                var dto = new { Id = 123, Name = "bob" };
                await client.EmitAsync("register", "source", dto);
            };

            client.OnDisconnected += async (sender, e) =>
            {
                LogHelper.Info("disconnected!!!");
            };
            await client.ConnectAsync();

        }


        public async void SendMessage(string message)
        {
            await client.EmitAsync("send_message", "source", message);
        }
    }



    //public class SocketIOClient : ISocketClient
    //{
    //    private Socket _socket;

    //    private string url;

    //    public SocketIOClient(string url)
    //    {
    //        this.url = url;
    //    }

    //    public void Start()
    //    {

    //        var options = new IO.Options() { IgnoreServerCertificateValidation = true, AutoConnect = true, ForceNew = true,  QueryString = "EIO=3" };
    //        //options.Transports = System.Collections.Immutable.ImmutableList.Create("websocket");
    //        _socket = IO.Socket(this.url);

    //        _socket.On(Socket.EVENT_CONNECT, () =>
    //        {
    //            LogHelper.Info("Connected!");
    //        });

    //        _socket.On("message", (data) =>
    //        {
    //            LogHelper.Info(data?.ToString());
    //        });

    //        _socket.On(Socket.EVENT_DISCONNECT, () =>
    //        {
    //            LogHelper.Info("Disconnected!");
    //        });

    //        _socket.On(Socket.EVENT_ERROR, () =>
    //        {
    //            LogHelper.Info("Error!");
    //        });

    //    }


    //    public void SendMessage(string message)
    //    {
    //        _socket.Send(message);
    //    }
    //}
}
