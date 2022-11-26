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
        _physics.IgnoreWalls = true;
        _physics.Velocity = DemonicsVector2.Zero;
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
        _playerAnimator.ArcanaThrow();
    }

    private new void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    public override void Exit()
    {
        base.Exit();
        _physics.IgnoreWalls = false;
    }
}