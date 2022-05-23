using System.Collections;
using UnityEngine;

public class AirHurtState : HurtParentState
{
	private Coroutine _stunCoroutine;
	private AirHurtState _airHurtState;

	protected override void Awake()
	{
		_airHurtState = GetComponent<AirHurtState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.HurtAir(true);
		_stunCoroutine = StartCoroutine(StunCoroutine(_hurtAttack.hitStun));
	}

	IEnumerator StunCoroutine(float hitStun)
	{
		yield return new WaitForSeconds(hitStun);
		ToIdleState();
	}

	private void ToIdleState()
	{
		_stateMachine.ChangeState(_idleState);
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}

	public override bool ToHurtState(AttackSO attack)
	{
		_airHurtState.Initialize(attack);
		_stateMachine.ChangeState(_airHurtState);
		return true;
	}

	public override void Exit()
	{
		base.Exit();
		if (_stunCoroutine != null)
		{
			StopCoroutine(_stunCoroutine);
		}
	}
}