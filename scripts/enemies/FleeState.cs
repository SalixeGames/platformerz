using Godot;
using System;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class FleeState : EnState
{
    
    public override void Ready(EnStateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "flee";
        fsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        if (Target == null)
            Controller.MovementDirection *= -1;
        else
        {
            Controller.SetDirToTarget(Target.GlobalPosition, true);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override EnState OnPlayerOutVision(Area2D visionArea)
    {
        base.OnPlayerOutVision(visionArea);
        return fsm.EnStates["move"];
    }
}
