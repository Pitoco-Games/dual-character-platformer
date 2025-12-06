using System;
using Movement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    
    private InputAction _movementAction;
    private InputAction _jumpAction;
    private bool _isJumping;

    private void Awake()
    {
        _movementAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");

        _jumpAction.performed += StartJumping;
        _jumpAction.canceled += StopJumping;
    }

    private void Update()
    {
        float horizontalMoveDir = _movementAction.ReadValue<Vector2>().x;
        _movement.Move(horizontalMoveDir);
    }
    
    private void StartJumping(InputAction.CallbackContext _)
    {
        _isJumping = true;
        _movement.StartJump();
    }
    
    private void StopJumping(InputAction.CallbackContext _)
    {
        _isJumping = false;
        _movement.StopJumping();
    }
}
