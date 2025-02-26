namespace Game;

public class UnitData
{
	private TileData _tileData;
	private int _hp;

	public int Hp
	{
		get => _hp;
		set => _hp = value;
	}

	public int MoveSpeed { get; } = 1;
}