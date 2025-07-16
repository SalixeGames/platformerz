using Godot;
using System;

[GlobalClass]
public partial class WalkState : State
{
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "walk";
        fsm = stateMachine;
    }

    public override State Update(float delta)
    {
        if (fsm.Controller.Direction.X == 0)
        {
            return fsm.States["idle"];
        }
        if (fsm.Controller.Direction.Y < 0)
        {
            return fsm.States["jump"];
        }
        
        fsm.Controller.Velocity = fsm.Controller.Direction;
        
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
}
