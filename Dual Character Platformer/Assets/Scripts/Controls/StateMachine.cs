using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public string customName;

    private State mainState = new PlayerIdleState();

    public State CurrentState { get; private set; }

    private void Awake()
    {
        SetStateToMain();
    }

    void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }
    
    private void LateUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.OnLateUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.OnFixedUpdate();
        }
    }
    
    public void SetState(State _newState)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }
        CurrentState = _newState;
        CurrentState.OnEnter(this);
    }
    
    public void SetStateToMain()
    {
        SetState(mainState);
    }
}
