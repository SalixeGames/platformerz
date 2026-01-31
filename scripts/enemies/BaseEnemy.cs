using Godot;
using System;
using System.Linq;
using Platformerz.scripts.enemies;

public partial class BaseEnemy : CharacterBody2D
{
    [ExportCategory("Enemy Type")]
    [Export] public EnemiesMovement MovementType = EnemiesMovement.Walking;
    [Export] public EnemiesDirection Direction = EnemiesDirection.Horizontal;
    [Export] public EnemiesAggroLevel AggroLevel = EnemiesAggroLevel.Passive;
    [Export] public EnemiesAttackType AttackType = EnemiesAttackType.Forward;
    
    [ExportCategory("State Machine")]
    public EnStateMachine StateMachine = new EnStateMachine();
    
    [ExportCategory("Usefull Nodes")]
    [Export] public AnimationPlayer Animator;
    [Export] public Sprite2D Sprite;
    [Export] public Area2D EnVisionArea;
    [Export] public Polygon2D Home;
    
    [ExportCategory("Stats")]
    [Export] public int BaseHealth = 100;
    [Export] public int Speed = 10;
    [Export] public float RoamingDistance = 50.0f;
    
    public Vector2 SpawnPosition = Vector2.Zero;
    public Vector2 OldSpawnPosition = Vector2.Zero;
    public float Gravity = 0.75f * ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public Vector2 MovementDirection = Vector2.Right;
    
    public override void _Ready()
    {
        if (Direction == EnemiesDirection.Vertical)
        {
            MovementDirection = Vector2.Down;
        }

        _set_sprite_properties();
        if (MovementType != EnemiesMovement.Walking || IsOnFloor())
        {
            SpawnPosition = GlobalPosition;
        }
        StateMachine?._Ready(this, EnVisionArea);
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
        
        MoveAndSlide();
    }
	
    public void UpdateAnim(string state) {
        Animator.Play(state);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (NeedTurnAround())
        {
            MovementDirection *= -1;
        }
        
        if (!IsOnFloor() && MovementType == EnemiesMovement.Walking)
        {
            MovementDirection.Y += (Gravity * (float)delta) / Speed;
            if (OldSpawnPosition == Vector2.Zero)
                SpawnPosition = GlobalPosition;
        }
        else if (IsOnFloor() && MovementType == EnemiesMovement.Walking)
        {
            MovementDirection.Y = 0;
        }
        
        if (IsOnFloor() && OldSpawnPosition == Vector2.Zero)
        {
            OldSpawnPosition = SpawnPosition;
            SpawnPosition = GlobalPosition;
        }
        if (SpawnPosition != OldSpawnPosition && IsOnFloor())
        {
            OldSpawnPosition = SpawnPosition;
            Home.GlobalPosition = SpawnPosition;
        }

        Scale = new Vector2(MovementDirection.X != 0 ? -MovementDirection.X : 1, 1);
        Rotation = 0;
        
        StateMachine._Process(delta);
        Velocity = (MovementDirection * Speed);
        MoveAndSlide();
    }

    public bool NeedTurnAround()
    {
        bool onWallAndHorizontal = IsOnWall() && MovementDirection.X != 0;
        bool onFloorAndVertical = IsOnFloor() && MovementDirection.Y != 0;
        bool tooFar = Math.Abs(GlobalPosition.DistanceTo(SpawnPosition)) > RoamingDistance;
        return onWallAndHorizontal || onFloorAndVertical || tooFar;
    }
}
