
using UnityEngine;

public class GroundParentState : State
{
    protected IdleState _idleState;
    protected WalkState _walkState;
    protected CrouchState _crouchState;
    protected JumpState _jumpState;
    protected JumpForwardState _jumpForwardState;
    protected DashState _dashState;
    protected AttackState _attackState;
    protected ArcanaState _arcanaState;
    protected GrabState _grabState;
    protected HurtState _hurtState;
    protected AirHurtState _airHurtState;
    protected AirborneHurtState _airborneHurtState;
    protected BlockState _blockState;
    protected BlockLowState _blockLowState;
    protected GrabbedState _grabbedState;
    protected KnockbackState _knockbackState;
    protected TauntState _tauntState;
    protected GiveUpState _giveUpState;
    protected ParryState _parryState;
    protected RedFrenzyState _redFrenzyState;

    protected virtual void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _walkState = GetComponent<WalkState>();
        _crouchState = GetComponent<CrouchState>();
        _jumpState = GetComponent<JumpState>();
        _jumpForwardState = GetComponent<JumpForwardState>();
        _dashState = GetComponent<DashState>();
        _attackState = GetComponent<AttackState>();
        _arcanaState = GetComponent<ArcanaState>();
        _grabState = GetComponent<GrabState>();
        _hurtState = GetComponent<HurtState>();
        _airHurtState = GetComponent<AirHurtState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
        _blockState = GetComponent<BlockState>();
        _blockLowState = GetComponent<BlockLowState>();
        _grabbedState = GetComponent<GrabbedState>();
        _knockbackState = GetComponent<KnockbackState>();
        _tauntState = GetComponent<TauntState>();
        _giveUpState = GetComponent<GiveUpState>();
        _parryState = GetComponent<ParryState>();
        _redFrenzyState = GetComponent<RedFrenzyState>();
    }

    public override bool ToAttackState(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum)
    {
        if (inputDirectionEnum == InputDirectionEnum.Down || _baseController.Crouch())
        {
            _attackState.Initialize(inputEnum, true, false);
        }
        else
        {
            _attackState.Initialize(inputEnum, false, false);
        }
        _stateMachine.ChangeState(_attackState);
        return true;
    }

    public override bool ToArcanaState(InputDirectionEnum inputDirectionEnum)
    {
        if (_player.ArcanaGauge >= (DemonicsFloat)1)
        {
            if (inputDirectionEnum == InputDirectionEnum.Down || _baseController.Crouch())
            {
                _arcanaState.Initialize(true);
            }
            else
            {
                _arcanaState.Initialize(false);
            }
            _stateMachine.ChangeState(_arcanaState);
            return true;
        }
        return false;
    }

    public override bool ToGrabState()
    {
        _stateMachine.ChangeState(_grabState);
        return true;
    }

    public override bool ToHurtState(AttackSO attack)
    {
        if (attack.knockbackArc > 0)
        {
            _airHurtState.Initialize(attack);
            _stateMachine.ChangeState(_airHurtState);
        }
        else
        {
            _hurtState.Initialize(attack);
            _stateMachine.ChangeState(_hurtState);
        }
        return true;
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        _airborneHurtState.Initialize(attack);
        _stateMachine.ChangeState(_airborneHurtState);
        return true;
    }

    public override bool ToGrabbedState()
    {
        _grabbedState.Initialize(true);
        _stateMachine.ChangeState(_grabbedState);
        return true;
    }

    public override bool ToKnockbackState()
    {
        if (_player.BlockingLeftOrRight())
        {
            GameManager.Instance.AddHitstop(_player);
            _blockState.Initialize(new AttackSO() { blockStun = 30, hitstop = 5, knockbackForce = new Vector2(0.1f, 1), knockbackDuration = 5, hurtEffectPosition = new Vector2(0, (float)_physics.Position.y + 1) });
            _stateMachine.ChangeState(_blockState);
            return false;
        }
        else
        {
            _stateMachine.ChangeState(_knockbackState);
            return true;
        }
    }

    public override bool ToTauntState()
    {
        _stateMachine.ChangeState(_tauntState);
        return true;
    }

    public override bool ToRedFrenzyState()
    {
        if (_player.HasRecoverableHealth())
        {
            _stateMachine.ChangeState(_redFrenzyState);
            return true;
        }
        return false;
    }

    public override bool ToGiveUpState()
    {
        _stateMachine.ChangeState(_giveUpState);
        return true;
    }

    public override bool AssistCall()
    {
        _player.AssistAction();
        return true;
    }
}
