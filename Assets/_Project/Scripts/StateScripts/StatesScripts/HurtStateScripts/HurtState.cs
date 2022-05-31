using System.Collections;
using UnityEngine;

public class HurtState : HurtParentState
{
	private Coroutine _stunCoroutine;
	private HurtState _hurtState;
	private GrabbedState _grabbedState;

	protected override void Awake()
	{
		base.Awake();
		_hurtState = GetComponent<HurtState>();
		_grabbedState = GetComponent<GrabbedState>();
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
		_player.OtherPlayerUI.ResetCombo();
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

	public override bool ToGrabbedState()
	{
		_stateMachine.ChangeState(_grabbedState);
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