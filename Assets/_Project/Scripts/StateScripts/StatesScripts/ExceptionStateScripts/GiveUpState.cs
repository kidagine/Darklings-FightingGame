using UnityEngine;

public class GiveUpState : State
{
	public override void Enter()
	{
		base.Enter();
		_player.CheckFlip();
		_playerAnimator.Knockdown();
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		_rigidbody.velocity = Vector2.zero;
	}
}
