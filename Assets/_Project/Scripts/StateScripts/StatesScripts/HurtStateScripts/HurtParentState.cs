using UnityEngine;

public class HurtParentState : State
{
    protected IdleState _idleState;
    protected DeathState _deathState;
    protected AttackState _attackState;
    protected ShadowbreakState _shadowbreakState;
    protected AttackSO _hurtAttack;
    protected bool _skipEnter;
    protected int _hurtFrame;

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
        _physics.Velocity = DemonicsVector2.Zero;
        _audio.Sound(_hurtAttack.impactSound).Play();
        _hurtFrame = _hurtAttack.hitStun;
        if (!_playerMovement.IsInCorner)
        {
            if (_hurtAttack.causesSoftKnockdown)
            {
                _playerMovement.Knockback(new Vector2(_hurtAttack.knockbackForce.x / 2, 0), _hurtAttack.knockbackDuration, (int)(-_player.transform.localScale.x), 0);
            }
            else
            {
                _playerMovement.Knockback(new Vector2(_hurtAttack.knockbackForce.x, _hurtAttack.knockbackForce.y), _hurtAttack.knockbackDuration, (int)(-_player.transform.localScale.x), _hurtAttack.knockbackArc);
            }
        }
        else
        {
            if (_hurtAttack.causesSoftKnockdown)
            {
                _playerMovement.Knockback(new Vector2(_hurtAttack.knockbackForce.x, 0), _hurtAttack.knockbackDuration, (int)(-_player.transform.localScale.x), 0, ignoreX: true);
            }
            else
            {
                _playerMovement.Knockback(new Vector2(_hurtAttack.knockbackForce.x, _hurtAttack.knockbackForce.y), _hurtAttack.knockbackDuration, (int)(-_player.transform.localScale.x), _hurtAttack.knockbackArc, ignoreX: true);
            }
        }
        _player.OtherPlayerUI.IncreaseCombo();
        if (_player.OtherPlayerUI.CurrentComboCount == 1)
        {
            _player.OtherPlayer.ResultAttack.comboDamage = 0;
            if (_hurtAttack.attackTypeEnum == AttackTypeEnum.Break)
            {
                _player.OtherPlayer.StartComboTimer(ComboTimerStarterEnum.Red);
            }
            else
            {
                _player.OtherPlayer.StartComboTimer(ComboTimerStarterEnum.Yellow);
            }
        }
        if (_hurtAttack.cameraShaker != null && !_hurtAttack.causesSoftKnockdown)
        {
            CameraShake.Instance.Shake(_hurtAttack.cameraShaker);
        }
        _player.SetHealth(_player.CalculateDamage(_hurtAttack));
        _playerUI.Damaged();
        _player.RecallAssist();
        GameManager.Instance.HitStop(_hurtAttack.hitstop);
        if (_player.Health <= 0)
        {
            ToDeathState();
        }
        _player.OtherPlayer.hitConnectsEvent?.Invoke();
    }

    private void ToDeathState()
    {
        _player.OtherPlayer.StopComboTimer();
        _stateMachine.ChangeState(_deathState);
    }

    public override bool AssistCall()
    {
        if (_player.AssistGauge >= (DemonicsFloat)1)
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