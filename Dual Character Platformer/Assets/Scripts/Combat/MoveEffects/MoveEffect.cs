using UnityEngine;

public abstract class MoveEffect : ScriptableObject
{
    [SerializeField] private float totalDuration;
    [SerializeField] private float windUp;
    [SerializeField] private float comboWindowStart;
    [SerializeField] private float cooldown;
    
    public virtual void Execute(Collider2D[] targets) {}
    
    public bool IsWithinComboWindow(float currentTime)
    {
        return currentTime >= comboWindowStart;
    }

    public bool IsActionDone(float currentTime)
    {
        return currentTime >= totalDuration;
    }

    public bool IsWindingUp(float currentTime)
    {
        return currentTime < windUp;
    }
}
