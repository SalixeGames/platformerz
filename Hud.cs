using Godot;
using System;

public partial class Hud : Control
{
	[Export] public Label HealthLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalScript.Instance.DataLoaded += _on_data_loaded;
		HealthLabel.Text = "Health: " + GlobalScript.Instance.Health;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_hurt_box_hit(float damage)
	{
		HealthLabel.Text = "Health: " + GlobalScript.Instance.Health;
	}

	public void _on_data_loaded()
	{
		HealthLabel.Text = "Health: " + GlobalScript.Instance.Health;
	}
}
