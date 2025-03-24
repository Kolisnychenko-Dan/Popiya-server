namespace Game.GameCommands
{
	public interface IGameCommand
	{
		bool TryExecute(GameData gameData);
	}	
}