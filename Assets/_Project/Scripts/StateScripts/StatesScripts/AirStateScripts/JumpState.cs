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
        _rigidbody.velocity = Vector2.zero;
        if (_jumpCancel)
        {
            _rigidbody.AddForce(new Vector2(0, _jumpCancelForce), ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody.AddForce(new Vector2(0, _player.playerStats.jumpForce), ForceMode2D.Impulse);
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
