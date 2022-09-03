using System.Collections;
using UnityEngine;

public class HurtParentState : State
{
	protected IdleState _idleState;
	protected DeathState _deathState;
	protected AttackState _attackState;
	protected ShadowbreakState _shadowbreakState;
	protected AttackSO _hurtAttack;
	protected bool _skipEnter;

	protected virtual void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_deathState = GetComponent<DeathState>();
		_attackState = GetComponent<AttackState>();
		_shadowbreakState = GetComponent<ShadowbreakState>();
	}

	public void Initialize(AttackSO hurtAttack, bool skipEnter = false)
	{
		_hurtAttack = hurtAttack;
		_skipEnter = skipEnter;
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound(_hurtAttack.impactSound).Play();
		if (!_playerMovement.IsInCorner)
		{
			_playerMovement.Knockback(new Vector2(
				_player.OtherPlayer.transform.localScale.x, 1.0f), new Vector2(_hurtAttack.knockback, _hurtAttack.bounce), _hurtAttack.knockbackDuration);
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
		_player.Health -= _player.CalculateDamage(_hurtAttack);
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

	private void ToDeathState()
	{
		_player.OtherPlayer.StopComboTimer();
		_stateMachine.ChangeState(_deathState);
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


	public override void Exit()
	{
		base.Exit();
		_playerUI.UpdateHealthDamaged();
	}
}