using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MS.Services
{
    public static class WebSocketMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketServerMiddleware>();
        }
    }

    public class WebSocketServerMiddleware
    {
        private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        private readonly RequestDelegate _next;

        public WebSocketServerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    string clientId = Guid.NewGuid().ToString();
                    _sockets.TryAdd(clientId, webSocket);

                    await ReceiveMessage(webSocket, clientId, async (result, buffer) =>
                    {
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            Console.WriteLine($"Received from {clientId}: {Encoding.UTF8.GetString(buffer)}");
                            // 这里可以根据实际情况处理消息，以下是回显消息
                            await webSocket.SendAsync(buffer, result.MessageType, result.EndOfMessage, CancellationToken.None);
                            return;
                        }
                        else if (result.MessageType == WebSocketMessageType.Close)
                        {
                            Console.WriteLine("Received close message");
                            _sockets.TryRemove(clientId, out WebSocket _);
                            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by server", CancellationToken.None);
                        }
                    });
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task ReceiveMessage(WebSocket socket, string clientId, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                handleMessage(result, buffer);
            }
        }

        public static async Task SendMessageToAllAsync(string message)
        {
            foreach (var pair in _sockets)
            {
                if (pair.Value.State == WebSocketState.Open)
                {
                    var buffer = Encoding.UTF8.GetBytes(message);
                    await pair.Value.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
