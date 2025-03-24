namespace Game
{
	public readonly struct TileCoordinates
	{
		public TileCoordinates(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; }
		public int Y { get; }
	}	
}