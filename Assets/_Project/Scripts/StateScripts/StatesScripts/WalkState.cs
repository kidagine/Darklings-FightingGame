using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    private IdleState _idleState;
    private CrouchState _crouchState;
    private JumpForwardState _jumpForwardState;
    private AttackState _attackState;

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _crouchState = GetComponent<CrouchState>();
        _jumpForwardState = GetComponent<JumpForwardState>();
        _attackState = GetComponent<AttackState>();
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
        ToJumpForwardState();
        _player.Flip();
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

    private void ToJumpForwardState()
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

    public override bool ToAttackState(InputEnum inputEnum)
    {
        _attackState.InputEnum = inputEnum;
        _attackState.Initialize(false, false);
        _stateMachine.ChangeState(_attackState);
        return true;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = new Vector2(_playerController.InputDirection.x * _playerMovement.MovementSpeed, _rigidbody.velocity.y);
    }
}
