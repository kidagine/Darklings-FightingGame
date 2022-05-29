using UnityEngine;

public class GrabState : State
{
	private IdleState _idleState;
	private ThrowState _throwState;

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_throwState = GetComponent<ThrowState>();
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound("Hit").Play();
		_playerAnimator.Grab();
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
		_player.CurrentAttack = _playerComboSystem.GetThrow();
	}

	private void ToIdleState()
	{
		if (_stateMachine.CurrentState == this)
		{
			_stateMachine.ChangeState(_idleState);
		}
	}

	public override bool ToThrowState()
	{
		_stateMachine.ChangeState(_throwState);
		return true;
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}
}
