using UnityEngine;

public class CrouchState : GroundParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Crouch();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToIdleState();
        _player.CheckFlip();
    }

    private void ToIdleState()
    {
        if (_baseController.StandUp())
        {
            _stateMachine.ChangeState(_idleState);
        }
    }

    public override bool ToBlockState(AttackSO attack)
    {
        _blockLowState.Initialize(attack);
        _stateMachine.ChangeState(_blockLowState);
        return true;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}