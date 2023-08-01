using Godot;

public partial class SelectLevel : HBoxContainer
{
	private Node Menu;
	private PackedScene[] Levels = { null, null };

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Menu = this.GetParent().GetParent();

		Levels[0] = GD.Load<PackedScene>("res://Levels/level_1.tscn");
		Levels[1] = GD.Load<PackedScene>("res://Levels/level_2.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnPlayButtonPressed()
	{
		this.Visible = true;

		// foreach (var lv in this.GetChildren())
		// {
		// 	lv.GetNode<Button>("Button").Disabled = false;
		// 	GD.Print("HABILITADO");
		// }

		Button lv1Button = this.GetNode<PanelContainer>("PanelContainer").GetNode<Button>("Button");
		Button lv2Button = this.GetNode<PanelContainer>("PanelContainer2").GetNode<Button>("Button");

		lv1Button.Disabled = false;
		lv2Button.Disabled = false;
	}

	private void OnLv1ButtonPressed()
	{
		GD.Print("APERTADO");
		var instance = Levels[0].Instantiate();

		this.GetTree().Root.AddChild(instance);

		this.QueueFree();
	}

	private void OnLv2ButtonPressed()
	{
		var instance = Levels[1].Instantiate();

		this.GetTree().Root.AddChild(instance);

		this.QueueFree();
	}
}