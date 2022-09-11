using FixMath.NET;
using System.Collections;
using UnityEngine;

public class JumpForwardState : AirParentState
{
	[SerializeField] protected GameObject _jumpPrefab = default;
	private Coroutine _stopJumpCoroutine;
	private Fix64 _jumpX;
	private int _jumpY;
	private bool _stopJump;
	public override void Enter()
	{
		base.Enter(); 
		Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
		_audio.Sound("Jump").Play();
		_playerAnimator.JumpForward(true);
		_playerMovement.ResetToWalkSpeed();
		_jumpX = (Fix64)_baseController.InputDirection.x * ((Fix64)_player.playerStats.jumpForce / (Fix64)2.5f);
		_jumpY = _player.playerStats.jumpForce + 1;
		_stopJumpCoroutine = StartCoroutine(Stop());
	}

	IEnumerator Stop()
	{
		yield return new WaitForSeconds(0.03f);
		_stopJump = true;
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		if (!_stopJump)
		{
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpY);
		}
		_rigidbody.velocity = new Vector2((float)_jumpX, _rigidbody.velocity.y);

	}
	public override void Exit()
	{
		if (_stopJumpCoroutine != null)
		StopCoroutine(_stopJumpCoroutine);
		_stopJump = false;
	}
}
