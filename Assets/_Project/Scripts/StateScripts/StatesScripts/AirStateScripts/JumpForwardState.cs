using UnityEngine;

public class JumpForwardState : AirParentState
{
	[SerializeField] protected GameObject _jumpPrefab = default;
	private int _jumpX;
	private int _jumpY;
	private int _jumpFrame = 5;
	public override void Enter()
	{
		base.Enter(); 
		Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
		_audio.Sound("Jump").Play();
		_jumpFrame = 5;
		_playerAnimator.JumpForward(true);
		_playerMovement.ResetToWalkSpeed();
		_jumpX = _baseController.InputDirection.x * (_player.playerStats.jumpForce - 8);
		_jumpY = _player.playerStats.jumpForce;
	}


	public override void UpdateLogic()
	{
		base.UpdateLogic();
		if (!DemonicsPhysics.WaitFrames(ref _jumpFrame))
		{
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpY);
		}
		_rigidbody.velocity = new Vector2(_jumpX, _rigidbody.velocity.y);
	}
}
