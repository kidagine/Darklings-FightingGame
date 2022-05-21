using UnityEngine;

public class JumpState : AirParentState
{
    public override void Enter()
    {
        base.Enter();
        Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
        _audio.Sound("Jump").Play();
        _playerAnimator.Jump(true);
        _player.SetPushboxTrigger(true);
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(new Vector2(0.0f, _playerStats.PlayerStatsSO.jumpForce), ForceMode2D.Impulse);
    }
}
