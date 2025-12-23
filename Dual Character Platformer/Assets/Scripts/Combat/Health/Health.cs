using UnityEngine;
using System;

public abstract class Health : MonoBehaviour
{    
    [SerializeField] protected GameObject parentToDestroy;
    [SerializeField] protected int maxHP;
    protected int curHP;
    
    public event Action<int, int> OnHealthChange;

    protected virtual void Start()
    {
        curHP = maxHP;
    }

    public virtual void ChangeHealth(int amount)
    {
        curHP += amount;
        OnHealthChange?.Invoke(curHP, amount);
    }
}
