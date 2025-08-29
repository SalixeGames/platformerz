using Godot;
using System;

[GlobalClass]
public partial class AttackState : State
{
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "attack";
        fsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Controller.CanAttack = false;
    }

    public override State AnimationEnd(string animationName)
    {
        return fsm.PreviousState;
    }
}
