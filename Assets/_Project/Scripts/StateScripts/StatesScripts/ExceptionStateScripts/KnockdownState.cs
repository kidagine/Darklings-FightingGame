using System.Collections;
using UnityEngine;

public class KnockdownState : State
{
	private WakeUpState _wakeUpState;
	private DeathState _deathState;
	private Coroutine _knockdownCoroutine;

	void Awake()
	{
		_wakeUpState = GetComponent<WakeUpState>();
		_deathState = GetComponent<DeathState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Knockdown();
		_player.SetHurtbox(false);
		_knockdownCoroutine = StartCoroutine(ToWakeUpStateCoroutine());
		if (_player.Health <= 0)
		{
			ToDeathState();
		}
	}

	IEnumerator ToWakeUpStateCoroutine()
	{
		yield return new WaitForSeconds(1.0f);
		ToWakeUpState();
	}

	private void ToWakeUpState()
	{
		_stateMachine.ChangeState(_wakeUpState);
	}

	private void ToDeathState()
	{
		_stateMachine.ChangeState(_deathState);
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
	}

	public override void Exit()
	{
		base.Exit();
		if (_knockdownCoroutine != null)
		{
			StopCoroutine(_knockdownCoroutine);
		}
	}
}