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
    public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    private Vector2 _direction;
    private int _lookingDirection = -1;
    
    public override void _Ready()
    {
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

        if (IsOnWall())
        {
            _lookingDirection *= -1;
            GD.Print("Rotate");
        }
        
        MyStateMachine._Process(delta);
        _direction.X = _lookingDirection * Speed;
        if (!IsOnFloor())
        {
            _direction.Y += Gravity * (float)delta;
        }
        else
        {
            _direction.Y = 0;
        }
        Velocity = _direction;
        MoveAndSlide();
    }
}
