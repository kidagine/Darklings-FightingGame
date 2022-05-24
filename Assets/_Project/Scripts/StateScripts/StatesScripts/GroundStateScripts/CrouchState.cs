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
        _player.Flip();
    }

    private void ToIdleState()
    {
        if (_playerController.StandUp())
        {
            _stateMachine.ChangeState(_idleState);
        }
    }

    public override bool ToAttackState(InputEnum inputEnum)
    {
        _attackState.Initialize(inputEnum, true, false);
        _stateMachine.ChangeState(_attackState);
        return true;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}