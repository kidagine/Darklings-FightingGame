using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private WalkState _walkState;
    private CrouchState _crouchState;
    private JumpState _jumpState;
    private DashState _dashState;
    private AttackState _attackState;

    void Awake()
    {
        _walkState = GetComponent<WalkState>();
        _crouchState = GetComponent<CrouchState>();
        _jumpState = GetComponent<JumpState>();
        _dashState = GetComponent<DashState>();
        _attackState = GetComponent<AttackState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Idle();
        _player.CanFlip = true;
        _playerMovement.HasAirDashed = false;
        _playerMovement.HasDoubleJumped = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToWalkState();
        ToCrouchState();
        ToJumpState();
        ToDashState();
        ToAttackState();
    }

    private void ToWalkState()
    {
        if (_playerController.InputDirection.x != 0.0f)
        {
            _stateMachine.ChangeState(_walkState);
        }
    }

    private void ToCrouchState()
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
            _stateMachine.ChangeState(_jumpState);
        }
        else if (_playerController.InputDirection.y <= 0.0f && _playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = false;
        }
    }

    private void ToDashState()
    {
        if (_playerController.DashForward())
        {
            _dashState.DashDirection = 1;
            _stateMachine.ChangeState(_dashState);
        }
        else if (_playerController.DashBackward())
        {
            _dashState.DashDirection = -1;
            _stateMachine.ChangeState(_dashState);
        }
    }

    private void ToAttackState()
    {
        // if (_playerController)
        // {
        //     _stateMachine.ChangeState(_attackState);
        // }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Debug.Log(collision.name);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
    }
}
