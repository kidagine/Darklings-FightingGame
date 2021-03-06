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
        if (_playerMovement.IsGrounded && _rigidbody.velocity.y <= 0.0f)
        {
            Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
            _audio.Sound("Landed").Play();
            _stateMachine.ChangeState(_idleState);
        }
    }
}
