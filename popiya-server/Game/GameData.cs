using System.Collections.Generic;

namespace Game
{
	public class GameData
	{
		public Tile[,] Map { get; private set; }
		public List<Unit> Units { get; private set; }

		public GameData()
		{
			Map = new Tile[10, 10];
			Units = new List<Unit>();
			Initialize();
		}
	
		public Tile GetTileByCoordinates(TileCoordinates tileCoordinates) => Map[tileCoordinates.X, tileCoordinates.Y];

		private void Initialize()
		{
			for (int x = 0; x < 10; x++)
			{
				for (int y = 0; y < 10; y++)
				{
					Map[x, y] = new Tile(new TileCoordinates(x, y));
				}
			}
        
			// Hardcoding a unit at (0,0)
			Unit unit = new Unit(Map[0, 0]);
			Map[0, 0].Unit = unit;
			Units.Add(unit);
		}
	}
}

