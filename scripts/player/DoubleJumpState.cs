using Godot;
using System;

[GlobalClass]
public partial class DoubleJumpState : State
{

    public override void Enter()
    {
        base.Enter();
        fsm.Controller.CanAerialStraffe = false;
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
        
        fsm.Controller.Velocity = fsm.Controller.Direction;
        return this;
    }
}
