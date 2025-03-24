using System;
using Game.GameCommandData;

namespace Game.GameCommands
{
	public class MoveGameCommand : IGameCommand
	{
		private readonly GameCommandData.MoveGameCommandData _moveGameCommandData;

		public MoveGameCommand(GameCommandData.MoveGameCommandData moveGameCommandData)
		{
			_moveGameCommandData = moveGameCommandData;
		}

		public bool TryExecute(GameData gameData)
		{
			var from = gameData.GetTileByCoordinates(_moveGameCommandData.From);
			var to = gameData.GetTileByCoordinates(_moveGameCommandData.To);
		
			if (from.Unit != null && to.Unit == null)
			{
				to.Unit = from.Unit;
				from.Unit = null;
				return true;
			}
			else
			{
				Console.WriteLine($"Error: Moving from {from.Coordinates} to {to.Coordinates}");
				return false;
			}
		}
	}	
}