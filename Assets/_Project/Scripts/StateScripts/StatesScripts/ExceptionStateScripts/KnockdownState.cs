using System.Collections;
using UnityEngine;

public class KnockdownState : State
{
    [SerializeField] private GameObject _groundedPrefab = default;
    private WakeUpState _wakeUpState;
    private DeathState _deathState;
    private Coroutine _knockdownCoroutine;

    void Awake()
    {
        _wakeUpState = GetComponent<WakeUpState>();
        _deathState = GetComponent<DeathState>();
    }

    public override void Enter()
    {
        base.Enter();
        _audio.Sound("Landed").Play();
        _playerAnimator.Knockdown();
        _player.SetHurtbox(false);
        _player.OtherPlayer.StopComboTimer();
        _playerUI.DisplayNotification(NotificationTypeEnum.Knockdown);
        Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
        _knockdownCoroutine = StartCoroutine(ToWakeUpStateCoroutine());
        if (_player.Health <= 0)
        {
            ToDeathState();
        }
        _player.SetHurtbox(false);
    }

    IEnumerator ToWakeUpStateCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        ToWakeUpState();
    }

    private void ToWakeUpState()
    {
        _stateMachine.ChangeState(_wakeUpState);
    }

    private void ToDeathState()
    {
        _stateMachine.ChangeState(_deathState);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _playerMovement.CheckForPlayer();
    }

    public override void Exit()
    {
        base.Exit();
        if (_knockdownCoroutine != null)
        {
            StopCoroutine(_knockdownCoroutine);
        }
        _player.SetHurtbox(true);
    }
}