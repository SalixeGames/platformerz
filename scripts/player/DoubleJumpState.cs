using Godot;
using System;

[GlobalClass]
public partial class DoubleJumpState : State
{

    public override void Enter()
    {
        base.Enter();
        if (fsm.PreviousState != null && fsm.PreviousState.Name == "wall_slide")
        {
            fsm.Controller.CanAerialStraffe = false;
        }
    }
    
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "double_jump";
        fsm = stateMachine;
    }

    public override State Update(float delta)
    {
        base.Update(delta);
        if (fsm.Controller.OnWall && fsm.Controller.Direction.X != 0 && GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.WallSlide))
        {
            if (fsm.Controller.Direction.Y > 0)
            {
                fsm.Controller.Direction.Y = 0;
            }
            return fsm.States["wall_slide"];
        }
        if (fsm.Controller.Direction.Y == 0)
        {
            fsm.Controller.Direction.Y = 0;
            return fsm.States["idle"];
        }

        if (fsm.Controller.OnCeil && GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.CeilingJump))
        {
            return fsm.States["ceil_reset"];
        }
        fsm.Controller.Velocity = fsm.Controller.Direction;
        return this;
    }

    public override State HandleInput(InputEvent @event)
    {
        bool canDash = fsm.Controller.CanDash && GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.Dash);
        if (@event.IsActionPressed("dash") && canDash)
        {
            return fsm.States["dash"];
        }
        return base.HandleInput(@event);
    }
}
