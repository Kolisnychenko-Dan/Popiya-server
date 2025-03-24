namespace Game
{
	public class Tile
	{
		private readonly TileCoordinates _coordinates;
		private Unit _unitData = null;
	
		public TileCoordinates Coordinates { get; }
	
		public Unit Unit
		{
			get => _unitData;
			set => _unitData = value;
		}
	
		public Tile(TileCoordinates coordinates)
		{
			Coordinates = coordinates;
		}
	}	
}