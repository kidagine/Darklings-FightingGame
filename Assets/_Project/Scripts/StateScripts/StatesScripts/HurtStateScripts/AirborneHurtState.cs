using System.Collections;
using UnityEngine;

public class AirborneHurtState : HurtParentState
{
    private WallSplatState _wallSplatState;
    private KnockdownState _knockdownState;
    private GrabbedState _grabbedState;
    private AirHurtState _airHurtState;
    private bool _canCheckGround;

    public bool WallSplat { get; set; }


    protected override void Awake()
    {
        base.Awake();
        _wallSplatState = GetComponent<WallSplatState>();
        _knockdownState = GetComponent<KnockdownState>();
        _grabbedState = GetComponent<GrabbedState>();
        _airHurtState = GetComponent<AirHurtState>();
    }

    public override void Enter()
    {
        _player.OtherPlayerUI.IncreaseCombo();
        if (_player.OtherPlayerUI.CurrentComboCount == 1)
        {
            _player.OtherPlayer.ResultAttack.comboDamage = 0;
        }
        _audio.Sound(_hurtAttack.impactSound).Play();
        _playerAnimator.HurtAir();
        if (_player.OtherPlayerUI.CurrentComboCount == 1)
        {
            _player.OtherPlayer.StartComboTimer(ComboTimerStarterEnum.Yellow);
        }
        _player.OtherPlayer.FreezeComboTimer();
        _player.SetHurtbox(true);
        if (WallSplat)
        {
            GameManager.Instance.AddHitstop(_player);
            _player.Flip((int)-_player.transform.localScale.x);
            _playerMovement.Knockback(new Vector2(_hurtAttack.knockbackForce.x, (float)DemonicsPhysics.GROUND_POINT), 30, (int)(-_player.transform.localScale.x), 3, ground: true);
        }
        else
        {
            _player.Flip((int)-_player.OtherPlayer.transform.localScale.x);
            GameObject effect = Instantiate(_hurtAttack.hurtEffect);
            effect.transform.localPosition = _hurtAttack.hurtEffectPosition;
            _playerMovement.Knockback(_hurtAttack.knockbackForce, _hurtAttack.knockbackDuration, (int)(_player.OtherPlayer.transform.localScale.x), _hurtAttack.knockbackArc);
        }
        CameraShake.Instance.Shake(_hurtAttack.cameraShaker);
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
    public override bool AssistCall()
    {
        return false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToKnockdownState();
        ToWallSplatState();
        if (!_playerMovement.IsGrounded)
        {
            _canCheckGround = true;
        }
    }

    private void ToDeathState()
    {
        _player.OtherPlayer.StopComboTimer();
        _stateMachine.ChangeState(_deathState);
    }

    private new void ToKnockdownState()
    {
        if (_playerMovement.IsGrounded && _canCheckGround)
        {
            _knockdownState.Initialize(_hurtAttack.causesSoftKnockdown);
            _stateMachine.ChangeState(_knockdownState);
        }
    }

    private void ToWallSplatState()
    {
        if (_playerMovement.IsInCorner && !WallSplat && !_hurtAttack.causesSoftKnockdown)
        {
            _stateMachine.ChangeState(_wallSplatState);
        }
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        this.Initialize(attack);
        _stateMachine.ChangeState(this);
        return true;
    }

    public override bool ToGrabbedState()
    {
        _stateMachine.ChangeState(_grabbedState);
        return true;
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
        WallSplat = false;
        _canCheckGround = false;
    }
}