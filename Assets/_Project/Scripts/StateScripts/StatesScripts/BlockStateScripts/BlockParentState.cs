using Demonics.Manager;
using FixMath.NET;
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
	private readonly int _chipDamage = 250;
	private bool _skip;
	protected int _blockFrame;

	public void Initialize(AttackSO attack, bool skip = false)
	{
		_skip = skip;
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
			_player.OtherPlayer.transform.localScale.x, 0), new Vector2(_blockAttack.knockback, 0), _blockAttack.knockbackDuration);

		if (!_skip)
		{
			GameObject effect = ObjectPoolingManager.Instance.Spawn(_blockEffectPrefab);
			effect.transform.localPosition = _blockAttack.hurtEffectPosition;
			GameManager.Instance.HitStop(_blockAttack.hitstop);
		}
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
		if (_player.AssistGauge >= (Fix64)1)
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
		_grabbedState.Initialize(true);
		_stateMachine.ChangeState(_grabbedState);
		return true;
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		_rigidbody.velocity = Vector2.zero;
	}

	public override void Exit()
	{
		base.Exit();
		_blockFrame = 0;
	}
}
