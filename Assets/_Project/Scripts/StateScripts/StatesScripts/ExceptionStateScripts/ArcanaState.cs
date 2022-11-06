using FixMath.NET;
using UnityEngine;

public class ArcanaState : State
{
    private IdleState _idleState;
    private FallState _fallState;
    private HurtState _hurtState;
    private AirHurtState _airHurtState;
    private AirborneHurtState _airborneHurtState;
    private GrabbedState _grabbedState;
    private ArcanaThrowState _arcanaThrowState;
    protected RedFrenzyState _redFrenzyState;
    private bool _crouch;
    private bool _air;

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _fallState = GetComponent<FallState>();
        _hurtState = GetComponent<HurtState>();
        _airHurtState = GetComponent<AirHurtState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
        _grabbedState = GetComponent<GrabbedState>();
        _arcanaThrowState = GetComponent<ArcanaThrowState>();
        _redFrenzyState = GetComponent<RedFrenzyState>();
    }

    public void Initialize(bool crouch = false, bool air = false)
    {
        _crouch = crouch;
        _air = air;
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetSpriteOrderPriority();
        _playerMovement.StopKnockback();
        if (_playerComboSystem.GetArcana(_crouch, _air).reversal)
        {
            _playerUI.DisplayNotification(NotificationTypeEnum.Reversal);
        }
        _player.CurrentAttack = _playerComboSystem.GetArcana(_crouch, _air);
        _audio.Sound(_player.CurrentAttack.attackSound).Play();
        _player.ArcanaGauge -= (Fix64)1;
        if (_player.CurrentAttack.travelDistance.x == 0)
        {
            _playerMovement.ZeroGravity();
        }
        _playerUI.DecreaseArcana();
        _playerUI.SetArcana((float)_player.ArcanaGauge);
        _playerMovement.TravelDistance(new Vector2(_player.CurrentAttack.travelDistance.x * transform.root.localScale.x, _player.CurrentAttack.travelDistance.y));
        _playerAnimator.Arcana(_player.CurrentAttack.name);
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ArcanaEnd);
    }

    private void ArcanaEnd()
    {
        if (_stateMachine.CurrentState == this)
        {
            if (!_playerMovement.IsGrounded)
            {
                ToFallState();
            }
            else
            {
                ToIdleState();
            }
        }
    }

    private void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    public void ToFallState()
    {
        _playerAnimator.Jump();
        _stateMachine.ChangeState(_fallState);
    }

    public override bool AssistCall()
    {
        _player.AssistAction();
        return true;
    }

    public override bool ToHurtState(AttackSO attack)
    {
        _player.OtherPlayerUI.DisplayNotification(NotificationTypeEnum.Punish);
        if (_playerMovement.IsGrounded)
        {
            _hurtState.Initialize(attack);
            _stateMachine.ChangeState(_hurtState);
        }
        else
        {
            _airHurtState.Initialize(attack);
            _stateMachine.ChangeState(_airHurtState);
        }
        return true;
    }

    public override bool ToRedFrenzyState()
    {
        _stateMachine.ChangeState(_redFrenzyState);
        return true;
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        _player.OtherPlayerUI.DisplayNotification(NotificationTypeEnum.Punish);
        _airborneHurtState.Initialize(attack);
        _stateMachine.ChangeState(_airborneHurtState);
        return true;
    }

    public override bool ToGrabbedState()
    {
        _stateMachine.ChangeState(_grabbedState);
        return true;
    }

    public override bool ToThrowState()
    {
        _stateMachine.ChangeState(_arcanaThrowState);
        return true;
    }

    public override void Exit()
    {
        base.Exit();
        _playerMovement.ResetGravity();
    }
}
