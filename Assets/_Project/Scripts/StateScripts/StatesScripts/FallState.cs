using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : State
{
    [SerializeField] private GameObject _groundedPrefab = default;
    private IdleState _idleState;
    private JumpForwardState _jumpForwardState;
    private AirDashState _airDashState;
    private AttackState _attackState;
    private ArcanaState _arcanaState;

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _jumpForwardState = GetComponent<JumpForwardState>();
        _airDashState = GetComponent<AirDashState>();
        _attackState = GetComponent<AttackState>();
        _arcanaState = GetComponent<ArcanaState>();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToIdleState();
        ToJumpState();
        ToJumpForwardState();
        ToAirDashState();
    }

    public void ToIdleState()
    {
        if (_playerMovement._isGrounded && _rigidbody.velocity.y <= 0.0f)
        {
            Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
            _audio.Sound("Landed").Play();
            _player.SetPushboxTrigger(false);
            _stateMachine.ChangeState(_idleState);
        }
    }

    public void ToJumpState()
    {
        if (_playerStats.PlayerStatsSO.canDoubleJump && !_playerMovement.HasDoubleJumped)
        {
            if (_playerController.InputDirection.x == 0.0f)
            {
                if (_playerController.InputDirection.y > 0.0f && !_playerMovement.HasJumped)
                {
                    _playerMovement.HasDoubleJumped = true;
                    _playerMovement.HasJumped = true;
                    _stateMachine.ChangeState(this);
                }
                else if (_playerController.InputDirection.y <= 0.0f && _playerMovement.HasJumped)
                {
                    _playerMovement.HasJumped = false;
                }
            }
        }
    }

    public void ToJumpForwardState()
    {
        if (_playerStats.PlayerStatsSO.canDoubleJump && !_playerMovement.HasDoubleJumped)
        {
            if (_playerController.InputDirection.x != 0.0f)
            {
                if (_playerController.InputDirection.y > 0.0f && !_playerMovement.HasJumped)
                {
                    _playerMovement.HasDoubleJumped = true;
                    _playerMovement.HasJumped = true;
                    _stateMachine.ChangeState(_jumpForwardState);
                }
                else if (_playerController.InputDirection.y <= 0.0f && _playerMovement.HasJumped)
                {
                    _playerMovement.HasJumped = false;
                }
            }
        }
    }

    private void ToAirDashState()
    {
        if (!_playerMovement.HasAirDashed)
        {
            if (_playerController.DashForward())
            {
                _airDashState.DashDirection = 1;
                _stateMachine.ChangeState(_airDashState);
            }
            else if (_playerController.DashBackward())
            {
                _airDashState.DashDirection = -1;
                _stateMachine.ChangeState(_airDashState);
            }
        }
    }

    public override bool ToAttackState(InputEnum inputEnum)
    {
        _attackState.InputEnum = inputEnum;
        _attackState.Initialize(false, true);
        _stateMachine.ChangeState(_attackState);
        return true;
    }

    public override bool ToArcanaState()
    {
        if (_player.Arcana >= 1.0f && _playerComboSystem.GetArcana().airOk)
        {
            _stateMachine.ChangeState(_arcanaState);
            return true;
        }
        return false;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        _playerMovement.ResetGravity();
    }
}
