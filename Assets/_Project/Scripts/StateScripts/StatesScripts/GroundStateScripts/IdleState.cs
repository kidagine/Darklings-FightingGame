using UnityEngine;

public class IdleState : GroundParentState
{
    public override void Enter()
    {
        base.Enter();
        if (GameManager.Instance.InfiniteHealth)
        {
            _player.MaxHealthStats();
        }
        _playerAnimator.Idle();
        _player.SetPushboxTrigger(false);
        _playerMovement.HasAirDashed = false;
        _playerMovement.HasDoubleJumped = false;
        _player.CanAirArcana = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToWalkState();
        ToCrouchState();
        ToJumpState();
        _player.CheckFlip();
    }

    private void ToWalkState()
    {
        if (_baseController.InputDirection.x != 0)
        {
            _stateMachine.ChangeState(_walkState);
        }
    }

    private void ToCrouchState()
    {
        if (_baseController.Crouch())
        {
            _stateMachine.ChangeState(_crouchState);
        }
    }

    private void ToJumpState()
    {
        if (_baseController.Jump() && !_playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = true;
            _stateMachine.ChangeState(_jumpState);
        }
        else if (_playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = false;
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

    public override bool ToBlockState(AttackSO attack)
    {
        _blockState.Initialize(attack);
        _stateMachine.ChangeState(_blockState);
        return true;
    }
}
