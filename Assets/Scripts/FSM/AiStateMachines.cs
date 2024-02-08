using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachines
{
    private Dictionary<string, AIState> states = new Dictionary<string, AIState>();

    public AIState CurrentState { get; private set; }

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void AddState(string name, AIState state)
    {
        //Debug.Assert(states.ContainsKey(name), $"State machine already contains state: {name}.");
        states[name] = state;
    }

    public void SetState(string name)
    {
        //Debug.Assert(states.ContainsKey(name), $"State machine does not contain state: {name}.");

        AIState newState = states[name];

        if (newState == CurrentState) return;

        //Exit parent state
        CurrentState?.OnExit();
        //Set new state
        CurrentState = newState;
        // enter new state
        CurrentState?.OnEnter();
    }
}

