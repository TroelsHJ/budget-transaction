using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Web;

namespace budget_transaction
{
    public class MyWebSocket
    {
        public MyWebSocket()
        {
            string url = string.Format("ws://{0}:{1}/socket.io/?EIO=3&transport=websocket", "localhost", 5000);
            //var ws = new WebSocketSharp.WebSocket(url);

            ClientWebSocket webSocketConnection = new ClientWebSocket();
            Uri p = new Uri("ws://broker-service.herokuapp.com/socket.io/?EIO=3&transport=websocket");
            webSocketConnection.ConnectAsync(p, CancellationToken.None);
            Thread.Sleep(5000);
        }
    }
}