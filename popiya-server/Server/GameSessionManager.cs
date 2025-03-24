using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Game;
using Game.GameCommandData;
using Game.GameCommands;
using Newtonsoft.Json;

namespace Server
{
    public class GameSessionManager
    {
        private Dictionary<string, WebSocket> _players = new Dictionary<string, WebSocket>();
        private Dictionary<string, List<string>> _playerSessions = new Dictionary<string, List<string>>();
        private Dictionary<string, GameData> _gameSessions = new Dictionary<string, GameData>();

        public void AddPlayer(string playerId, WebSocket socket)
        {
            if (!_players.ContainsKey(playerId))
            {
                _players[playerId] = socket;
                _playerSessions[playerId] = new List<string>();
            }
        }

        public string CreateGameSession(string playerId)
        {
            string sessionId = Guid.NewGuid().ToString();
            _gameSessions[sessionId] = new GameData();
            _playerSessions[playerId].Add(sessionId);

            return sessionId;
        }

        public bool JoinGameSession(string playerId, string sessionId)
        {
            if (_gameSessions.ContainsKey(sessionId))
            {
                _playerSessions[playerId].Add(sessionId);
                return true;
            }
            return false;
        }

        public bool ProcessMove(string playerId, string jsonData)
        {
            var moveData = JsonConvert.DeserializeObject<MoveGameCommandData>(jsonData);
            string sessionId = moveData.SessionId;

            if (_gameSessions.TryGetValue(sessionId, out var gameData))
            {
                var moveCommand = new MoveGameCommand(moveData);
                return moveCommand.TryExecute(gameData);
            }
            return false;
        }

        public void RemovePlayer(string playerId)
        {
            _players.Remove(playerId);
            _playerSessions.Remove(playerId);
        }

        public WebSocket GetPlayerSocket(string playerId)
        {
            return _players.ContainsKey(playerId) ? _players[playerId] : null;
        }
    }
}
