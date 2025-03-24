namespace Server
{
	public struct GameMessage
	{
		public MessageType Type { get; }
		public string PlayerId { get; }
		public string Data { get; }
		
		public GameMessage(MessageType type, string playerId, string data)
		{
			Type = type;
			PlayerId = playerId;
			Data = data;
		}
	}
}