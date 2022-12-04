using UnityEngine;

public class WalkState : GroundParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Walk();
        _playerMovement.MovementSpeed = _player.playerStats.SpeedWalk;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _physics.Velocity = new DemonicsVector2((DemonicsFloat)_baseController.InputDirection.x * (DemonicsFloat)_playerMovement.MovementSpeed, (DemonicsFloat)0);
        ToIdleState();
        ToCrouchState();
        ToJumpForwardState();
        _player.CheckFlip();
    }

    private new void ToIdleState()
    {
        if (_baseController.InputDirection.x == 0.0f)
        {
            _stateMachine.ChangeState(_idleState);
        }
    }

    public void ToCrouchState()
    {
        if (_baseController.Crouch())
        {
            _stateMachine.ChangeState(_crouchState);
        }
    }

    public override bool ToParryState()
    {
        _stateMachine.ChangeState(_parryState);
        return true;
    }

    public override bool ToDashState(int direction)
    {
        _dashState.DashDirection = direction;
        _stateMachine.ChangeState(_dashState);
        return true;
    }

    private void ToJumpForwardState()
    {
        if (_baseController.Jump() && !_playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = true;
            if (_baseController.InputDirection.x != 0)
            {
                _stateMachine.ChangeState(_jumpForwardState);
            }
            else
            {
                _stateMachine.ChangeState(_jumpState);
            }
        }
        else if (_playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = false;
        }
    }

    public override bool ToBlockState(AttackSO attack)
    {
        _blockState.Initialize(attack);
        _stateMachine.ChangeState(_blockState);
        return true;
    }
}
