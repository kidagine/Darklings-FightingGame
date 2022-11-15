using UnityEngine;

public class FallState : AirParentState
{
    private IdleState _idleState;
    private bool _checkInputBuffer;

    protected override void Awake()
    {
        base.Awake();
        _idleState = GetComponent<IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        if (_checkInputBuffer)
        {
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToIdleState();
        _player.CheckFlip();
    }

    public void Initialize(bool checkInputBuffer)
    {
        _checkInputBuffer = checkInputBuffer;
    }

    public void ToIdleState()
    {
        if (_playerMovement.IsGrounded && _physics.Velocity.y <= (DemonicsFloat)0)
        {
            Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
            _audio.Sound("Landed").Play();
            _physics.Velocity = DemonicsVector2.Zero;
            _stateMachine.ChangeState(_idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _checkInputBuffer = false;
    }
}
