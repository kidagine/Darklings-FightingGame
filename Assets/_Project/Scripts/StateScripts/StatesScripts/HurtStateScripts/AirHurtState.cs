using System.Collections;
using UnityEngine;

public class AirHurtState : HurtParentState
{
	private Coroutine _stunCoroutine;
	private AirHurtState _airHurtState;
	private FallState _fallState;

	protected override void Awake()
	{
		base.Awake();
		_airHurtState = GetComponent<AirHurtState>();
		_fallState = GetComponent<FallState>();
	}

	public override void Enter()
	{
		_playerAnimator.HurtAir(true);
		_stunCoroutine = StartCoroutine(StunCoroutine(_hurtAttack.hitStun));
		base.Enter();
	}

	IEnumerator StunCoroutine(float hitStun)
	{
		yield return new WaitForSeconds(hitStun);
		ToFallAfterStunState();
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		ToFallStateAfterGround();
	}


	private void ToFallAfterStunState()
	{
		if (GameManager.Instance.InfiniteHealth)
		{
			_player.MaxHealthStats();
		}
		_playerAnimator.Jump();
		_player.OtherPlayerUI.ResetCombo();
		_stateMachine.ChangeState(_fallState);
	}

	private void ToFallStateAfterGround()
	{
		if (_playerMovement.IsGrounded && _rigidbody.velocity.y <= 0.0f)
		{
			_player.OtherPlayerUI.ResetCombo();
			_stateMachine.ChangeState(_fallState);
		}
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y / 1.1f);
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