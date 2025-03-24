namespace Game
{
	public class Unit
	{
		public Unit(Tile tile)
		{
			Tile = tile;
		}

		public Tile Tile { get; private set; }
		public int Hp { get; set; } = 10;
		public int MoveSpeed { get; } = 1;

		public void MoveTo(Tile newTile)
		{
			if (newTile.Unit == null)
			{
				Tile.Unit = null;
				newTile.Unit = this;
				Tile = newTile;
			}
		}
	}	
}