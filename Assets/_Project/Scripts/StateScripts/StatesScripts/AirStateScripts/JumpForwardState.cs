using System.Collections;
using UnityEngine;

public class JumpForwardState : AirParentState
{
	private Coroutine _stopJumpCoroutine;
	private float _jumpX;
	private float _jumpY;
	private bool _stopJump;
	public override void Enter()
	{
		base.Enter(); 
		Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
		_audio.Sound("Jump").Play();
		_playerAnimator.JumpForward(true);
		_playerMovement.ResetToWalkSpeed();
		_jumpX = _baseController.InputDirection.x * (_playerStats.PlayerStatsSO.jumpForce / 2.5f);
		_jumpY = _playerStats.PlayerStatsSO.jumpForce + 1.0f;
		_stopJumpCoroutine = StartCoroutine(Stop());
	}

	IEnumerator Stop()
	{
		yield return new WaitForSeconds(0.03f);
		_stopJump = true;
	}

	public override void UpdatePhysics()
	{
		if (!_stopJump)
		{
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpY);
		}
		_rigidbody.velocity = new Vector2(_jumpX, _rigidbody.velocity.y);

	}
	public override void Exit()
	{
		if (_stopJumpCoroutine != null)
		StopCoroutine(_stopJumpCoroutine);
		_stopJump = false;
	}
}
