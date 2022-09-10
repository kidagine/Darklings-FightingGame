
using UnityEngine;

public class ThrowState : State
{
	private IdleState _idleState;
	private KnockbackState _knockbackState;

	private void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_knockbackState = GetComponent<KnockbackState>();
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
		if (_baseController.InputDirection.x == -1 && transform.root.localScale.x == 1
			|| _baseController.InputDirection.x == 1 && transform.root.localScale.x == -1)
		{
			_player.Flip((int)transform.root.localScale.x * -1);
		}
	}

	private void ToIdleState()
	{
		_stateMachine.ChangeState(_idleState);
	}

	public override bool ToKnockbackState()
	{
		_playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
		_stateMachine.ChangeState(_knockbackState);
		return true;
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}
}