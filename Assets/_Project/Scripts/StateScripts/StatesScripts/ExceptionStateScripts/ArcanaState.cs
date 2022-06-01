using UnityEngine;

public class ArcanaState : State
{
	private IdleState _idleState;
	private FallState _fallState;
	private HurtState _hurtState;
	private GrabbedState _grabbedState;

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_fallState = GetComponent<FallState>();
		_hurtState = GetComponent<HurtState>();
		_grabbedState = GetComponent<GrabbedState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Arcana();
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ArcanaEnd);
		_player.CurrentAttack = _playerComboSystem.GetArcana();
		_player.Arcana--;
		_playerUI.DecreaseArcana();
		_playerUI.SetArcana(_player.Arcana);
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
	}

	private void ArcanaEnd()
	{
		if (_stateMachine.CurrentState == this)
		{
			if (_rigidbody.velocity.y <= 0.0f)
			{
				ToFallState();
			}
			else
			{
				ToIdleState();
			}
		}
	}

	private void ToIdleState()
	{
		_stateMachine.ChangeState(_idleState);
	}

	public void ToFallState()
	{
		_playerAnimator.Jump();
		_stateMachine.ChangeState(_fallState);
	}

	public override bool AssistCall()
	{
		_player.AssistAction();
		return true;
	}

	public override bool ToHurtState(AttackSO attack)
	{
		_hurtState.Initialize(attack);
		_stateMachine.ChangeState(_hurtState);
		return true;
	}

	public override bool ToGrabbedState()
	{
		_stateMachine.ChangeState(_grabbedState);
		return true;
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}
}
