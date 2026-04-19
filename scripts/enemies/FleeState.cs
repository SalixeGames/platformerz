using Godot;
using System;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class FleeState : EnState
{
    private DateTime _timePLayerOut = DateTime.MinValue;
    private bool _playerInVision = true;
    
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

    public override EnState PhysicsUpdate(float delta)
    {
        GD.Print(DateTime.Now - _timePLayerOut);
        if (DateTime.Now - _timePLayerOut > TimeSpan.FromSeconds(1) && !_playerInVision)
        {
            _timePLayerOut = DateTime.MinValue;
            Controller.GoTowardSpawn();
            _playerInVision = true;
            return fsm.EnStates["move"];
        }
        return base.PhysicsUpdate(delta);
    }

    public override EnState OnPlayerOutVision(Area2D visionArea)
    {
        _timePLayerOut = DateTime.Now;
        _playerInVision = false;
        GD.Print(_timePLayerOut);
        return base.OnPlayerOutVision(visionArea);
    }
}
