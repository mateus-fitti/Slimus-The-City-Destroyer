using Godot;

public partial class MapController : TileMap
{
	[Export] public int MaxBuilds = 5;
	[Export] public int SpawnChance = 10; // INCREASE MEANS LESS CHANCE TO SPAWN ALL BUILDINGS

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnPlayerMReset(Vector2 playerPos)
	{
		Godot.Collections.Array<Vector2I> city = this.GetUsedCells(0);
		Vector2 localP = this.ToLocal(playerPos);
		Vector2I mapP = this.LocalToMap(localP);
		Vector2I atlasBuild = new(0, 2); // ATLAS COORD FOR BUILDING
		int buildCount = 0;
		RandomNumberGenerator rng = new();

		foreach (var cell in city)
		{
			if (buildCount < MaxBuilds && cell != mapP)
			{
				int n = rng.RandiRange(1, SpawnChance);
				if (n == 1)
				{
					this.SetCell(1, cell, 0, atlasBuild, 0);
					buildCount++;
				}
			}
		}
	}
}