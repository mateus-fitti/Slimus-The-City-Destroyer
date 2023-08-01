using Godot;

public partial class MouseTile : TileMap
{
	private Sprite2D HoverTile;

	public override void _Ready()
	{
		HoverTile = this.GetNode<Sprite2D>("Sprite2D");
	}

	public override void _Process(double delta)
	{
		Vector2 cellPos = this.ToLocal(GetTrueMousePosition());
		Vector2I mapPos = this.LocalToMap(cellPos);

		if (this.GetCellSourceId(0, mapPos) >= 0)
		{
			HoverTile.Visible = true;
			HoverTile.Position = this.MapToLocal(mapPos);
		}
		else
			HoverTile.Visible = false;

		// if (Input.IsActionPressed("LMouseButton"))
		// {
		// 	Vector2 cellPos = this.ToLocal(GetGlobalMousePosition());
		// 	Vector2I mapPos = this.LocalToMap(cellPos);
		// 	//this.EraseCell(0, mapPos);
		// 	//tile_map.tile_set.tile_set_modulate(id, color)
		// 	this.SetCell(0, mapPos, 0, this.GetCellAtlasCoords(0, mapPos), 1);
		// }
		// if (Input.IsActionJustReleased("LMouseButton"))
		// {
		// 	Vector2 cellPos = this.ToLocal(GetGlobalMousePosition());
		// 	Vector2I mapPos = this.LocalToMap(cellPos);
		// 	//this.EraseCell(0, mapPos);
		// 	//tile_map.tile_set.tile_set_modulate(id, color)
		// 	this.SetCell(0, mapPos, 0, this.GetCellAtlasCoords(0, mapPos), 0);
		// }
	}

	private Vector2 GetTrueMousePosition()
	{
		Vector2 truePosition = GetGlobalMousePosition();
		truePosition.Y -= this.TileSet.TileSize.Y / 2;

		return truePosition;
	}
}