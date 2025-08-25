using Godot;
using System;

[GlobalClass]
public partial class JumpState : State
{
    private bool _canJump = false;
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "jump";
        fsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _canJump = false;
    }

    public override State HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump") && GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.DoubleJump))
        {
            _canJump = true;
            fsm.Controller.Direction.Y = -fsm.Controller.jumpVelocity;
            fsm.Controller.Velocity = fsm.Controller.Direction;
        }
        return base.HandleInput(@event);
    }

    public override State Update(float delta)
    {
        base.Update(delta);
        if (fsm.Controller.Direction.Y == 0)
        {
            fsm.Controller.Direction.Y = 0;
            return fsm.States["idle"];
        }
        if (fsm.Controller.Direction.Y < 0 && _canJump)
        {
            return fsm.States["double_jump"];
        }
        
        fsm.Controller.Velocity = fsm.Controller.Direction;
        return this;
    }
}
