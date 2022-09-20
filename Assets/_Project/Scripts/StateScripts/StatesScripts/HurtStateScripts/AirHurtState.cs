using UnityEngine;

public class AirHurtState : HurtParentState
{
    private FallState _fallState;
    private HurtState _hurtState;
    private AirborneHurtState _airborneHurtState;

    protected override void Awake()
    {
        base.Awake();
        _fallState = GetComponent<FallState>();
        _hurtState = GetComponent<HurtState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
    }

    public override void Enter()
    {
        _player.CheckFlip();
        _playerAnimator.HurtAir(true);
        GameObject effect = Instantiate(_hurtAttack.hurtEffect);
        effect.transform.localPosition = _hurtAttack.hurtEffectPosition;
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToFallStateAfterGround();
        _playerMovement.CheckForPlayer();
        if (DemonicsPhysics.WaitFrames(ref _hurtFrame))
        {
            ToFallAfterStunState();
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
        if (_playerMovement.IsGrounded && _rigidbody.velocity.y <= 0.0f)
        {
            _hurtState.Initialize(_hurtAttack, true);
            _stateMachine.ChangeState(_hurtState);
        }
    }

    public override bool ToHurtState(AttackSO attack)
    {
        this.Initialize(attack);
        _stateMachine.ChangeState(this);
        return true;
    }
}