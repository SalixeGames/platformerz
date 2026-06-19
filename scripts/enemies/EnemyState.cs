using Godot;
using System;

[GlobalClass]
public partial class EnemyState : Resource
{
    private BaseEnemy _controller;
    
    public void Enter(BaseEnemy controller)
    {
        _controller = controller;
    }
    
    public void Exit()
    {
        
    }
    
    public EnemyState OnProcess(double delta)
    {
        return this;
    }
    
    public EnemyState OnPhysicProcess(double delta)
    {
        Vector2 velocity = _controller.Velocity;
        // Add the gravity.
        if (!_controller.IsOnFloor())
        {
            velocity += _controller.GetGravity() * (float)delta;
        }
        
        _controller.Velocity = velocity;
        return this;
    }
}
