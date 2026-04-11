using Godot;
using System;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class KnockBackState : EnState
{
    private Vector2 BaseDirection;
    
    public override void Ready(EnStateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "knock_back";
        fsm = stateMachine;
    }

    public override void Enter()
    {
        BaseDirection = Controller.MovementDirection;
        base.Enter();
        if (Target == null)
        {
            Controller.MovementDirection *= -2;
        }
        else
        {
            Controller.SetDirToTarget(2 * Target.GlobalPosition, true);
        }
    }

    public override EnState PhysicsUpdate(float delta)
    {
        Controller.MovementDirection /= (2.0f * delta);
        return base.PhysicsUpdate(delta);
    }

    public override EnState AnimationEnd(StringName animationName)
    {
        GD.Print("Anime End");
        if (animationName == "knock_back")
        {
            return fsm.EnStates["move"];
        }
        return base.AnimationEnd(animationName);
    }

    public override void Exit()
    {
        GD.Print("Exit called");
        Controller.MovementDirection = BaseDirection;
        if (Controller.Health <= 0)
        {
            Controller.Die();
        }
        base.Exit();
    }
    
    public override EnState OnHurt(float damage)
    {
        return this;
    }
}
