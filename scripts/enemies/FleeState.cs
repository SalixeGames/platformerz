using Godot;
using System;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class FleeState : EnState
{
    private DateTime _timePLayerOut = DateTime.MinValue;
    
    public override void Ready(EnStateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "flee";
        fsm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _timePLayerOut = DateTime.Now;
        
        if (Target == null)
        {
            Controller.MovementDirection *= -1;
        }
        else
        {
            Controller.SetDirToTarget(Target.GlobalPosition, true);
        }
    }

    public override EnState PhysicsUpdate(float delta)
    {
        if (DateTime.Now - _timePLayerOut > TimeSpan.FromSeconds(1) && !PlayerInVision)
        {
            _timePLayerOut = DateTime.MinValue;
            Controller.GoTowardSpawn();
            PlayerInVision = true;
            return fsm.EnStates["move"];
        }
        return base.PhysicsUpdate(delta);
    }

    public override EnState OnPlayerOutVision(Area2D visionArea)
    {
        _timePLayerOut = DateTime.Now;
        return base.OnPlayerOutVision(visionArea);
    }
}
