using Godot;
using System;

[GlobalClass]
public partial class IdleState : State
{
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "idle";
        fsm = stateMachine;
    }

    public override State PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);
        return this;
    }

    public override State HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump"))
        {
            fsm.Controller.Direction.Y = -fsm.Controller.jumpVelocity;
            fsm.Controller.Velocity = fsm.Controller.Direction;
        }
        return base.HandleInput(@event);
    }

    public override State Update(float delta)
    {
        base.Update(delta);
        if (fsm.Controller.Direction.X != 0)
        {
            return fsm.States["walk"];
        }
        if (fsm.Controller.Direction.Y < 0)
        {
            return fsm.States["jump"];
        }
        fsm.Controller.Velocity = fsm.Controller.Direction;
        return this;
    }
}
