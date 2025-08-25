using System.Linq;
using Godot;

namespace Platformerz.scripts.player;

public partial class PlayerController : CharacterBody2D
{
	
    [Export] public float moveSpeed = 150.0f;
    [Export] public float jumpVelocity = 800.0f;
    [Export] public StateMachine stateMachine = new StateMachine();
    
    public string StateName = "idle";
    public string OldStateName = null;
    public string LookingDirection = "right";
    public string WalkingDirection = "right";
    
    public bool OnWall = false;
    public bool OnCeil = false;
    public bool OnFloor = false;

    public Vector2 Direction = Vector2.Zero;
	
    // get gravity from project settings (keep everything synced)
    public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    [Export] public AnimationPlayer Animator;
    
    public override void _Ready()
    {
        stateMachine?._Ready(this);
        GlobalScript.Instance.LoadGame("test_save");
    }

    /* public override void _ExitTree()
    {
        GlobalScript.Instance.SaveGame("test_save");
    } */

    public override void _PhysicsProcess(double delta)
    {
        if (Position.Y > 600)
            Position = Vector2.Zero;
        
        MoveAndSlide();
        stateMachine._PhysicsProcess(delta);
    }
	
    public void UpdateAnim(string state) {
        Animator.Play(state + "_" + _GetLookingDirection());
    }

    private string _GetLookingDirection()
    {
        WalkingDirection = LookingDirection;
        
        if(Direction.X > 0)
            LookingDirection = "right";
        else if(Direction.X < 0)
            LookingDirection = "left";
        
        return LookingDirection;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        Direction.X = (Input.GetActionStrength("walk_right") - Input.GetActionStrength("walk_left")) * moveSpeed;
        
        // apply gravity if in the air
        if (!IsOnFloor()) {
            Direction.Y += Gravity * (float)delta;
        }
        else
        {
            Direction.Y = 0;
        }

        if (Input.IsActionJustPressed("get_info"))
        {
            GD.Print("Life: " + GlobalScript.Instance.Health.ToString() + " \nBlobs: " + GlobalScript.Instance.BlobsList + " \nPowers: " + GlobalScript.Instance.PowersList);
            GlobalScript.Instance.SaveGame("test_save");
        }
        
        stateMachine._Process(delta);
        Velocity = Direction;
        MoveAndSlide();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        stateMachine._UnhandledInput(@event);
    }

    public void _on_ceiling_entered(Node2D body)
    {
        OnCeil = true;
        Direction.Y = 0;
    }

    public void _on_floor_entered(Node2D body)
    {
        OnFloor = true;
        Direction.Y = 0;
    }

    public void _on_wall_entered(Node2D body)
    {
        OnWall = true;
    }

    public void _on_ceiling_left(Node2D body)
    {
        OnCeil = false;
    }

    public void _on_floor_left(Node2D body)
    {
        OnFloor = false;
    }

    public void _on_wall_left(Node2D body)
    {
        OnWall = false;
    }

    public void _on_animation_player_animation_finished(string animationName)
    {
        stateMachine._AnimationEnd(animationName);
    }

    public void _on_drop_area_entered(Node2D body)
    {
        if (body.GetType() == typeof(Blob))
        {
            Blob blob = (Blob)body;
            GD.Print(blob.id);
            GlobalScript.Instance.BlobsList.Add(blob.id);
            body.QueueFree();
        }
        if (body.GetType() == typeof(Powerups)) 
        {
            Powerups powerup = (Powerups)body;
            GD.Print(powerup.Powerup);
            GlobalScript.Instance.PowersList.Add(powerup.Powerup);
            body.QueueFree();
        }
    }
}