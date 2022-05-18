using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : State
{
    private IdleState _idleState;
    private AttackState _attackState;


    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _attackState = GetComponent<AttackState>();
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
        _attackState.Crouch = true;
        _stateMachine.ChangeState(_attackState);
        return true;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}