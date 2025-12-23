using UnityEngine;

public abstract class AttackingBaseState : State
{
    protected bool WillCombo;
    protected float CurrentDuration;
    
    public override void OnUpdate()
    {
        CurrentDuration += Time.deltaTime;
    }
}
