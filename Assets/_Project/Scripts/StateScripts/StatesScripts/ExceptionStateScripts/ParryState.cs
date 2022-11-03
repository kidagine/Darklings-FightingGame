using UnityEngine;

public class ParryState : State
{
    [SerializeField] private GameObject _parryEffect = default;
    private IdleState _idleState;
    private HurtState _hurtState;
    private AirborneHurtState _airborneHurtState;
    private GrabbedState _grabbedState;
    private BlockState _blockState;
    private readonly int _parryKnockback = 2;
    private readonly int _hitstopOnParry = 12;
    private readonly int _knockbackDuration = 10;
    private bool _parried;

    private void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _hurtState = GetComponent<HurtState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
        _grabbedState = GetComponent<GrabbedState>();
        _blockState = GetComponent<BlockState>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetSpriteOrderPriority();
        _player.SetResultAttack(0, _player.playerStats.mParry);
        _player.parryConnectsEvent?.Invoke();
        _audio.Sound("ParryStart").Play();
        _player.CheckFlip();
        _playerAnimator.BlueFrenzy();
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
    }


    private void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    public override bool ToHurtState(AttackSO attack)
    {
        if (_player.Parrying)
        {
            Parry(attack);
            return false;
        }
        _hurtState.Initialize(attack);
        _stateMachine.ChangeState(_hurtState);
        return true;
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        if (_player.Parrying)
        {
            Parry(attack);
            return false;
        }
        _airborneHurtState.Initialize(attack);
        _stateMachine.ChangeState(_airborneHurtState);
        return true;
    }

    private void Parry(AttackSO attack)
    {
        _audio.Sound("Parry").Play();
        _player.HealthGain();
        _parried = true;
        GameManager.Instance.HitStop(_hitstopOnParry);
        GameObject effect = Instantiate(_parryEffect);
        effect.transform.localPosition = attack.hurtEffectPosition;
        if (!attack.isProjectile)
        {
            if (_player.OtherPlayerMovement.IsInCorner)
            {
                _playerMovement.Knockback(new Vector2(
                    _player.OtherPlayer.transform.localScale.x, 0), new Vector2(_parryKnockback, 0), _knockbackDuration);
            }
            else
            {
                _player.OtherPlayerMovement.Knockback(new Vector2(
                    _player.transform.localScale.x, 0), new Vector2(_parryKnockback, 0), _knockbackDuration);
            }
        }
    }

    public override bool ToBlockState(AttackSO attack)
    {
        if (_parried)
        {
            _blockState.Initialize(attack);
            _stateMachine.ChangeState(_blockState);
            return true;
        }
        return false;
    }

    public override bool ToParryState()
    {
        if (_parried)
        {
            _stateMachine.ChangeState(this);
            return true;
        }
        return false;
    }

    public override bool ToGrabbedState()
    {
        _stateMachine.ChangeState(_grabbedState);
        return true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _rigidbody.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
        _parried = false;
        _playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
    }
}