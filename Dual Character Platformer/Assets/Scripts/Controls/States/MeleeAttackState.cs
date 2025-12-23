using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttackState : AttackingBaseState
{
    private PlayerControls _playerControls;
    private CharacterActionManager _actionManager;

    public override void OnEnter(StateMachine stateMachine)
    {
        Debug.Log("Entering MeleeAttackState");
        base.OnEnter(stateMachine);
        _playerControls = GetComponentFromStateMachine<PlayerControls>();
        _actionManager = GetComponentFromStateMachine<CharacterActionManager>();
        
        _playerControls.AttackAction.performed += OnAttackInput;

        _actionManager.DoMeleeAttack();
    }

    private void OnAttackInput(InputAction.CallbackContext _)
    {
        if (!_actionManager.CurrentAttackMoveEffect.IsActionDone(base.CurrentDuration))
        {
            WillCombo = true;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        if (_actionManager.CurrentAttackMoveEffect.IsWindingUp(base.CurrentDuration))
        {
            return;
        }

        if (_actionManager.CurrentAttackMoveEffect.IsWithinComboWindow(base.CurrentDuration)
            && WillCombo)
        {
            _actionManager.DoMeleeAttack();
            WillCombo = false;
            base.CurrentDuration = 0;
        }

        if (_actionManager.CurrentAttackMoveEffect.IsActionDone(base.CurrentDuration))
        {
            _actionManager.ResetCombo();
            _stateMachine.SetStateToMain();
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
        _playerControls.AttackAction.performed -= OnAttackInput;
    }
}
