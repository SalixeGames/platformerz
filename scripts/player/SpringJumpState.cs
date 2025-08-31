using Godot;
using System;

[GlobalClass]
public partial class SpringJumpState : State
{
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "spring_jump";
        fsm = stateMachine;
    }

    public override State Update(float delta)
    {
        base.Update(delta);
        if (fsm.Controller.Direction.X != 0)
        {
            if (Input.IsActionPressed("sprint") &&
                GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.Sprint))
            {
                return fsm.States["sprint"];
            }
            return fsm.States["walk"];
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
            fsm.Controller.Direction.Y = -fsm.Controller.jumpVelocity * fsm.Controller.SpringJumpModif;
            fsm.Controller.Velocity = fsm.Controller.Direction;
            return fsm.PreviousState;
        }
        return base.HandleInput(@event);
    }

    public override State AnimationEnd(string animationName)
    {
        return fsm.States["idle"];
    }
}
