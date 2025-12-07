using Godot;
using System;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class MoveState : EnState
{
    public override void Ready(EnStateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "move";
        fsm = stateMachine;
    }
}
