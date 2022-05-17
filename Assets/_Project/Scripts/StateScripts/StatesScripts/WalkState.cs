using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    private FallState _idleState;
    private CrouchState _crouchState;
    private JumpForwardState _jumpForwardState;

    void Awake()
    {
        _idleState = GetComponent<FallState>();
        _crouchState = GetComponent<CrouchState>();
        _jumpForwardState = GetComponent<JumpForwardState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Walk();
        _playerMovement.MovementSpeed = _playerStats.PlayerStatsSO.walkSpeed;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToIdleState();
        ToCrouchState();
        ToJumpState();
    }

    private void ToIdleState()
    {
        if (_playerController.InputDirection.x == 0.0f)
        {
            _stateMachine.ChangeState(_idleState);
        }
    }

    public void ToCrouchState()
    {
        if (_playerController.Crouch())
        {
            _stateMachine.ChangeState(_crouchState);
        }
    }

    private void ToJumpState()
    {
        if (_playerController.InputDirection.y > 0.0f && !_playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = true;
            _stateMachine.ChangeState(_jumpForwardState);
        }
        else if (_playerController.InputDirection.y <= 0.0f && _playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = false;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = new Vector2(_playerController.InputDirection.x * _playerMovement.MovementSpeed, _rigidbody.velocity.y);
    }
}
