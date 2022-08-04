using System.Collections;
using UnityEngine;

public class GiveUpState : State
{
	public override void Enter()
	{
		base.Enter();
		_player.CheckFlip();
		_playerAnimator.Knockdown();
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}
}
