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
            Vector2 targetDir = Controller.GlobalPosition.DirectionTo(Target.GlobalPosition).Normalized();
            
            if (Controller.Direction == EnemiesDirection.Horizontal && targetDir.X < 0)
                Controller.MovementDirection = targetDir.X > 0 ? Vector2.Left :  Vector2.Right;
            else
                Controller.MovementDirection = targetDir.Y > 0 ? Vector2.Up :  Vector2.Down;
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
