namespace Game;

public class TileData
{
	private readonly TileCoordinates _coordinates;
	private UnitData? _unitData;
	
	public TileCoordinates Coordinates { get; }

	public UnitData? Unit
	{
		get => _unitData;
		set => _unitData = value;
	}

}