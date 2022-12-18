using UnityEngine;

public class AttackState : State
{
    private IdleState _idleState;
    private CrouchState _crouchState;
    private JumpState _jumpState;
    private JumpForwardState _jumpForwardState;
    private FallState _fallState;
    private HurtState _hurtState;
    private AirHurtState _airHurtState;
    private AirborneHurtState _airborneHurtState;
    private GrabbedState _grabbedState;
    private ArcanaState _arcanaState;
    private KnockbackState _knockbackState;
    protected RedFrenzyState _redFrenzyState;
    private InputEnum _inputEnum;
    private InputEnum _inputEnumCurrent;
    private bool _air;
    private bool _crouch;

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _crouchState = GetComponent<CrouchState>();
        _fallState = GetComponent<FallState>();
        _jumpState = GetComponent<JumpState>();
        _jumpForwardState = GetComponent<JumpForwardState>();
        _hurtState = GetComponent<HurtState>();
        _airHurtState = GetComponent<AirHurtState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
        _grabbedState = GetComponent<GrabbedState>();
        _arcanaState = GetComponent<ArcanaState>();
        _knockbackState = GetComponent<KnockbackState>();
        _redFrenzyState = GetComponent<RedFrenzyState>();
    }

    public void Initialize(InputEnum inputEnum, bool crouch, bool air)
    {
        _inputEnum = inputEnum;
        _crouch = crouch;
        _air = air;
    }

    public override void Enter()
    {
        base.Enter();
        _player.CheckFlip();
        _player.SetSpriteOrderPriority();
        _player.CurrentAttack = _playerComboSystem.GetComboAttack(_inputEnum, _crouch, _air);
        _audio.Sound(_player.CurrentAttack.attackSound).Play();
        _playerAnimator.Attack(_player.CurrentAttack.name);
        _player.CurrentAttack = _playerAnimator.GetFramedata(_player.CurrentAttack);
        if (!_air)
        {
            _playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
            if (_baseController.Crouch())
            {
                if (_player.CurrentAttack.isAirAttack)
                {
                    _playerAnimator.OnCurrentAnimationFinished.AddListener(ToFallState);
                }
                else
                {
                    _playerAnimator.OnCurrentAnimationFinished.AddListener(ToCrouchState);
                }
            }
            else
            {
                _playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
            }
        }
        else
        {
            _playerAnimator.OnCurrentAnimationFinished.AddListener(ToFallState);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToFallStateOnGround();
        ToJumpState();
        ToJumpForwardState();
        if (!_air && !_playerMovement.HasJumped)
        {
            _playerMovement.TravelDistance(new DemonicsVector2((DemonicsFloat)_player.CurrentAttack.travelDistance.x * transform.root.localScale.x, (DemonicsFloat)_player.CurrentAttack.travelDistance.y));
        }
    }

    private void ToFallStateOnGround()
    {
        if (_air && _playerMovement.IsGrounded && _physics.Velocity.y <= (DemonicsFloat)0)
        {
            _stateMachine.ChangeState(_fallState);
        }
    }

    private new void ToIdleState()
    {
        if (_stateMachine.CurrentState == this)
        {
            _stateMachine.ChangeState(_idleState);
        }
    }

    private void ToCrouchState()
    {
        if (_stateMachine.CurrentState == this)
        {
            _stateMachine.ChangeState(_crouchState);
        }
    }

    private void ToFallState()
    {
        if (_stateMachine.CurrentState == this)
        {
            _playerAnimator.Jump();
            _stateMachine.ChangeState(_fallState);
        }
    }

    public override bool ToAttackState(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum)
    {
        if (_lockJumpCancel)
        {
            return false;
        }
        if (_playerMovement.IsGrounded)
        {
            if (_player.CurrentAttack == _player.playerStats.m2H)
            {
                return false;
            }
            if (inputEnum == InputEnum.Heavy && inputDirectionEnum == InputDirectionEnum.NoneVertical)
            {
                return false;
            }
            if (inputEnum == InputEnum.Medium && _crouch && _player.CurrentAttack == _player.playerStats.m2M)
            {
                return false;
            }
        }
        else
        {
            if (_player.CurrentAttack == _player.playerStats.jH && !_player.OtherPlayerMovement.IsGrounded)
            {
                return false;
            }
        }

        if (_player.CanSkipAttack && inputEnum != InputEnum.Throw)
        {
            if (_playerMovement.IsInHitstop)
            {
                if (!_player.LockChain)
                {
                    Attack(inputEnum, inputDirectionEnum);
                    _player.hitstopEvent.AddListener(ChainAttack);
                    _player.LockChain = true;
                }
            }
            else
            {
                Attack(inputEnum, inputDirectionEnum);
                ChainAttack();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Attack(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum)
    {
        _inputEnumCurrent = inputEnum;
        if (inputDirectionEnum == InputDirectionEnum.Down || _baseController.Crouch())
        {
            if (_playerMovement.IsGrounded)
            {
                _crouch = true;
            }
        }
        else
        {
            _crouch = false;
        }
        if (_player.CurrentAttack.isAirAttack)
        {
            _air = true;
        }
    }

    private void ChainAttack()
    {
        Initialize(_inputEnumCurrent, _crouch, _air);
        _stateMachine.ChangeState(this);
        _player.LockChain = false;
    }

    public override bool ToArcanaState(InputDirectionEnum inputDirectionEnum)
    {
        if (_player.ArcanaGauge >= (DemonicsFloat)1 && _player.CanSkipAttack)
        {
            if (_playerMovement.IsInHitstop)
            {
                if (!_player.LockChain)
                {
                    Arcana(inputDirectionEnum);
                    _player.hitstopEvent.AddListener(ChainArcana);
                    _player.LockChain = true;
                }
            }
            else
            {
                Arcana(inputDirectionEnum);
                ChainArcana();
            }
            return true;
        }
        return false;
    }

    private void Arcana(InputDirectionEnum inputDirectionEnum)
    {
        if (inputDirectionEnum == InputDirectionEnum.Down || _baseController.Crouch())
        {
            if (_playerMovement.IsGrounded)
            {
                _crouch = true;
            }
        }
        else
        {
            _crouch = false;
        }
        if (_air && _player.CanAirArcana)
        {
            _player.CanAirArcana = false;
        }
    }

    private void ChainArcana()
    {
        _arcanaState.Initialize(_crouch, _air);
        _stateMachine.ChangeState(_arcanaState);
        _player.LockChain = false;
    }

    public override bool ToHurtState(AttackSO attack)
    {
        if (_player.CanTakeSuperArmorHit(attack))
        {
            _audio.Sound(attack.impactSound).Play();
            _player.HurtOnSuperArmor(attack);
            if (_player.Health <= 0)
            {
                _hurtState.Initialize(attack);
                _stateMachine.ChangeState(_hurtState);
            }
            return true;
        }
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
    private bool _lockJumpCancel;
    private void ToJumpState()
    {
        if (_player.CurrentAttack.jumpCancelable || _air && !_lockJumpCancel)
        {
            if (_player.playerStats.canDoubleJump && !_playerMovement.HasDoubleJumped && _player.OtherPlayerStateManager.CurrentState is AirHurtState)
            {
                if (_baseController.InputDirection.x == 0)
                {
                    if (_baseController.InputDirection.y > 0 && !_playerMovement.HasJumped)
                    {
                        _lockJumpCancel = true;
                        _playerMovement.HasJumped = true;
                        if (_player.IsInHitstop())
                        {
                            _player.hitstopEvent.AddListener(JumpEvent);
                        }
                        else
                        {
                            JumpEvent();
                        }
                    }
                    else if (_baseController.InputDirection.y <= 0 && _playerMovement.HasJumped)
                    {
                        _playerMovement.HasJumped = false;
                    }
                }
            }
        }
    }

    private void JumpEvent()
    {
        if (!_playerMovement.IsGrounded && _player.CurrentAttack != _player.playerStats.m2H)
        {
            _playerMovement.HasDoubleJumped = true;
        }
        _jumpState.Initialize(true);
        _stateMachine.ChangeState(_jumpState);
        _lockJumpCancel = false;
    }

    public void ToJumpForwardState()
    {
        if (_player.CurrentAttack.jumpCancelable || _air && !_lockJumpCancel)
        {
            if (_player.playerStats.canDoubleJump && !_playerMovement.HasDoubleJumped && _player.OtherPlayerStateManager.CurrentState is AirHurtState)
            {
                if (_baseController.InputDirection.x != 0)
                {
                    if (_baseController.InputDirection.y > 0 && !_playerMovement.HasJumped)
                    {
                        _lockJumpCancel = true;
                        _playerMovement.HasJumped = true;
                        if (_player.IsInHitstop())
                        {
                            _player.hitstopEvent.AddListener(JumpForwardEvent);
                        }
                        else
                        {
                            JumpForwardEvent();
                        }
                    }
                    else if (_baseController.InputDirection.y <= 0 && _playerMovement.HasJumped)
                    {
                        _playerMovement.HasJumped = false;
                    }
                }
            }
        }
    }

    private void JumpForwardEvent()
    {
        if (!_playerMovement.IsGrounded && _player.CurrentAttack != _player.playerStats.m2H)
        {
            _playerMovement.HasDoubleJumped = true;
        }
        _jumpForwardState.Initialize(true);
        _stateMachine.ChangeState(_jumpForwardState);
        _lockJumpCancel = false;
    }

    public override bool ToRedFrenzyState()
    {
        if (_player.CanSkipAttack)
        {
            if (_player.HasRecoverableHealth())
            {
                _stateMachine.ChangeState(_redFrenzyState);
                return true;
            }
        }
        return false;
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        if (_player.CanTakeSuperArmorHit(attack))
        {
            _audio.Sound(attack.impactSound).Play();
            _player.HurtOnSuperArmor(attack);
            if (_player.Health <= 0)
            {
                _hurtState.Initialize(attack);
                _stateMachine.ChangeState(_hurtState);
            }
            return true;
        }
        _airborneHurtState.Initialize(attack);
        _stateMachine.ChangeState(_airborneHurtState);
        return true;
    }

    public override bool ToGrabbedState()
    {
        if (_playerMovement.IsGrounded)
        {
            _stateMachine.ChangeState(_grabbedState);
            return true;
        }
        return false;
    }

    public override bool AssistCall()
    {
        _player.AssistAction();
        return true;
    }

    public override bool ToKnockbackState()
    {
        _stateMachine.ChangeState(_knockbackState);
        return true;
    }

    public override void Exit()
    {
        base.Exit();
        _player.hitstopEvent.RemoveAllListeners();
        _physics.EnableGravity(true);
        if (!_air)
        {
            _physics.Velocity = new DemonicsVector2((DemonicsFloat)0, _physics.Velocity.y);
        }
        _player.CanSkipAttack = false;
        _playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
    }
}