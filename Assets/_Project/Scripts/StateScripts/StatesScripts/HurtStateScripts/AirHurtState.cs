using System.Collections;
using UnityEngine;

public class AirHurtState : HurtParentState
{
	private Coroutine _stunCoroutine;
	private FallState _fallState;
	private HurtState _hurtState;

	protected override void Awake()
	{
		base.Awake();
		_fallState = GetComponent<FallState>();
		_hurtState = GetComponent<HurtState>();
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
		_player.OtherPlayer.StopComboTimer();
		_stateMachine.ChangeState(_fallState);
	}

	private void ToFallStateAfterGround()
	{
		if (_playerMovement.IsGrounded && _rigidbody.velocity.y <= 0.0f)
		{
			_hurtState.Initialize(_hurtAttack, true);
			_stateMachine.ChangeState(_hurtState);
		}
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
	}

	public override bool ToHurtState(AttackSO attack)
	{
		this.Initialize(attack);
		_stateMachine.ChangeState(this);
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