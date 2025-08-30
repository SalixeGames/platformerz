using Godot;
using System;

[GlobalClass]
public partial class Attack2State : State
{
    private bool _combo = false;
    
    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "attack_2";
        fsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Controller.CanAttack = false;
        _combo = false;
    }

    public override State HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("attack") && 
            GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.Combo2))
        {
            _combo = true;
        }
        return base.HandleInput(@event);
    }

    public override State AnimationEnd(string animationName)
    {
        if (_combo) return fsm.States["attack_3"];
        return fsm.PreAttackState;
    }
}
