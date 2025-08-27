using System;
using System.Linq;
using Godot;
using Godot.Collections;
using Platformerz.scripts.player;

[GlobalClass]
public partial class StateMachine : Resource
{
    [Export]
    public Array<State> StatesList;
    
    public System.Collections.Generic.Dictionary<string, State> States;
    private State _currentState;
    public State PreviousState;
    public DateTime LastTransition;
    public PlayerController Controller;

    public StateMachine()
    {
    }

    public void _Ready(PlayerController controller)
    {
        Controller = controller;
        
        States = new System.Collections.Generic.Dictionary<string, State>();
        if (StatesList?.Count == null)
        {
            StatesList = new Array<State>();
            StatesList.Add(new IdleState());
            StatesList.Add(new WalkState());
        }
        
        foreach (State state in StatesList) {
            state.Ready(this);
            States.Add(state.Name, state);
            States[state.Name] = state;
            state.Controller = Controller;
            state.Enter(); // reset
            state.Exit(); // reset
        }
        
        _currentState = StatesList.First();
        _currentState?.Enter();
        PreviousState = _currentState;
    }

    public void _Process(double delta)
    {
        TransitionTo(_currentState?.Update((float)delta));
    }
    
    public void _PhysicsProcess(double delta)
    {
        TransitionTo(_currentState?.PhysicsUpdate((float)delta));
    }
    
    public void _UnhandledInput(InputEvent @event)
    {
        TransitionTo(_currentState?.HandleInput(@event));
        @event.Dispose();
    }

    public void _AnimationEnd(string animationName)
    {
        TransitionTo(_currentState?.AnimationEnd(animationName));
    }
    
    public void TransitionTo(State state)
    {
        if (state == _currentState)
            return;
        LastTransition = DateTime.Now;
        
        PreviousState = _currentState;
        _currentState?.Exit();
        _currentState = state;
        _currentState?.Enter();
    }
}