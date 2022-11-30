using UnityEngine;

public class JumpState : AirParentState
{
    [SerializeField] protected GameObject _jumpPrefab = default;
    private int _pushboxFrame;
    private bool _jumpCancel;

    public void Initialize(bool jumpCancel = false)
    {
        _jumpCancel = jumpCancel;
    }

    public override void Enter()
    {
        base.Enter();
        Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
        _audio.Sound("Jump").Play();
        _playerAnimator.Jump();
        _physics.Velocity = new DemonicsVector2((DemonicsFloat)0, _physics.Velocity.y);
        if (_jumpCancel)
        {
            _player.ExitHitstop();
            _playerMovement.StopKnockback();
            _player.HasJuggleForce = true;
            _physics.Velocity = new DemonicsVector2(_physics.Velocity.x, _jumpCancelForce);
        }
        else
        {
            if (_playerMovement.HasDoubleJumped)
            {
                _physics.Velocity = new DemonicsVector2(_physics.Velocity.x, _player.playerStats.JumpForce / _jumpDoubleDivider);
            }
            else
            {
                _physics.Velocity = new DemonicsVector2(_physics.Velocity.x, _player.playerStats.JumpForce);
            }
        }
        _player.SetPushboxTrigger(true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _pushboxFrame++;
        if (_pushboxFrame == 2)
        {
            _player.SetPushboxTrigger(false);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _jumpCancel = false;
        _player.SetPushboxTrigger(false);
    }
}
