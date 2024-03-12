using Godot;
using System;

public partial class UIController : Control
{

	private Label HpLabel;
	private Label ScoreLabel;
	private Container DefeatC;
	private TextureProgressBar HpBar;
	private int FinalScore;
	private AudioStreamPlayer loseSound;
	private AudioStreamPlayer buttonSound;

	[Signal]
	public delegate void RestartEventHandler();
	[Signal]
	public delegate void MenuEventHandler();
	
	public override void _Ready()
	{
		HBoxContainer hBox = this.GetNode<HBoxContainer>("HBoxContainer");
		VBoxContainer vBox = hBox.GetNode<VBoxContainer>("VBoxContainer");
		HpBar = vBox.GetNode<TextureProgressBar>("TextureProgressBar");
		HpLabel = vBox.GetNode<Label>("HPLabel");

		ScoreLabel = hBox.GetNode<Label>("ScoreLabel");
		
		DefeatC = this.GetNode<PanelContainer>("PanelContainer");
		
		loseSound = this.GetNode<AudioStreamPlayer>("AudioSPlayer Defeat");
		buttonSound = this.GetNode<AudioStreamPlayer>("AudioSPlayer Button");
	}

	public override void _Process(double delta)
	{
	}

	private void OnHealthUpdate(int hp, int maxHp)
	{
		HpLabel.Text = hp + " / " + maxHp;
		HpBar.Value = hp;
	}

	private void OnScoreUpdate(int points)
	{
		ScoreLabel.Text = "Score: " + points;
		FinalScore = points;
	}

	private void OnPlayerMDefeat()
	{
		loseSound.Play();
		
		DefeatC.Visible = true;
		Label defeatLabel = DefeatC.GetNode<Label>("DefeatLabel");
		Button restartButton = DefeatC.GetNode<Button>("Button");
		Button menuButton = DefeatC.GetNode<Button>("Button2");

		defeatLabel.Text = "YOU LOSE\nFINAL SCORE: " + FinalScore;
		restartButton.Disabled = false;
		menuButton.Disabled = false;
	}

	private void OnRestartButtonPressed()
	{
		buttonSound.Play();
		EmitSignal(SignalName.Restart);
	}

	private void OnMenuButtonPressed()
	{
		buttonSound.Play();
		EmitSignal(SignalName.Menu);
	}

}
