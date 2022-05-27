using UnityEngine;

public class JumpForwardState : AirParentState
{
    public override void Enter()
    {
        base.Enter();
        Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
        _audio.Sound("Jump").Play();
        _player.SetPushboxTrigger(true);
        _playerAnimator.JumpForward(true);
        _playerMovement.ResetToWalkSpeed();
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(new Vector2(Mathf.Round(_baseController.InputDirection.x) * (_playerStats.PlayerStatsSO.jumpForce / 2.5f), _playerStats.PlayerStatsSO.jumpForce + 1.0f), ForceMode2D.Impulse);
    }
}
