using Godot;
using System;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class MoveState : EnState
{
    private bool _onWall = true;
    private float _deltaWall = 0f;
    
    public override void Ready(EnStateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "move";
        fsm = stateMachine;
    }

    public override void Enter()
    {
        GD.Print("Entering move");
        base.Enter();
    }

    public override EnState OnPlayerInVision(Area2D visionArea)
    {
        base.OnPlayerInVision(visionArea);
        fsm.EnStates["flee"].Target = visionArea;
        return fsm.EnStates["freeze"];
    }
}
