using System.Collections;
using UnityEngine;

public class KnockdownState : State
{
	private WakeUpState _wakeUpState;
	private Coroutine _knockdownCoroutine;

	void Awake()
	{
		_wakeUpState = GetComponent<WakeUpState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Knockdown();
		_player.SetHurtbox(false);
		_knockdownCoroutine = StartCoroutine(ToWakeUpStateCoroutine());
	}

	IEnumerator ToWakeUpStateCoroutine()
	{
		yield return new WaitForSeconds(1.0f);
		_stateMachine.ChangeState(_wakeUpState);
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