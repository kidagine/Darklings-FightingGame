using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
	private IdleState _idleState;
	public InputEnum InputEnum { get; set; }
	public bool Crouch { get; set; }
	public static bool CanSkipAttack;
	void Awake()
	{
		_idleState = GetComponent<IdleState>();
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound("Hit").Play();
		_player.CurrentAttack = _playerComboSystem.GetComboAttack(InputEnum, Crouch);
		_playerAnimator.Attack(_player.CurrentAttack.name, true);
		_playerMovement.TravelDistance(new Vector2(_player.CurrentAttack.travelDistance * transform.root.localScale.x, _player.CurrentAttack.travelDirection.y));
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
	}

	public override bool ToAttackState(InputEnum inputEnum)
	{
		if (CanSkipAttack)
		{
			_stateMachine.ChangeState(this);
			return true;
		}
		else
		{
			return false;
		}
	}

	public override void Exit()
	{
		base.Exit();
		CanSkipAttack = false;
	}
}
