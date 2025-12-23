using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : State
{
    private PlayerControls _playerControls;
    private CharacterActionManager _actionManager;

    public override void OnEnter(StateMachine stateMachine)
    {
        Debug.Log("Entering PlayerIdleState");
        base.OnEnter(stateMachine);
        
        _playerControls = stateMachine.GetComponent<PlayerControls>();
        _actionManager = GetComponentFromStateMachine<CharacterActionManager>();
        
        
        _playerControls.AttackAction.performed += OnAttackInput;
    }

    private void OnAttackInput(InputAction.CallbackContext _)
    {
        if (_actionManager.IsMeleeCharacter)
        {
            _stateMachine.SetState(new MeleeAttackState());
        }
        else
        {
            //Set ranged attack state
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _playerControls.AttackAction.performed -= OnAttackInput;
    }
}
