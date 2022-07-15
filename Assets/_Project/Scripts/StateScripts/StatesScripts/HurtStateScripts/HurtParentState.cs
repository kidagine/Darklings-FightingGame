using UnityEngine;

public class HurtParentState : State
{
	protected IdleState _idleState;
	protected DeathState _deathState;
	protected AttackState _attackState;
	protected AttackSO _hurtAttack;
	private readonly float _damageDecay = 0.97f;
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
		_player.OtherPlayerUI.IncreaseCombo();
		if (_player.OtherPlayerUI.CurrentComboCount == 1)
		{
			if (_hurtAttack.attackTypeEnum == AttackTypeEnum.Break)
			{
				_player.OtherPlayer.StartComboTimer(ComboTimerStarterEnum.Red);
			}
			else
			{
				_player.OtherPlayer.StartComboTimer(ComboTimerStarterEnum.Yellow);
			}
		}
		_player.Health -= CalculateDamage();
		_playerUI.SetHealth(_player.Health);
		_playerUI.Damaged();
		_playerMovement.ResetGravity();
		_player.RecallAssist();
		GameManager.Instance.HitStop(_hurtAttack.hitstop);
		if (_player.Health <= 0)
		{
			ToDeathState();
		}
	}

	private float CalculateDamage()
	{
		int comboCount = _player.OtherPlayerUI.CurrentComboCount;
		float calculatedDamage = _hurtAttack.damage / _playerStats.PlayerStatsSO.defense;
		if (comboCount > 1)
		{
			float damageScale = 1.0f;
			for (int i = 0; i < comboCount; i++)
			{
				damageScale *= _damageDecay;
			}
			calculatedDamage *= damageScale;
		}
		_player.OtherPlayer.SetResultAttack((int)calculatedDamage);
		return (int)calculatedDamage;
	}

	private void ToDeathState()
	{
		_player.OtherPlayer.StopComboTimer();
		_stateMachine.ChangeState(_deathState);
	}

	public override void Exit()
	{
		base.Exit();
		_playerUI.UpdateHealthDamaged();
	}
}