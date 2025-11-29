using Godot;
using System;
using System.Linq;
using Godot.Collections;

[GlobalClass]
public partial  class EnemyStateMachine : StateMachine
{
    
    public new BaseEnemy Controller;

    public void _Ready(BaseEnemy controller)
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
}