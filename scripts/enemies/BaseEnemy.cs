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
    [Export] public EnStateMachine StateMachine;
    
    [ExportCategory("Usefull Nodes")]
    [Export] public AnimationPlayer Animator;
    [Export] public Sprite2D Sprite;
    
    [ExportCategory("Stats")]
    [Export] public int BaseHealth = 100;
    [Export] public int Speed = 10;
    [Export] public float RoamingDistance = 50.0f;
    
    public Vector2 SpawnPosition = Vector2.Zero;
    public float Gravity = 0.75f * ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public Vector2 MovementDirection = Vector2.Left;
    public int MovementOrientation = 1;
    private Vector2 _movementVector = Vector2.Zero;
    private float _downwardsVelocity = 0.0f;
    
    public override void _Ready()
    {
        if (Direction == EnemiesDirection.Vertical)
        {
            MovementDirection = Vector2.Down;
        }

        _set_sprite_properties();
        SpawnPosition = GlobalPosition;
        StateMachine?._Ready(this);
    }

    private void _set_sprite_properties()
    {
        Sprite.Frame = (int) MovementType;
        switch (MovementType)
        {
            case EnemiesMovement.Flying:
                Animator.Play("fly_idle");
                break;
            case EnemiesMovement.Both:
                Animator.Play("duo_idle");
                break;
            default:
                Animator.Play("wlk_idle");
                break;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Position.Y > 600)
            QueueFree();
        
        Scale = new Vector2(MovementOrientation, 1);
        Rotation = 0;
        MoveAndSlide();
    }
	
    public void UpdateAnim(string state) {
        Animator.Play(state);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if ((IsOnWall() && MovementDirection.X != 0) || 
            (IsOnFloor() && MovementDirection.Y != 0) ||
            Math.Abs(GlobalPosition.DistanceTo(SpawnPosition)) > RoamingDistance)
        {
            if (MovementType == EnemiesMovement.Both)
            {
            }
            MovementOrientation *= -1;
            if ((IsOnFloor() && MovementDirection.Y != 0) || 
                (IsOnWall() && MovementDirection.X != 0))
            {
                SpawnPosition = GlobalPosition;
            }
        }
        
        if (!IsOnFloor() && MovementType == EnemiesMovement.Walking)
        {
            _downwardsVelocity += Gravity * (float)delta;
        }
        else if (IsOnFloor() && MovementType == EnemiesMovement.Walking)
        {
            _downwardsVelocity = 0;
        }
        _movementVector = MovementDirection * MovementOrientation * Speed;
        _movementVector.Y += _downwardsVelocity;
        
        StateMachine._Process(delta);
        Velocity = _movementVector;
        MoveAndSlide();
    }

    void _on_vision_area_entered(Area2D area)
    {
        GD.Print("Hello there!");
    }
}
