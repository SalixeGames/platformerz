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

    public override EnState Update(float delta)
    {
        if (Controller.MovementType == EnemiesMovement.Both)
        {
            _deltaWall -= delta;
            if (Controller.IsOnWall() && !_onWall && _deltaWall <= 0)
            {
                _onWall = true;
                _deltaWall = 1.0f;
                Controller.MovementDirection = Vector2.Up;
            }
            else if (Controller.IsOnFloor() && _onWall && _deltaWall <= 0)
            {
                _onWall = false;
                _deltaWall = 1.0f;
                Controller.MovementDirection = Vector2.Left;
            }
        }
        return base.Update(delta);
    }
}
