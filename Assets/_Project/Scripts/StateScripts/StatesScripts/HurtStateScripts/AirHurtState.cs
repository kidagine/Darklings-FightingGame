using System.Collections;
using UnityEngine;

public class AirHurtState : HurtParentState
{
    private FallState _fallState;
    private HurtState _hurtState;
    private AirborneHurtState _airborneHurtState;
    private int _groundCheckFrames;
    private bool _canCheckGround;

    protected override void Awake()
    {
        base.Awake();
        _fallState = GetComponent<FallState>();
        _hurtState = GetComponent<HurtState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
    }

    public override void Enter()
    {
        _player.OtherPlayer.UnfreezeComboTimer();
        _player.CheckFlip();
        _playerAnimator.HurtAir();
        GameObject effect = Instantiate(_hurtAttack.hurtEffect);
        effect.transform.localPosition = _hurtAttack.hurtEffectPosition;
        base.Enter();
        _player.hitstopEvent.AddListener(() => _playerMovement.TravelDistance(new DemonicsVector2((DemonicsFloat)0, (DemonicsFloat)0.16)));
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToFallStateAfterGround();
        if (DemonicsWorld.WaitFrames(ref _hurtFrame))
        {
            ToFallAfterStunState();
        }
        if (!_player.IsInHitstop())
        {
            _groundCheckFrames++;
        }
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        _airborneHurtState.Initialize(attack);
        _stateMachine.ChangeState(_airborneHurtState);
        return true;
    }

    private void ToFallAfterStunState()
    {
        if (GameManager.Instance.InfiniteHealth)
        {
            _player.MaxHealthStats();
        }
        _playerAnimator.Jump();
        _player.OtherPlayer.StopComboTimer();
        _stateMachine.ChangeState(_fallState);
    }

    private void ToFallStateAfterGround()
    {
        if (_playerMovement.IsGrounded && _physics.Velocity.y <= (DemonicsFloat)0 && _groundCheckFrames > 3)
        {
            _physics.Velocity = DemonicsVector2.Zero;
            _hurtState.Initialize(_hurtAttack, true);
            _stateMachine.ChangeState(_hurtState);
        }
    }

    public override bool ToHurtState(AttackSO attack)
    {
        this.Initialize(attack);
        _canCheckGround = false;
        _stateMachine.ChangeState(this);
        return true;
    }

    public override void Exit()
    {
        base.Exit();
        _groundCheckFrames = 0;
    }
}