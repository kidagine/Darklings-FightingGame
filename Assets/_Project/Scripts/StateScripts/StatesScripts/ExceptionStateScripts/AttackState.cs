using UnityEngine;

public class AttackState : State
{
	private IdleState _idleState;
	private CrouchState _crouchState;
	private FallState _fallState;
	private HurtState _hurtState;
	public static bool CanSkipAttack;
	private InputEnum _inputEnum;
	private bool _air;
	private bool _crouch;

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_crouchState = GetComponent<CrouchState>();
		_fallState = GetComponent<FallState>();
		_hurtState = GetComponent<HurtState>();
	}

	public void Initialize(InputEnum inputEnum, bool crouch, bool air)
	{
		_inputEnum = inputEnum;
		_crouch = crouch;
		_air = air;
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound("Hit").Play();
		_player.CurrentAttack = _playerComboSystem.GetComboAttack(_inputEnum, _crouch, _air);
		_playerAnimator.Attack(_player.CurrentAttack.name, true);
		if (!_air)
		{
			_playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
			if (_baseController.Crouch())
			{
				_playerAnimator.OnCurrentAnimationFinished.AddListener(ToCrouchState);
			}
			else
			{
				_playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
			}
			_playerMovement.TravelDistance(new Vector2(
				_player.CurrentAttack.travelDistance * transform.root.localScale.x, _player.CurrentAttack.travelDirection.y));
		}
		else
		{
			_playerAnimator.OnCurrentAnimationFinished.AddListener(ToFallState);
		}
	}

	private void ToIdleState()
	{
		if (_stateMachine.CurrentState == this)
		{
			_stateMachine.ChangeState(_idleState);
		}
	}

	private void ToCrouchState()
	{
		if (_stateMachine.CurrentState == this)
		{
			_stateMachine.ChangeState(_crouchState);
		}
	}
	private void ToFallState()
	{
		if (_stateMachine.CurrentState == this)
		{
			_playerAnimator.Jump();
			_stateMachine.ChangeState(_fallState);
		}
	}

	public override bool ToAttackState(InputEnum inputEnum)
	{
		if (CanSkipAttack)
		{
			Initialize(inputEnum, _crouch, _air);
			_stateMachine.ChangeState(this);
			return true;
		}
		else
		{
			return false;
		}
	}

	public override bool ToHurtState(AttackSO attack)
	{
		_hurtState.Initialize(attack);
		_stateMachine.ChangeState(_hurtState);
		return true;
	}

	public override bool AssistCall()
	{
		_player.AssistAction();
		return true;
	}

	public override void Exit()
	{
		base.Exit();
		CanSkipAttack = false;
	}
}
