using Godot;
using System;

[GlobalClass]
public partial class WallSlideState : State
{
    public override void Enter()
    {
        base.Enter();
        fsm.Controller.Gravity *= 0.5f;
    }

    public override void Ready(StateMachine stateMachine)
    {
        base.Ready(stateMachine);
        Name = "wall_slide";
        fsm = stateMachine;
    }

    public override State PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);
        return this;
    }

    public override State HandleInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump"))
        {
            if (fsm.Controller.Velocity.Y < -fsm.Controller.jumpVelocity / 2.5f) fsm.Controller.Direction.Y = fsm.Controller.Velocity.Y;
            else fsm.Controller.Direction.Y = -fsm.Controller.jumpVelocity / 2.5f;
            fsm.Controller.Direction.X = fsm.Controller.moveSpeed * -fsm.Controller.GetSignedDirection();
            fsm.Controller.Velocity = fsm.Controller.Direction;
            return fsm.PreviousState;
        }
        return base.HandleInput(@event);
    }

    public override State Update(float delta)
    {
        base.Update(delta);
        fsm.Controller.Velocity = fsm.Controller.Direction;

        if (fsm.Controller.Direction.Y == 0)
        {
            switch (fsm.Controller.LookingDirection)
            {
                case "left":
                    fsm.Controller.LookingDirection = "right";
                    break;
                case "right":
                    fsm.Controller.LookingDirection = "left";
                    break;
            }
            return fsm.States["idle"];
        }
        if (!fsm.Controller.OnWall)
        {
            switch (fsm.Controller.LookingDirection)
            {
                case "left":
                    fsm.Controller.LookingDirection = "right";
                    break;
                case "right":
                    fsm.Controller.LookingDirection = "left";
                    break;
            }
            return fsm.PreviousState;
        }
        return this;
    }

    public override void Exit()
    {
        base.Exit();
        fsm.Controller.Gravity *= 2;
    }
}
