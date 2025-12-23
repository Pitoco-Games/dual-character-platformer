using System.Collections.Generic;
using UnityEngine;

public class CharacterActionManager : MonoBehaviour
{
    [SerializeField] private List<BaseAttackMoveEffect> _comboSequence;
    [SerializeField] private Transform _attackPositionTransform;
    [SerializeField] private LayerMask _layersToIgnore;
    [SerializeField] private bool _isMeleeCharacter;
    
    
    private int _comboIndex = 0;
    public BaseAttackMoveEffect CurrentAttackMoveEffect => _comboSequence[_comboIndex];
    public bool IsMeleeCharacter => _isMeleeCharacter; 
        
    public void DoMeleeAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPositionTransform.position, CurrentAttackMoveEffect.AttackSize, ~_layersToIgnore);
        
        CurrentAttackMoveEffect.Execute(hits);
        
        _comboIndex++;
        if (_comboIndex == _comboSequence.Count)
        {
            _comboIndex = 0;
        }
    }

    public void ResetCombo()
    {
        _comboIndex = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPositionTransform.position,
            CurrentAttackMoveEffect.AttackSize);
    }
}
