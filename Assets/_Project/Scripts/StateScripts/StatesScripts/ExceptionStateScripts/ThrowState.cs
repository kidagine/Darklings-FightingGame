using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : State
{
	private IdleState _idleState;

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound("Hit").Play();
		_playerAnimator.Throw();
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
		_player.CurrentAttack = _playerComboSystem.GetThrow();
	}

	private void ToIdleState()
	{
		if (_stateMachine.CurrentState == this)
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
