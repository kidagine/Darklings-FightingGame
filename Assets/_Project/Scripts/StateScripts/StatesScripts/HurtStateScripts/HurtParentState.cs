using UnityEngine;

public class HurtParentState : State
{
	protected IdleState _idleState;
	protected DeathState _deathState;
	protected AttackSO _hurtAttack;

	protected virtual void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_deathState = GetComponent<DeathState>();
	}

	public void Initialize(AttackSO hurtAttack)
	{
		_hurtAttack = hurtAttack;
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound(_hurtAttack.impactSound).Play();
		GameObject effect = Instantiate(_hurtAttack.hurtEffect);
		effect.transform.localPosition = _hurtAttack.hurtEffectPosition;
		if (!_playerMovement.IsInCorner)
		{
			_playerMovement.Knockback(new Vector2(transform.root.localScale.x * -1.0f, 0.0f), _hurtAttack.knockback, _hurtAttack.knockbackDuration);
		}
		_player.Health--;
		_playerUI.SetHealth(_player.Health);
		GameManager.Instance.HitStop(_hurtAttack.hitstop);
		if (_player.Health <= 0)
		{
			ToDeathState();
		}
	}

	private void ToDeathState()
	{
		_stateMachine.ChangeState(_deathState);
	}

	public override void Exit()
	{
		base.Exit();
		// _otherPlayerUI.ResetCombo();
		_playerUI.UpdateHealthDamaged();
	}
}