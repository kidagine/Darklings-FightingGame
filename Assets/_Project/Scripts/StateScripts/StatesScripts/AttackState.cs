using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private IdleState _idleState;


    void Awake()
    {
        _idleState = GetComponent<IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Attack5L();
        _player.CurrentAttack = _playerComboSystem.GetComboAttack(InputEnum.Light);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public void ToIdleState()
    {
        if (_playerMovement._isGrounded)
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
