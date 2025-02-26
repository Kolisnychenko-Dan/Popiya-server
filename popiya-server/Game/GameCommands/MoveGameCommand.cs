namespace Game.GameCommands;

public class MoveGameCommand(TileData from, TileData to) : IGameCommand
{
	public void Execute()
	{
		to.Unit = from.Unit;
		from.Unit = null;
	}
}