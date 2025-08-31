using Godot;
using System;

[GlobalClass]
public partial class Attack3State : State
{
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "attack_3";
        fsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Controller.CanAttack = false;
        fsm.Controller.Direction.X = fsm.Controller.GetSignedDirection() * fsm.Controller.dashVelocity / 2;
        fsm.Controller.Direction.Y = -fsm.Controller.jumpVelocity/2;
        fsm.Controller.Dashing = true;;
        fsm.Controller.Velocity = fsm.Controller.Direction;
    }

    public override void Exit()
    {
        base.Exit();
        fsm.Controller.Dashing = false;
    }

    public override State AnimationEnd(string animationName)
    {
        return fsm.PreAttackState;
    }
}