using UnityEngine;

public abstract class BaseAttackMoveEffect : MoveEffect
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSize;
    
    public int Damage => _damage;
    public float AttackSize => _attackSize;
}
