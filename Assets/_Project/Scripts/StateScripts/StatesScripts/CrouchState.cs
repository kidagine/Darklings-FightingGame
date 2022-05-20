using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : State
{
    private IdleState _idleState;
    private AttackState _attackState;
    private ArcanaState _arcanaState;


    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _attackState = GetComponent<AttackState>();
        _arcanaState = GetComponent<ArcanaState>();
    }

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
        _attackState.InputEnum = inputEnum;
        _attackState.Initialize(true, false);
        _stateMachine.ChangeState(_attackState);
        return true;
    }

    public override bool ToArcanaState()
    {
        if (_player.Arcana >= 1.0f)
        {
            _stateMachine.ChangeState(_arcanaState);
            return true;
        }
        return false;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}