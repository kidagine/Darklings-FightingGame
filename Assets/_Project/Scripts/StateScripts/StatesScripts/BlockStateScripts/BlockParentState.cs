using Demonics.Manager;
using UnityEngine;

public class BlockParentState : State
{
	[SerializeField] private GameObject _blockEffectPrefab = default;
	protected IdleState _idleState;
	protected CrouchState _crouchState;
	protected FallState _fallState;
	protected AttackSO _blockAttack;
	protected Coroutine _blockCoroutine;

	public void Initialize(AttackSO attack)
	{
		_blockAttack = attack;
	}

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_crouchState = GetComponent<CrouchState>();
		_fallState = GetComponent<FallState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerMovement.Knockback(new Vector2(
			_player.OtherPlayer.transform.localScale.x, 0.0f), _blockAttack.knockback, _blockAttack.knockbackDuration);
		GameObject effect = ObjectPoolingManager.Instance.Spawn(_blockEffectPrefab);
		effect.transform.localPosition = _blockAttack.hurtEffectPosition;
	}


	public override bool ToBlockState(AttackSO attack)
	{
		this.Initialize(attack);
		_stateMachine.ChangeState(this);
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
