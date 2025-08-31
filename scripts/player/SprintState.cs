using Godot;
using System;

[GlobalClass]
public partial class SprintState : State
{
    private float _speedModif = 1.75f;
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "sprint";
        fsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Controller.moveSpeed *= _speedModif;
    }

    public override State PhysicsUpdate(float delta)
    {
        if (fsm.Controller.Direction.X == 0)
        {
            return fsm.States["idle"];
        }
        if (fsm.Controller.Direction.Y < 0)
        {
            return fsm.States["jump"];
        }
        if (!Input.IsActionPressed("sprint")) return fsm.States["walk"];
        return base.PhysicsUpdate(delta);
    }

    public override State HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump"))
        {
            fsm.Controller.Direction.Y = -fsm.Controller.jumpVelocity;
            fsm.Controller.Velocity = fsm.Controller.Direction;
        }

        bool canDash = fsm.Controller.CanDash && GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.Dash);
        if (@event.IsActionPressed("dash") && canDash)
        {
            return fsm.States["dash"];
        }
        return base.HandleInput(@event);
    }

    public override void Exit()
    {
        base.Exit();
        fsm.Controller.moveSpeed /= _speedModif;
    }
}
