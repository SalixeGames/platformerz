using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class EnemyStateMachine : Resource
{
    public enum EnStates
    {
        Idle,
        Walk,
        Float,
        Attack,
        Back,
        Flee
    }
    
    [Export]
    public Dictionary<EnStates, EnemyState> States = new Dictionary<EnStates, EnemyState>{
        { EnStates.Idle, new EnIdleState() }
    };
    
    private EnemyState _curState = new EnemyState();
    private EnemyState _prevState = new EnemyState();
    private BaseEnemy _controller;
    
    public void _Ready(BaseEnemy controller)
    {
        _controller = controller;
        
        _curState = States[EnStates.Idle];
        _curState?.Enter(_controller);
        _prevState = _curState;
    }
    
    public void _Process(double delta)
    {
        TransitionTo(_curState.OnProcess(delta));
    }
    
    public void _PhysicsProcess(double delta)
    {
        TransitionTo(_curState.OnPhysicProcess(delta));
    }
    
    public void TransitionTo(EnemyState newState)
    {
        if (newState == _curState)
            return;
        
        _prevState = _curState;
        _curState?.Exit();
        _curState = newState;
        _curState?.Enter(_controller);
    }
}
