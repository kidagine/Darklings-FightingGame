using UnityEngine;

public class WakeUpState : State
{
    private IdleState _idleState;

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        if (GameManager.Instance.InfiniteHealth)
        {
            _player.MaxHealthStats();
        }
        _player.CheckFlip();
        _playerAnimator.WakeUp();
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
    }

    private void ToIdleState()
    {
        if (_stateMachine.CurrentState == this)
        {
            _stateMachine.ChangeState(_idleState);
        }
    }
}
