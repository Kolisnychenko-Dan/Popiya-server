namespace Game
{
	public class GameSessionData
	{
		private string _id;
		private GameData _gameData;

		public GameSessionData()
		{
			_gameData = new GameData();
			_id = "123";
		}
	}	
}