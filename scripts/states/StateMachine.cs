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
    public State CurrentState;
    public State PreviousState;
    public State PreAttackState;
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
        
        CurrentState = StatesList.First();
        CurrentState?.Enter();
        PreviousState = CurrentState;
        PreAttackState =  CurrentState;
    }

    public void _Process(double delta)
    {
        TransitionTo(CurrentState?.Update((float)delta));
    }
    
    public void _PhysicsProcess(double delta)
    {
        TransitionTo(CurrentState?.PhysicsUpdate((float)delta));
    }
    
    public void _UnhandledInput(InputEvent @event)
    {
        TransitionTo(CurrentState?.HandleInput(@event));
        @event.Dispose();
    }

    public void _AnimationEnd(string animationName)
    {
        TransitionTo(CurrentState?.AnimationEnd(animationName));
    }
    
    public void TransitionTo(State state)
    {
        if (state == CurrentState)
            return;
        if (CurrentState.Name == "wall_slide" || CurrentState.Name == "attack")
            LastTransition = DateTime.Now;
        if (!CurrentState.Name.Contains("attack")) PreAttackState = CurrentState;
        
        PreviousState = CurrentState;
        CurrentState?.Exit();
        CurrentState = state;
        CurrentState?.Enter();
    }
}