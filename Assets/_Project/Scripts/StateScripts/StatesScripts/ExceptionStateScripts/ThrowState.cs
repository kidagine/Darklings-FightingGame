
using UnityEngine;

public class ThrowState : State
{
	private IdleState _idleState;

	private void Awake()
	{
		_idleState = GetComponent<IdleState>();
	}

	public override void Enter()
	{
		base.Enter();
		CheckThrowDirection();
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
		_playerAnimator.OnCurrentAnimationFinished.AddListener(() => { _player.OtherPlayerStateManager.TryToKnockdownState(); });
		_playerAnimator.Throw();
	}

	private void CheckThrowDirection()
	{
		if (_baseController.InputDirection.x == -1.0f && transform.root.localScale.x == 1.0f
			|| _baseController.InputDirection.x == 1.0f && transform.root.localScale.x == -1.0f)
		{
			_player.Flip((int)transform.root.localScale.x * -1);
		}
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