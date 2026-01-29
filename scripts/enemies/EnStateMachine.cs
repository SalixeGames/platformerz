using System;
using System.Linq;
using Godot;
using Godot.Collections;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class EnStateMachine : Resource
{
    [Export]
    public Array<EnState> EnStatesList;
    
    public System.Collections.Generic.Dictionary<string, EnState> EnStates;
    public EnState CurrentState;
    public EnState PreviousState;
    public EnState PreAttackState;
    public DateTime LastTransition;
    public BaseEnemy Controller;

    public EnStateMachine()
    {
    }

    public void _Ready(BaseEnemy controller, Area2D visionArea)
    {
        Controller = controller;
        
        EnStates = new System.Collections.Generic.Dictionary<string, EnState>();
        if (EnStatesList?.Count == null)
        {
            EnStatesList = new Array<EnState>();
            // EnStatesList.Add(new IdleState());
            // EnStatesList.Add(new WalkState());
        }
        
        foreach (EnState enState in EnStatesList) {
            enState.Ready(this);
            EnStates.Add(enState.Name, enState);
            EnStates[enState.Name] = enState;
            enState.Controller = Controller;
            visionArea.AreaEntered += enState.OnPlayerInVision;
            enState.Enter(); // reset
            enState.Exit(); // reset
        }
        
        CurrentState = EnStatesList.First();
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

    public void _AnimationEnd(string animationName)
    {
        TransitionTo(CurrentState?.AnimationEnd(animationName));
    }
    
    public void TransitionTo(EnState enState)
    {
        if (enState == CurrentState)
            return;
        
        PreviousState = CurrentState;
        CurrentState?.Exit();
        CurrentState = enState;
        CurrentState?.Enter();
    }
}