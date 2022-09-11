using UnityEngine;

public class GrabState : State
{
	private IdleState _idleState;
	private ThrowState _throwState;
	private HurtState _hurtState;
	private AirborneHurtState _airborneHurtState;
	private GrabbedState _grabbedState;

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_throwState = GetComponent<ThrowState>();
		_hurtState = GetComponent<HurtState>();
		_airborneHurtState = GetComponent<AirborneHurtState>();
		_grabbedState = GetComponent<GrabbedState>();
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
		if (_baseController.InputDirection.x == -1 && transform.root.localScale.x == 1
|| _baseController.InputDirection.x == 1 && transform.root.localScale.x == -1)
		{
			_throwState.Initialize(true);
		}
		_stateMachine.ChangeState(_throwState);
		return true;
	}

	public override bool ToHurtState(AttackSO attack)
	{
		_hurtState.Initialize(attack);
		_stateMachine.ChangeState(_hurtState);
		return true;
	}

	public override bool ToAirborneHurtState(AttackSO attack)
	{
		_airborneHurtState.Initialize(attack);
		_stateMachine.ChangeState(_airborneHurtState);
		return true;
	}

	public override bool ToGrabbedState()
	{
		_grabbedState.Initialize(true);
		_stateMachine.ChangeState(_grabbedState);
		return true;
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		_rigidbody.velocity = Vector2.zero;
	}
}
