using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;


    public bool DisableThoughtsInput { private get; set; }
    public bool DisableCodexInput { private get; set; }


    void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        _playerMovement.MovementInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           _playerMovement.JumpAction();
        }
        if (context.canceled)
        {
           // _playerMovement.JumpStopAction();
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerMovement.CrouchAction();
        }
        if (context.canceled)
        {
            _playerMovement.StandUpAction();
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player.AttackAction();
        }
    }

    public void ActivateInput()
    {
        _playerInput.ActivateInput();
    }

    public void DeactivateInput()
    {
        _playerInput.DeactivateInput();
    }
}
