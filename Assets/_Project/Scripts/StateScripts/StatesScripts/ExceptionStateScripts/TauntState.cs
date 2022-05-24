using UnityEngine;

public class TauntState : GroundParentState
{

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Taunt();
	}


	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}
}
