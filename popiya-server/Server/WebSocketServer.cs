using System.Net;
using System.Net.WebSockets;
using System.Text;
using Game.GameCommandResponseData;
using Newtonsoft.Json;

namespace Server
{
    public class WebSocketServer
    {
        private GameSessionManager _gameManager = new ();

        public async Task Start()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5000/");
            listener.Start();
            Console.WriteLine("WebSocket Server started on ws://localhost:5000/");

            while (true)
            {
                var context = await listener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    var wsContext = await context.AcceptWebSocketAsync(null);
                    var webSocket = wsContext.WebSocket;
                    _ = HandleClient(webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }
        }

        private async Task HandleClient(WebSocket socket)
        {
            byte[] buffer = new byte[1024];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    return;
                }

                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                await ProcessMessage(message, socket);
            }
        }

        private async Task ProcessMessage(string jsonMessage, WebSocket senderSocket)
        {
            var receivedMessage = JsonConvert.DeserializeObject<GameMessage>(jsonMessage);
            string playerId = receivedMessage.PlayerId;

            switch (receivedMessage.Type)
            {
                case MessageType.Create:
                    _gameManager.AddPlayer(playerId, senderSocket);
                    string sessionId = _gameManager.CreateGameSession(playerId);

                    var response = new CommandResponse(ResponseStatus.Success, new GameMessage(
                        type: MessageType.Create,
                        playerId: playerId,
                        data: JsonConvert.SerializeObject(new CreateSessionResponseData(sessionId))
                    ));

                    await SendMessageToClient(response, senderSocket);
                    break;

                case MessageType.Connect:
                    bool joined = _gameManager.JoinGameSession(playerId, receivedMessage.Data);
                    var connectResponse = new CommandResponse(
                        joined ? ResponseStatus.Success : ResponseStatus.Failure,
                        new GameMessage(
                            type: MessageType.Connect,
                            playerId: playerId,
                            data: receivedMessage.Data
                        )
                    );
                    await SendMessageToClient(connectResponse, senderSocket);
                    break;

                case MessageType.Move:
                    bool moveSuccess = _gameManager.ProcessMove(playerId, receivedMessage.Data);
                    var moveResponse = new CommandResponse(
                        moveSuccess ? ResponseStatus.Success : ResponseStatus.Failure,
                        receivedMessage
                    );
                    await SendMessageToClient(moveResponse, senderSocket);
                    break;
            }
        }

        private async Task SendMessageToClient(object message, WebSocket socket)
        {
            string json = JsonConvert.SerializeObject(message);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
