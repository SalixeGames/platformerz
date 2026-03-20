using Godot;
using System;

public partial class HurtBox : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node body)
	{
		HitBox box = (HitBox)body;
		EmitSignal(SignalName.HurtBoxHit, box.Damage);
	}
	
	[Signal]
	public delegate void HurtBoxHitEventHandler(float damage);
}
