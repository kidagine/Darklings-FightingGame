using UnityEngine;

public class JumpForwardState : AirParentState
{
    [SerializeField] protected GameObject _jumpPrefab = default;
    private DemonicsFloat _jumpX;
    private DemonicsFloat _jumpY;
    private DemonicsVector2 _jump;
    private bool _jumpCancel;
    private readonly DemonicsFloat _jumpForwardX = (DemonicsFloat)0.14;

    public void Initialize(bool jumpCancel = false)
    {
        _jumpCancel = jumpCancel;
    }

    public override void Enter()
    {
        base.Enter();
        Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
        _audio.Sound("Jump").Play();
        _playerAnimator.JumpForward();
        _playerMovement.ResetToWalkSpeed();
        if (_jumpCancel)
        {
            _player.ExitHitstop();
            _playerMovement.StopKnockback();
            _player.HasJuggleForce = true;
            _jump = new DemonicsVector2(_jumpForwardX * (DemonicsFloat)_baseController.InputDirection.x, _jumpCancelForce);
        }
        else
        {
            if (_playerMovement.HasDoubleJumped)
            {
                _jump = new DemonicsVector2(_jumpForwardX * (DemonicsFloat)_baseController.InputDirection.x, (_player.playerStats.JumpForce + (DemonicsFloat)0.01) / _jumpDoubleDivider);
            }
            else
            {
                _jump = new DemonicsVector2(_jumpForwardX * (DemonicsFloat)_baseController.InputDirection.x, _player.playerStats.JumpForce + (DemonicsFloat)0.01);
            }
        }
        _physics.Velocity = _jump;
    }


    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _physics.Velocity = new DemonicsVector2(_jump.x, _physics.Velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        _jumpCancel = false;
    }
}
