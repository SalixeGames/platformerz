using Godot;
using System;

[GlobalClass]
public partial class CeilResetState : State
{
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "ceil_reset";
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

        fsm.Controller.Gravity /= 20;
        if (fsm.PreviousState != null && fsm.PreviousState.Name == "wall_slide")
        {
            fsm.Controller.CanAerialStraffe = false;
        }
    }

    public override State Update(float delta)
    {
        base.Update(delta);
        return this;
    }
    
    public override State AnimationEnd(string animationName) 
    {
        if (fsm.PreviousState == fsm.States["double_jump"]) return fsm.States["jump"];
        return fsm.States["idle"];
    }

    public override void Exit()
    {
        fsm.Controller.Gravity *= 20;
        base.Exit();
    }
}
