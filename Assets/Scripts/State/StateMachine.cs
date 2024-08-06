using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public IState<T> currentState { get; set; }
    public IState<T> previousState { get; set; }
    private T t;
    public StateMachine(T t)
    {
        this.t = t;
    }
    public void ChangeState(IState<T> newState)
    {
        if (currentState != null)
        {
            if (newState == currentState)
            {
                return;
            }
            previousState = currentState;
            currentState.OnExit(t);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(t);
        }

    }
    public void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(t);
        }
    }
}
