using Godot;
using System;
using Platformerz.scripts.enemies;

public partial class BaseEnemy : CharacterBody2D
{
    [ExportCategory("Enemy Type")]
    [Export] public EnemiesMovement MovementType = EnemiesMovement.Walking;
    [Export] public EnemiesDirection Direction = EnemiesDirection.Horizontal;
    [Export] public EnemiesAggroLevel AggroLevel = EnemiesAggroLevel.Passive;
    [Export] public EnemiesAttackType AttackType = EnemiesAttackType.Forward;
    
    [ExportCategory("State Machine")]
    [Export] public EnemyStateMachine MyStateMachine = new EnemyStateMachine();
    
    [ExportCategory("Animation")]
    [Export] public AnimationPlayer Animator;
    
    [ExportCategory("Stats")]
    [Export] public int BaseHealth = 100;
    [Export] public int Speed = 10;
    public float Gravity = 0.75f * ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    private Vector2 _direction = Vector2.Left;
    private Vector2 _movementVector = Vector2.Zero;
    private float _downwardsVelocity = 0.0f;
    
    public override void _Ready()
    {
        if (Direction == EnemiesDirection.Vertical)
        {
            _direction = Vector2.Down;
        }
        MyStateMachine?._Ready(this);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Position.Y > 600)
            QueueFree();
        
        MoveAndSlide();
        MyStateMachine._PhysicsProcess(delta);
    }
	
    public void UpdateAnim(string state) {
        Animator.Play(state);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if ((IsOnWall() && Direction == EnemiesDirection.Horizontal) || 
            (IsOnFloor() && Direction == EnemiesDirection.Vertical))
        {
            _direction *= -1;
        }
        
        MyStateMachine._Process(delta);
        if (!IsOnFloor() && MovementType == EnemiesMovement.Walking)
        {
            _downwardsVelocity += Gravity * (float)delta;
        }
        else if (IsOnFloor() && MovementType == EnemiesMovement.Walking)
        {
            _downwardsVelocity = 0;
        }
        _movementVector = _direction * Speed;
        _movementVector.Y += _downwardsVelocity;
        
        Velocity = _movementVector;
        MoveAndSlide();
    }
}
