using UnityEngine;

public class ArcanaState : State
{
    private IdleState _idleState;


    void Awake()
    {
        _idleState = GetComponent<IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Arcana();
        _player.CurrentAttack = _playerComboSystem.GetArcana();
    }

    public void ToIdleState()
    {
        if (_playerMovement._isGrounded)
        {
            _stateMachine.ChangeState(_idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}
