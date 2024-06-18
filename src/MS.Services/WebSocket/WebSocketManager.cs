using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MS.Services
{
    public class WebSocketManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public async Task HandleWebSocketAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                string clientId = Guid.NewGuid().ToString(); // 或生成唯一标识符
                _sockets.TryAdd(clientId, webSocket);

                await ReceiveMessage(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine($"Message from Client: {Encoding.UTF8.GetString(buffer)}");
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        _sockets.TryRemove(clientId, out WebSocket ws);
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,
                                            "Connection closed by client", CancellationToken.None);
                    }
                });
            }
        }

        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                handleMessage(result, buffer);
            }
        }

        public async Task SendMessageAsync(string message)
        {
            foreach (var pair in _sockets)
            {
                WebSocket webSocket = pair.Value;
                if (webSocket.State == WebSocketState.Open)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
