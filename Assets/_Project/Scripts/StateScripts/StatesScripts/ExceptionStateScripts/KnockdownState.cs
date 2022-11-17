using System.Collections;
using UnityEngine;

public class KnockdownState : State
{
    [SerializeField] private GameObject _groundedPrefab = default;
    private WakeUpState _wakeUpState;
    private DeathState _deathState;
    private readonly int _knockdownFrames = 60;
    private int _knockdownFramesCurrent;


    void Awake()
    {
        _wakeUpState = GetComponent<WakeUpState>();
        _deathState = GetComponent<DeathState>();
    }

    public override void Enter()
    {
        base.Enter();
        _audio.Sound("Landed").Play();
        _playerMovement.StopKnockback();
        _physics.Velocity = DemonicsVector2.Zero;
        _physics.EnableGravity(true);
        _playerAnimator.Knockdown();
        _player.OtherPlayer.StopComboTimer();
        _playerUI.DisplayNotification(NotificationTypeEnum.Knockdown);
        Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
        if (_player.Health <= 0)
        {
            ToDeathState();
        }
        else
        {
            _knockdownFramesCurrent = _knockdownFrames;
        }
    }

    private void ToDeathState()
    {
        _stateMachine.ChangeState(_deathState);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (DemonicsWorld.WaitFrames(ref _knockdownFramesCurrent))
        {
            _stateMachine.ChangeState(_wakeUpState);
        }
    }
}