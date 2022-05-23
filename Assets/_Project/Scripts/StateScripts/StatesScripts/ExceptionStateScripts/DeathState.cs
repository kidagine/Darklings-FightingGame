using UnityEngine;

public class DeathState : State
{
	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Knockdown();
		_player.SetHurtbox(false);
		_player.SetPushboxTrigger(true);
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
	}
}