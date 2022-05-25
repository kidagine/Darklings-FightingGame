using System.Collections;
using UnityEngine;

public class HurtState : HurtParentState
{
	private Coroutine _stunCoroutine;
	private HurtState _hurtState;


	protected override void Awake()
	{
		base.Awake();
		_hurtState = GetComponent<HurtState>();
	}
	public override void Enter()
	{
		_playerAnimator.Hurt(true);
		_stunCoroutine = StartCoroutine(StunCoroutine(_hurtAttack.hitStun));
		base.Enter();
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
		_hurtState.Initialize(attack);
		_stateMachine.ChangeState(_hurtState);
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