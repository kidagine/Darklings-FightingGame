using UnityEngine;

public class ArcanaThrowState : State
{
    private IdleState _idleState;

    private void Awake()
    {
        _idleState = GetComponent<IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
        _playerAnimator.ArcanaThrow();
    }

    private void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }
}