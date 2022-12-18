using Demonics.Manager;
using System.Collections;
using UnityEngine;

public class DashState : State
{
    [SerializeField] private GameObject _playerGhostPrefab = default;
    [SerializeField] private GameObject _dashPrefab = default;
    private IdleState _idleState;
    private RunState _runState;
    private HurtState _hurtState;
    private AirborneHurtState _airborneHurtState;
    private RedFrenzyState _redFrenzyState;
    private Coroutine _dashCoroutine;
    private readonly int _dashFrames = 5;
    private int _dashFramesCurrent;
    private readonly int _ghostsAmount = 3;
    private int _ghostsAmountCurrent;

    public int DashDirection { get; set; }

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _runState = GetComponent<RunState>();
        _hurtState = GetComponent<HurtState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
        _redFrenzyState = GetComponent<RedFrenzyState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Dash();
        _audio.Sound("Dash").Play();
        _dashFramesCurrent = _dashFrames;
        //Transform dashEffect = ObjectPoolingManager.Instance.Spawn(_dashPrefab, transform.root.position).transform;
        // if (DashDirection > 0)
        // {
        //     dashEffect.localScale = new Vector2(1, transform.localScale.y);
        //     dashEffect.position = new Vector2(dashEffect.position.x - 1, dashEffect.position.y);
        // }
        // else
        // {
        //     dashEffect.localScale = new Vector2(-1, transform.localScale.y);
        //     dashEffect.position = new Vector2(dashEffect.position.x + 1, dashEffect.position.y);
        // }
        _physics.Velocity = new DemonicsVector2((DemonicsFloat)DashDirection * (DemonicsFloat)_player.playerStats.DashForce, _physics.Velocity.y);
    }

    private new void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    private void ToRunState()
    {
        _stateMachine.ChangeState(_runState);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _player.CheckFlip();
        Dash();
    }

    private void Dash()
    {
        if (_ghostsAmountCurrent < _ghostsAmount)
        {
            if (DemonicsWorld.WaitFramesOnce(ref _dashFramesCurrent))
            {
                //GameObject playerGhost = ObjectPoolingManager.Instance.Spawn(_playerGhostPrefab, transform.position);
                //playerGhost.GetComponent<PlayerGhost>().SetSprite(_playerAnimator.GetCurrentSprite(), transform.root.localScale.x, Color.white);
                _dashFramesCurrent = _dashFrames;
                _ghostsAmountCurrent++;
            }
        }
        else
        {
            if (_baseController.InputDirection.x * transform.root.localScale.x > 0)
            {
                ToRunState();
            }
            else
            {
                ToIdleState();
            }
            _inputBuffer.CheckInputBufferAttacks();
        }
    }

    public override bool AssistCall()
    {
        _player.AssistAction();
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

    public override bool ToHurtState(AttackSO attack)
    {
        _hurtState.Initialize(attack);
        _stateMachine.ChangeState(_hurtState);
        return true;
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        _airborneHurtState.Initialize(attack);
        _stateMachine.ChangeState(_airborneHurtState);
        return true;
    }

    public override void Exit()
    {
        base.Exit();
        _ghostsAmountCurrent = 0;
        _physics.Velocity = DemonicsVector2.Zero;
    }
}
