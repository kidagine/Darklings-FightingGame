using Demonics.Manager;
using System.Collections;
using UnityEngine;

public class GrabbedState : State
{
    [SerializeField] private GameObject _techThrowPrefab = default;
    private DeathState _deathState;
    private KnockdownState _knockdownState;
    private AirborneHurtState _airborneHurtState;
    private KnockbackState _knockbackState;
    private bool _canTechThrow;


    private void Awake()
    {
        _deathState = GetComponent<DeathState>();
        _knockdownState = GetComponent<KnockdownState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
        _knockbackState = GetComponent<KnockbackState>();
    }

    public void Initialize(bool canTechThrow)
    {
        _canTechThrow = canTechThrow;
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.HurtAir();
        _physics.IgnoreWalls = true;
        _physics.EnableGravity(false);
        _playerMovement.StopKnockback();
        _physics.Velocity = DemonicsVector2.Zero;
        _player.OtherPlayerStateManager.TryToThrowState();
        StartCoroutine(CanTechThrowCoroutine());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // _physics.SetPositionWithRender(new DemonicsVector2(_player.OtherPlayerMovement.Physics.Position.x + _player.GrabPoint.x, _player.OtherPlayerMovement.Physics.Position.y + _player.GrabPoint.y));
    }

    public override bool ToKnockdownState()
    {
        _player.OtherPlayerUI.IncreaseCombo();
        _player.SetHealth(_player.playerStats.mThrow.damage);
        _playerUI.Damaged();
        if (_player.OtherPlayerUI.CurrentComboCount == 1)
        {
            _player.OtherPlayer.ResultAttack.comboDamage = 0;
        }
        _player.OtherPlayer.SetResultAttack(_player.playerStats.mThrow.damage, _player.playerStats.mThrow);
        _player.OtherPlayer.hitConnectsEvent?.Invoke();
        _player.OtherPlayer.StopComboTimer();
        GameplayManager.Instance.HitStop(_player.playerStats.mThrow.hitstop);
        if (_player.Health <= 0)
        {
            ToDeathState();
        }
        _stateMachine.ChangeState(_knockdownState);
        return true;
    }

    public override bool ToGrabState()
    {
        if (_canTechThrow && !_player.OtherPlayer.CurrentAttack.isArcana)
        {
            // ObjectPoolingManager.Instance.Spawn(_techThrowPrefab, new Vector2(transform.position.x, transform.position.y + 1));
            _playerUI.DisplayNotification(NotificationTypeEnum.ThrowBreak);
            _stateMachine.ChangeState(_knockbackState);
            _player.OtherPlayerStateManager.TryToKnockbackState();
            return true;
        }
        return false;
    }

    //MAKE DETERMINISTIC
    IEnumerator CanTechThrowCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        _canTechThrow = false;
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        _airborneHurtState.Initialize(attack);
        _stateMachine.ChangeState(_airborneHurtState);
        return true;
    }

    private void ToDeathState()
    {
        _stateMachine.ChangeState(_deathState);
    }

    public override void Exit()
    {
        base.Exit();
        _physics.IgnoreWalls = false;
        _physics.EnableGravity(true);
        _playerUI.UpdateHealthDamaged();
    }
}