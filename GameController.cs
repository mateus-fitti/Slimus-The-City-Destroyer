using Godot;

public partial class GameController : Node2D
{
	private PackedScene LevelScene;
	private PackedScene MenuScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LevelScene = GD.Load<PackedScene>(this.SceneFilePath);
		MenuScene = GD.Load<PackedScene>("res://menu.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnRestart()
	{
		var instance = LevelScene.Instantiate();

		this.GetTree().Root.AddChild(instance);

		this.QueueFree();
	}

	private void OnMenu()
	{
		var instance = MenuScene.Instantiate();

		this.GetTree().Root.AddChild(instance);

		this.QueueFree();
	}
}