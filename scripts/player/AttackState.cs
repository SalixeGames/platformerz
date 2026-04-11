using Godot;
using System;

[GlobalClass]
public partial class AttackState : State
{
    private bool _combo = false;
    
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
        _combo = false;
    }

    public override State HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("attack") && 
            GlobalScript.Instance.PowersList.Contains(GlobalScript.Powerups.Combo1))
        {
            _combo = true;
        }
        return base.HandleInput(@event);
    }

    public override State AnimationEnd(string animationName)
    {
        if (_combo)
        {
            fsm.Controller.Direction.Y = -fsm.Controller.jumpVelocity/2;
            fsm.Controller.Velocity = fsm.Controller.Direction;
            return fsm.States["attack_2"];
        }
        return fsm.PreAttackState;
    }
}
