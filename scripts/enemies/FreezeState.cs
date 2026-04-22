using Godot;
using System;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class FreezeState : EnState
{
    private Vector2 _previousDirection = Vector2.Zero;
    
    public override void Ready(EnStateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "freeze";
        fsm = stateMachine;
    }

    public override EnState Update(float delta)
    {
        if (DateTime.Now - StateTimer >= TimeSpan.FromSeconds(1))
        {
            return fsm.EnStates["flee"];
        }
        return base.Update(delta);
    }

    public override void Enter()
    {
        base.Enter();
        _previousDirection = Controller.MovementDirection;
        Controller.MovementDirection = Vector2.Zero;
    }

    public override void Exit()
    {
        Controller.MovementDirection = _previousDirection;
        base.Exit();
    }
}
