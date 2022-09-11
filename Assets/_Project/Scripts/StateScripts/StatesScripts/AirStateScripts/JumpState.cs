using System.Collections;
using UnityEngine;

public class JumpState : AirParentState
{
	private int _pushboxFrame;
	public override void Enter()
	{
		base.Enter();
		Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
		_audio.Sound("Jump").Play();
		_playerAnimator.Jump(true);
		_rigidbody.velocity = Vector2.zero;
		_rigidbody.AddForce(new Vector2(0, _player.playerStats.jumpForce), ForceMode2D.Impulse);
		_player.SetPushboxTrigger(true);
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_pushboxFrame++;
		if (_pushboxFrame == 2)
		{
			_player.SetPushboxTrigger(false);
		}
	}

	public override void Exit()
	{
		base.Exit();
		_player.SetPushboxTrigger(false);
	}
}
