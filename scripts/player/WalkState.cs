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

    public override void Enter()
    {
        base.Enter();
        if (fsm.PreviousState != null && fsm.PreviousState.Name == "wall_slide" && fsm.Controller.Direction.Y < 0)
        {
            fsm.Controller.CanAerialStraffe = false;
        }
    }

    public override State Update(float delta)
    {
        if (fsm.Controller.OnWall && fsm.Controller.Direction.X != 0 && fsm.Controller.Direction.Y != 0 && GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.WallSlide))
        {
            if (fsm.Controller.Direction.Y > 0)
            {
                fsm.Controller.Direction.Y = 0;
            }
            return fsm.States["wall_slide"];
        }
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
