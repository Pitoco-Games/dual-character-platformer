using System;
using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine;
    public event Action OnStateExit;

    public virtual void OnEnter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnLateUpdate() { }

    public virtual void OnExit()
    {
        OnStateExit?.Invoke();
    }

    protected T GetComponentFromStateMachine<T>() where T : Component
    {
        return _stateMachine.GetComponent<T>();
    }
}
