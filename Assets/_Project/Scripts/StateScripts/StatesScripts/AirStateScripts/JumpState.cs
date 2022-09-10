using System.Collections;
using UnityEngine;

public class JumpState : AirParentState
{
	public override void Enter()
	{
		base.Enter();
		Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
		_audio.Sound("Jump").Play();
		_playerAnimator.Jump(true);
		_rigidbody.velocity = Vector2.zero;
		_rigidbody.AddForce(new Vector2(0, _player.playerStats.jumpForce), ForceMode2D.Impulse);
		StartCoroutine(PushboxCoroutine());
	}

	IEnumerator PushboxCoroutine()
	{
		_player.SetPushboxTrigger(true);
		yield return new WaitForSeconds(0.2f);
		_player.SetPushboxTrigger(false);
	}

	public override void Exit()
	{
		base.Exit();
		_player.SetPushboxTrigger(false);
	}
}
