using Godot;
using System;

[GlobalClass]
public partial class DashState : State
{
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "dash";
        fsm = stateMachine;
    }

    public override State PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);
        return this;
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Controller.CanDash = false;
        fsm.Controller.Dashing = true;
        fsm.Controller.Direction.X = fsm.Controller.GetSignedDirection() * fsm.Controller.dashVelocity;
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
        return this;
    }
    
    public override State AnimationEnd(string animationName) 
    {
        
        return fsm.PreviousState;
    }

    public override void Exit()
    {
        base.Exit();
        fsm.Controller.Dashing = false;
    }
}
