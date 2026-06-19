using Godot;
using System;

public partial class BaseEnemy : CharacterBody2D
{
	[ExportCategory("Movement")] 
	[Export] public float Speed = 50.0f;
	// Which way the enemy go first
	[Export] public Vector2 InitialDirection = Vector2.Left;

	[ExportCategory("StateMachine")] [Export]
	public EnemyStateMachine StateMachine = new EnemyStateMachine();
	
	private Vector2 _actualDirection = Vector2.Zero;  // Indicate which way is the Enemy facing
	
	public override void _Ready()
	{
		StateMachine?._Ready(this);
		_actualDirection = InitialDirection;
	}

	public override void _PhysicsProcess(double delta)
	{
		StateMachine?._PhysicsProcess(delta);
		MoveAndSlide();
	}

	public override void _Process(double delta)
	{
		StateMachine?._Process(delta);
		base._Process(delta);
	}
}
