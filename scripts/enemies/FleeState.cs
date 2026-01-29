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
        GD.Print("Ready to flee");
    }

    public override EnState OnPlayerOutVision(Area2D visionArea)
    {
        return fsm.EnStates["move"];
    }
}
