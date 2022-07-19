using Demonics.Manager;
using UnityEngine;

public class BlockParentState : State
{
	[SerializeField] private GameObject _blockEffectPrefab = default;
	protected IdleState _idleState;
	protected CrouchState _crouchState;
	protected FallState _fallState;
	protected AttackState _attackState;
	protected GrabbedState _grabbedState;
	protected ShadowbreakState _shadowbreakState;
	protected HurtState _hurtState;
	protected AttackSO _blockAttack;
	protected Coroutine _blockCoroutine;
	private readonly float _chipDamage = 250;

	public void Initialize(AttackSO attack)
	{
		_blockAttack = attack;
	}

	protected virtual void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_crouchState = GetComponent<CrouchState>();
		_fallState = GetComponent<FallState>();
		_attackState = GetComponent<AttackState>();
		_grabbedState = GetComponent<GrabbedState>();
		_hurtState = GetComponent<HurtState>();
		_shadowbreakState = GetComponent<ShadowbreakState>();
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound("Block").Play();
		_playerMovement.Knockback(new Vector2(
			_player.OtherPlayer.transform.localScale.x, 0.0f), _blockAttack.knockback, _blockAttack.knockbackDuration);
		GameObject effect = ObjectPoolingManager.Instance.Spawn(_blockEffectPrefab);
		effect.transform.localPosition = _blockAttack.hurtEffectPosition;
		if (_blockAttack.isArcana)
		{
			_player.Health -= _chipDamage;
			_playerUI.SetHealth(_player.Health);
			_playerUI.Damaged();
			_playerUI.UpdateHealthDamaged();
		}
	}


	public override bool ToBlockState(AttackSO attack)
	{
		this.Initialize(attack);
		_stateMachine.ChangeState(this);
		return true;
	}

	public override bool AssistCall()
	{
		if (_player.AssistGauge >= 1.0f)
		{
			_stateMachine.ChangeState(_shadowbreakState);
			return true;
		}
		return false;
	}

	public override bool ToHurtState(AttackSO attack)
	{
		if (attack.attackTypeEnum == AttackTypeEnum.Break)
		{
			_hurtState.Initialize(attack);
			_stateMachine.ChangeState(_hurtState);
			return true;
		}
		return false;
	}

	public override bool ToGrabbedState()
	{
		_stateMachine.ChangeState(_grabbedState);
		return true;
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}

	public override void Exit()
	{
		base.Exit();
		if (_blockCoroutine != null)
		{
			StopCoroutine(_blockCoroutine);
		}
	}
}
