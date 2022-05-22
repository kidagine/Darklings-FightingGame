using UnityEngine;

public class AirParentState : State
{
    [SerializeField] protected GameObject _jumpPrefab = default;
    [SerializeField] protected GameObject _groundedPrefab = default;
    protected FallState _fallState;
    protected JumpState _jumpState;
    protected JumpForwardState _jumpForwardState;
    protected AirDashState _airDashState;
    protected AttackState _attackState;
    protected ArcanaState _arcanaState;

    protected virtual void Awake()
    {
            _fallState = GetComponent<FallState>();
        _jumpState = GetComponent<JumpState>();
        _jumpForwardState = GetComponent<JumpForwardState>();
        _airDashState = GetComponent<AirDashState>();
        _attackState = GetComponent<AttackState>();
        _arcanaState = GetComponent<ArcanaState>();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToFallState();
        ToJumpState();
        ToJumpForwardState();
        ToAirDashState();
    }

    public void ToFallState()
    {
        if (_rigidbody.velocity.y <= 0.0f)
        {
            _stateMachine.ChangeState(_fallState);
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
                    _stateMachine.ChangeState(_jumpState);
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
}
