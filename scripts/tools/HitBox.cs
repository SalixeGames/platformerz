using Godot;
using System;

public partial class HitBox : Area2D
{
	[Export] public float Damage;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetDamage(float damage)
	{
		Damage = damage;
	}

	public float GetDamage()
	{
		return Damage;
	}
}
