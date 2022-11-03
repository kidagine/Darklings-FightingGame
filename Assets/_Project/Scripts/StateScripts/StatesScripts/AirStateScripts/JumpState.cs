using FixMath.NET;
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
        _playerAnimator.Jump(true);
        _physics.VelocityX = (Fix64)0;
        if (_jumpCancel)
        {
            _physics.VelocityY = (Fix64)_jumpCancelForce;
        }
        else
        {
            _physics.VelocityY = (Fix64)_player.playerStats.jumpForce;
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
