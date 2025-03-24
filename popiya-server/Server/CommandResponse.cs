namespace Server
{
	public enum ResponseStatus
	{
		Success,
		Failure
	}
	
	public struct CommandResponse
	{
		public ResponseStatus Status { get; }
		public GameMessage Message { get; }

		public CommandResponse(ResponseStatus status, GameMessage message)
		{
			Status = status;
			Message = message;
		}
	}	
}


