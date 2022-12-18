using Demonics.Manager;
using UnityEngine;

public class AirDashState : State
{
    [SerializeField] private GameObject _playerGhostPrefab = default;
    [SerializeField] private GameObject _dashPrefab = default;
    private RedFrenzyState _redFrenzyState;
    private FallState _fallState;
    private JumpState _jumpState;
    private JumpForwardState _jumpForwardState;
    private readonly int _dashFrames = 5;
    private int _dashFramesCurrent;
    private readonly int _ghostsAmount = 3;
    private int _ghostsAmountCurrent;
    public int DashDirection { get; set; }
    void Awake()
    {
        _fallState = GetComponent<FallState>();
        _jumpState = GetComponent<JumpState>();
        _jumpForwardState = GetComponent<JumpForwardState>();
        _redFrenzyState = GetComponent<RedFrenzyState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.AirDash();
        _audio.Sound("Dash").Play();
        _player.SetPushboxTrigger(false);
        _dashFramesCurrent = _dashFrames;
        // Transform dashEffect = ObjectPoolingManager.Instance.Spawn(_dashPrefab, transform.position).transform;
        // if (DashDirection > 0)
        // {
        //     dashEffect.localScale = new Vector2(1, transform.localScale.y);
        //     dashEffect.position = new Vector2(dashEffect.position.x - 1, dashEffect.position.y);
        // }
        // else
        // {
        //     dashEffect.localScale = new Vector2(-1, transform.root.localScale.y);
        //     dashEffect.position = new Vector2(dashEffect.position.x + 1, dashEffect.position.y);
        // }
        _physics.Velocity = new DemonicsVector2((DemonicsFloat)DashDirection * (DemonicsFloat)_player.playerStats.DashForce, (DemonicsFloat)0);
        _physics.EnableGravity(false);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToJumpState();
        ToJumpForwardState();
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
            _stateMachine.ChangeState(_fallState);
            _inputBuffer.CheckInputBufferAttacks();
        }
    }

    public void ToJumpState()
    {
        if (_player.playerStats.canDoubleJump && !_playerMovement.HasDoubleJumped && _ghostsAmountCurrent == _ghostsAmount)
        {
            if (_baseController.InputDirection.x == 0)
            {
                if (_baseController.InputDirection.y > 0 && !_playerMovement.HasJumped)
                {
                    _playerMovement.HasDoubleJumped = true;
                    _playerMovement.HasJumped = true;
                    _stateMachine.ChangeState(_jumpState);
                }
                else if (_baseController.InputDirection.y <= 0 && _playerMovement.HasJumped)
                {
                    _playerMovement.HasJumped = false;
                }
            }
        }
    }

    public void ToJumpForwardState()
    {
        if (_player.playerStats.canDoubleJump && !_playerMovement.HasDoubleJumped && _ghostsAmountCurrent == _ghostsAmount)
        {
            if (_baseController.InputDirection.x != 0)
            {
                if (_baseController.InputDirection.y > 0 && !_playerMovement.HasJumped)
                {
                    _playerMovement.HasDoubleJumped = true;
                    _playerMovement.HasJumped = true;
                    _stateMachine.ChangeState(_jumpForwardState);
                }
                else if (_baseController.InputDirection.y <= 0 && _playerMovement.HasJumped)
                {
                    _playerMovement.HasJumped = false;
                }
            }
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

    public override void Exit()
    {
        base.Exit();
        _ghostsAmountCurrent = 0;
        _physics.Velocity = DemonicsVector2.Zero;
        _physics.EnableGravity(true);
    }
}

