using UnityEngine;

public class ArcanaState : State
{
	private IdleState _idleState;

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Arcana();
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
		_player.CurrentAttack = _playerComboSystem.GetArcana();
		_player.Arcana--;
		_playerUI.DecreaseArcana();
		_playerUI.SetArcana(_player.Arcana);
	}

	private void ToIdleState()
	{
		if (_stateMachine.CurrentState == this)
		{
			_stateMachine.ChangeState(_idleState);
		}
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}
}
