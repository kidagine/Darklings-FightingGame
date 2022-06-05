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
		if (_brainController.ControllerInputName == ControllerTypeEnum.Cpu.ToString() && TrainingSettings.OnHit)
		{
			ToAttackState();
		}
		else
		{
			ToIdleState();
		}
	}

	private void ToIdleState()
	{
		_stateMachine.ChangeState(_idleState);
	}

	private void ToAttackState()
	{
		_attackState.Initialize(InputEnum.Light, false, false);	
		_stateMachine.ChangeState(_attackState);
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
			_player.OtherPlayerUI.ResetCombo();
			StopCoroutine(_stunCoroutine);
		}
	}
}