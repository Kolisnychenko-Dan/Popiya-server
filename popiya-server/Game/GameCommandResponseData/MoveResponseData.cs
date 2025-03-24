using Game.GameCommandData;

namespace Game.GameCommandResponseData
{
	public class MoveResponseData
	{
		public MoveGameCommandData MoveGameCommandData { get; }
		
		public MoveResponseData(MoveGameCommandData moveGameCommandData)
		{
			MoveGameCommandData = moveGameCommandData;
		}
	}	
}