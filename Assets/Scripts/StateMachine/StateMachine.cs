using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
    [NaughtyAttributes.ReadOnly]
    public string currentStateName;
    public Dictionary<Type, BaseState> states;

    [SerializeField]
    public BaseState CurrentState { get; private set; }
    public BaseState PreviousState { get; private set; }

    public UnityAction<BaseState> OnStateChanged;
    public UnityAction OnStateOver;

    public void InitStates(Dictionary<Type, BaseState> _states)
    {
        states = _states;
    }

    private void Update()
    {
        if (CurrentState == null)
        {
            CurrentState = states.Values.First();
            currentStateName = CurrentState.ToString();
            CurrentState.Enter();
        }
        CurrentState?.Tick();
    }

    public void SwitchToNewState(Type _nextState)
    {
        CurrentState.Exit();
        PreviousState = CurrentState;
        foreach (var item in states)
            if (item.Key == _nextState)
                CurrentState = item.Value;
        currentStateName = CurrentState.ToString();
        OnStateChanged?.Invoke(CurrentState);
        CurrentState.Enter();
    }

    public bool IsThisState(Type _state)
    {
        BaseState compareState = null;
        foreach (var item in states)
            if (item.Key == _state)
                compareState = item.Value;
        return compareState == CurrentState;
    }

    public void StateOver()
    {
        OnStateOver?.Invoke();
    }
}

