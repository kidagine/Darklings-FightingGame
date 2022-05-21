using UnityEngine;

public class AttackState : State
{
	private IdleState _idleState;
	private FallState _fallState;
	public InputEnum InputEnum { get; set; }
	public static bool CanSkipAttack;
	private bool _crouch;
	private bool _air;

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_fallState = GetComponent<FallState>();
	}

	public void Initialize(bool crouch, bool air)
	{
		_crouch = crouch;
		_air = air;
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound("Hit").Play();
		_player.CurrentAttack = _playerComboSystem.GetComboAttack(InputEnum, _crouch, _air);
		_playerAnimator.Attack(_player.CurrentAttack.name, true);
		if (!_air)
		{
			_playerMovement.TravelDistance(new Vector2(_player.CurrentAttack.travelDistance * transform.root.localScale.x, _player.CurrentAttack.travelDirection.y));
		}
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		ToFallState();
	}

	public void ToIdleState()
	{
		if (_playerMovement._isGrounded)
		{
			_stateMachine.ChangeState(_idleState);
		}
	}

	public void ToFallState()
	{
		if (_playerMovement._isGrounded && _rigidbody.velocity.y <= 0.0f && _air)
		{
			_playerAnimator.Jump();
			_stateMachine.ChangeState(_fallState);
		}
	}

	public override bool ToAttackState(InputEnum inputEnum)
	{
		if (CanSkipAttack)
		{
			_stateMachine.ChangeState(this);
			return true;
		}
		else
		{
			return false;
		}
	}

	public override void Exit()
	{
		base.Exit();
		CanSkipAttack = false;
	}
}
