using UnityEngine;

public class HurtParentState : State
{
	protected IdleState _idleState;
	protected DeathState _deathState;
	protected AttackState _attackState;
	protected AttackSO _hurtAttack;

	protected virtual void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_deathState = GetComponent<DeathState>();
		_attackState = GetComponent<AttackState>();
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
			_playerMovement.Knockback(new Vector2(
				_player.OtherPlayer.transform.localScale.x, 0.0f), _hurtAttack.knockback, _hurtAttack.knockbackDuration);
		}
		_player.Health--;
		_playerUI.SetHealth(_player.Health);
		_player.OtherPlayerUI.IncreaseCombo();
		_playerMovement.ResetGravity();
		_player.RecallAssist();
		GameManager.Instance.HitStop(_hurtAttack.hitstop);
		if (_player.Health <= 0)
		{
			ToDeathState();
		}
	}

	private void ToDeathState()
	{
		_player.OtherPlayerUI.ResetCombo();
		_stateMachine.ChangeState(_deathState);
	}

	public override void Exit()
	{
		base.Exit();
		_playerUI.UpdateHealthDamaged();
	}
}