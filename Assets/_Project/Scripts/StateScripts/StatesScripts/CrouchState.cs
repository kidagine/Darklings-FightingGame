using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : State
{
    private IdleState _idleState;


    void Awake()
    {
        _idleState = GetComponent<IdleState>();
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

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}