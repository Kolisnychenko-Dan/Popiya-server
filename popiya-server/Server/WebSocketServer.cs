using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server;

public class WebSocketServer
{
	private static List<WebSocket> _clients = new();

    public static async Task Start()
    {
        HttpListener listener = new();
        listener.Prefixes.Add("http://localhost:5000/");
        listener.Start();
        Console.WriteLine("WebSocket Server started on ws://localhost:5000/");

        while (true)
        {
            HttpListenerContext context = await listener.GetContextAsync();
            if (context.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext wsContext = await context.AcceptWebSocketAsync(null);
                WebSocket webSocket = wsContext.WebSocket;
                _clients.Add(webSocket);
                _ = HandleClient(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Close();
            }
        }
    }

    private static async Task HandleClient(WebSocket socket)
    {
        byte[] buffer = new byte[1024];

        while (socket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                _clients.Remove(socket);
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                return;
            }
            string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Received: {message}");
            await BroadcastMessage(message);
        }
    }

    private static async Task BroadcastMessage(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        foreach (var client in _clients)
        {
            if (client.State == WebSocketState.Open)
            {
                await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}