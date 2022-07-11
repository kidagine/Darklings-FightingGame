using UnityEngine;

public class ArcanaState : State
{
	private IdleState _idleState;
	private FallState _fallState;
	private HurtState _hurtState;
	private AirborneHurtState _airborneHurtState;
	private GrabbedState _grabbedState;
	private ArcanaThrowState _arcanaThrowState;
	private bool _crouch;
	private bool _air;

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_fallState = GetComponent<FallState>();
		_hurtState = GetComponent<HurtState>();
		_airborneHurtState = GetComponent<AirborneHurtState>();
		_grabbedState = GetComponent<GrabbedState>();
		_arcanaThrowState = GetComponent<ArcanaThrowState>();
	}

	public void Initialize(bool crouch = false, bool air = false)
	{
		_crouch = crouch;
		_air = air;
	}

	public override void Enter()
	{
		base.Enter();
		if (_playerComboSystem.GetArcana(_crouch, _air).reversal)
		{
			_playerUI.DisplayNotification(NotificationTypeEnum.Reversal);
		}
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ArcanaEnd);
		_player.CurrentAttack = _playerComboSystem.GetArcana(_crouch, _air);
		_playerAnimator.Arcana(_player.CurrentAttack.name);
		_player.Arcana--;
		_playerUI.DecreaseArcana();
		_playerUI.SetArcana(_player.Arcana);
		_playerMovement.TravelDistance(new Vector2(
				_player.CurrentAttack.travelDistance * transform.root.localScale.x, 0.0f));
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
	}

	private void ArcanaEnd()
	{
		if (_stateMachine.CurrentState == this)
		{
			if (!_playerMovement.IsGrounded)
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
		_player.OtherPlayerUI.DisplayNotification(NotificationTypeEnum.Punish);
		_hurtState.Initialize(attack);
		_stateMachine.ChangeState(_hurtState);
		return true;
	}

	public override bool ToAirborneHurtState(AttackSO attack)
	{
		_player.OtherPlayerUI.DisplayNotification(NotificationTypeEnum.Punish);
		_airborneHurtState.Initialize(attack);
		_stateMachine.ChangeState(_airborneHurtState);
		return true;
	}

	public override bool ToGrabbedState()
	{
		_stateMachine.ChangeState(_grabbedState);
		return true;
	}

	public override bool ToThrowState()
	{
		_stateMachine.ChangeState(_arcanaThrowState);
		return true;
	}


	public override void UpdatePhysics()
	{
		if (_player.CurrentAttack.travelDistance == 0.0f)
		{
			base.UpdatePhysics();
			_rigidbody.velocity = Vector2.zero;
		}
	}
}
