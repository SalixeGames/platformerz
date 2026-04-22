using System;
using System.Linq;
using Godot;
using Godot.Collections;

namespace Platformerz.scripts.enemies;

[GlobalClass]
public partial class EnStateMachine : Resource
{
    public Array<EnState> EnStatesList = new Array<EnState>();
    
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
        
        visionArea.AreaEntered += OnPlayerInVision;
        visionArea.AreaExited += OnPlayerOutVision;
        controller.Animator.AnimationFinished += _AnimationEnd;
        EnStates = new System.Collections.Generic.Dictionary<string, EnState>();
        EnStatesList.Add(new MoveState());
        EnStatesList.Add(new FleeState());
        EnStatesList.Add(new KnockBackState());
        EnStatesList.Add(new FreezeState());
        
        foreach (EnState enState in EnStatesList) {
            enState.Ready(this);
            EnStates.Add(enState.Name, enState);
            EnStates[enState.Name] = enState;
            enState.Controller = Controller;
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

    public void _AnimationEnd(StringName animationName)
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
    
    public void OnPlayerInVision(Area2D visionArea)
    {
        TransitionTo(CurrentState?.OnPlayerInVision(visionArea));
    }

    public void OnPlayerOutVision(Area2D visionArea)
    {
        TransitionTo(CurrentState?.OnPlayerOutVision(visionArea));
    }

    public void OnHurt(float damage)
    {
        TransitionTo(CurrentState?.OnHurt(damage));
    }
}