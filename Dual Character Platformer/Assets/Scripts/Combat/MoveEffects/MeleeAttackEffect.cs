using UnityEngine;

[CreateAssetMenu(fileName = "Character_MeleeAttack_x", menuName = "ScriptableObjects/MoveEffects/MeleeAttack")]
public class MeleeAttackEffect : BaseAttackMoveEffect
{
    public override void Execute(Collider2D[] targets)
    {
        Debug.Log($"Melee attack! Dealt {Damage} damage");
        foreach (Collider2D target in targets)
        {
            target.GetComponent<Health>().ChangeHealth(-Damage);
        }
    }
}
