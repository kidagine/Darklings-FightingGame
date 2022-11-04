using FixMath.NET;
using UnityEngine;

public class FallState : AirParentState
{
    private IdleState _idleState;

    protected override void Awake()
    {
        base.Awake();
        _idleState = GetComponent<IdleState>();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToIdleState();
        _player.CheckFlip();
    }

    public void ToIdleState()
    {
        if (_playerMovement.IsGrounded && _physics.Velocity.y <= (Fix64)0)
        {
            Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
            _audio.Sound("Landed").Play();
            _physics.Velocity = FixVector2.Zero;
            _stateMachine.ChangeState(_idleState);
        }
    }
}
