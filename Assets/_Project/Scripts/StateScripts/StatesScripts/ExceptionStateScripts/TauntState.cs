using UnityEngine;

public class TauntState : State
{
	private IdleState _idleState;

	private void Awake()
	{
		_idleState = GetComponent<IdleState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
		_playerAnimator.Taunt();
	}

	private void ToIdleState()
	{
		_stateMachine.ChangeState(_idleState);
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}
}
