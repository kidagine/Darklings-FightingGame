using System.Collections;
using UnityEngine;

public class KnockdownState : State
{
    [SerializeField] private GameObject _groundedPrefab = default;
    private WakeUpState _wakeUpState;
    private DeathState _deathState;
    private readonly int _hardKnockdownFrames = 60;
    private readonly int _softKnockdownFrames = 30;
    private int _knockdownFramesCurrent;
    private bool _softKnockdown;


    void Awake()
    {
        _wakeUpState = GetComponent<WakeUpState>();
        _deathState = GetComponent<DeathState>();
    }

    public void Initialize(bool softKnockdown = false)
    {
        _softKnockdown = softKnockdown;
    }

    public override void Enter()
    {
        base.Enter();
        _audio.Sound("Landed").Play();
        _playerMovement.StopKnockback();
        _physics.EnableGravity(true);
        _playerAnimator.Knockdown();
        _player.OtherPlayer.StopComboTimer();
        Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
        if (_softKnockdown)
        {
            _playerUI.DisplayNotification(NotificationTypeEnum.SoftKnockdown);
        }
        else
        {
            _playerUI.DisplayNotification(NotificationTypeEnum.Knockdown);
        }
        if (_player.Health <= 0)
        {
            ToDeathState();
        }
        else
        {
            if (_softKnockdown)
            {
                _knockdownFramesCurrent = _softKnockdownFrames;
            }
            else
            {
                _knockdownFramesCurrent = _hardKnockdownFrames;
            }
        }
    }

    private void ToDeathState()
    {
        _stateMachine.ChangeState(_deathState);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _physics.Velocity = DemonicsVector2.Zero;
        if (DemonicsWorld.WaitFrames(ref _knockdownFramesCurrent))
        {
            _stateMachine.ChangeState(_wakeUpState);
        }
        _player.CheckFlip();
    }
}