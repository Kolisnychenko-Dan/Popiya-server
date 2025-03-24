namespace Game.GameCommandData
{
	public class MoveGameCommandData : ASessionCommand
	{
		public TileCoordinates From { get; }
		public TileCoordinates To { get; }

		public MoveGameCommandData(TileCoordinates from, TileCoordinates to)
		{
			From = from;
			To = to;
		}
	}	
}