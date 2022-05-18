using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    void Start()
    {
        CurrentState = GetInitialState();
        if (CurrentState != null)
        {
            CurrentState.Enter();
        }
    }

    void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateLogic();
        }
    }

    void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdatePhysics();
        }
    }

    public virtual void ChangeState(State newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        CurrentState = newState;
        OnStateChange();
        CurrentState.Enter();
    }

    protected virtual void OnStateChange() { }

    protected virtual State GetInitialState()
    {
        return null;
    }
}