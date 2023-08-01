using Godot;

public partial class PlayerM : CharacterBody2D
{
	[Export] public int MaxHealth = 100;
	public int _Health;
	[Export] public int MoveCost = -10;
	[Export] public int DestructionGain = 20;
	[Export] public int PointsPerBuild = 10;
	[Export] public int PointsPerCity = 100;
	public int _Score = 0;
	public const float Speed = 1350.0f;
	public TileMap LevelMap;
	private AnimationTree animTree;
	private AnimationNodeStateMachinePlayback animState;

	[Signal]
	public delegate void HealthUpdateEventHandler(int hp, int maxHp);

	[Signal]
	public delegate void ScoreUpdateEventHandler(int points);

	[Signal]
	public delegate void DefeatEventHandler();

	[Signal]
	public delegate void ResetEventHandler(Vector2 playerPos);

	public override void _Ready()
	{
		LevelMap = this.GetParent().GetNode<TileMap>("TileMap"); // PATH TO TILEMAP

		_Health = MaxHealth;

		EmitSignal(SignalName.HealthUpdate, _Health, MaxHealth);
		EmitSignal(SignalName.Reset, this.GlobalPosition);

		animTree = this.GetNode<AnimationTree>("AnimationTree");
		animState = (AnimationNodeStateMachinePlayback)animTree.Get("parameters/playback");

		animState.Start("idle");
	}

	public override void _PhysicsProcess(double delta)
	{
		Moving();
	}

	private void Moving()
	{
		Vector2 movement = new();
		Vector2 previousPos = this.GlobalPosition;

		if (Input.IsActionJustPressed("Up"))
		{
			movement.X = -1.0f;
			movement.Y = -1.0f;
		}

		if (Input.IsActionJustPressed("Right"))
		{
			movement.X = 1.0f;
			movement.Y = -1.0f;
		}

		if (Input.IsActionJustPressed("Down"))
		{
			movement.X = 1.0f;
			movement.Y = 1.0f;
		}

		if (Input.IsActionJustPressed("Left"))
		{
			movement.X = -1.0f;
			movement.Y = 1.0f;
		}

		movement.Y /= 2;
		movement = movement.Normalized() * Speed;

		if (movement != Vector2.Zero)
		{
			Velocity = movement;
			MoveAndSlide();
			if (!SnapToTile())
				this.GlobalPosition = previousPos;
		}
	}

	private bool SnapToTile()
	{
		bool canMove = true;

		Vector2 cellPos = LevelMap.ToLocal(this.GlobalPosition);
		Vector2I mapPos = LevelMap.LocalToMap(cellPos);
		if (LevelMap.GetCellSourceId(0, mapPos) >= 0)
		{
			if (LevelMap.GetCellSourceId(1, mapPos) >= 0)
			{
				LevelMap.EraseCell(1, mapPos);
				UpdateHp(DestructionGain);
				WinCondition();
				_Score += PointsPerBuild;
				EmitSignal(SignalName.ScoreUpdate, _Score);

				animState.Travel("eatingBuilding");
				animState.Start("eatingBuilding", true);
			}
			else
			{
				UpdateHp(MoveCost);
				animState.Travel("idle");
			}
			cellPos = LevelMap.MapToLocal(mapPos);
			this.GlobalPosition = LevelMap.ToGlobal(cellPos);
		}
		else
			canMove = false;

		return canMove;
	}

	private void UpdateHp(int hp)
	{
		_Health += hp;
		if (_Health > MaxHealth)
			_Health = MaxHealth;
		EmitSignal(SignalName.HealthUpdate, _Health, MaxHealth);

		if (_Health <= 0)
		{
			EmitSignal(SignalName.Defeat);
			this.QueueFree();
		}
	}

	private void WinCondition()
	{
		if (LevelMap.GetUsedCells(1).Count <= 0)
		{
			//EmitSignal(SignalName.Victory);
			//this.SetPhysicsProcess(false);
			EmitSignal(SignalName.Reset, this.GlobalPosition);
			_Score += PointsPerCity;
		}
	}
}