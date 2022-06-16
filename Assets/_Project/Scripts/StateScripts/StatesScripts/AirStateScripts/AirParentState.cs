using UnityEngine;

public class AirParentState : State
{
	[SerializeField] protected GameObject _jumpPrefab = default;
	[SerializeField] protected GameObject _groundedPrefab = default;
	protected FallState _fallState;
	protected JumpState _jumpState;
	protected JumpForwardState _jumpForwardState;
	protected AirDashState _airDashState;
	protected AttackState _attackState;
	protected ArcanaState _arcanaState;
	protected AirHurtState _airHurtState;
	protected AirborneHurtState _airborneHurtState;
	protected BlockAirState _blockAirState;
	protected KnockbackState _knockbackState;

	protected virtual void Awake()
	{
		_fallState = GetComponent<FallState>();
		_jumpState = GetComponent<JumpState>();
		_jumpForwardState = GetComponent<JumpForwardState>();
		_airDashState = GetComponent<AirDashState>();
		_attackState = GetComponent<AttackState>();
		_arcanaState = GetComponent<ArcanaState>();
		_airHurtState = GetComponent<AirHurtState>();
		_airborneHurtState = GetComponent<AirborneHurtState>();
		_blockAirState = GetComponent<BlockAirState>();
		_knockbackState = GetComponent<KnockbackState>();
	}

	public override void Enter()
	{
		base.Enter();
		_player.SetAirPushBox(true);
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		ToFallState();
		ToJumpState();
		ToJumpForwardState();
		ToAirDashState();
	}

	public void ToFallState()
	{
		if (_rigidbody.velocity.y <= 0.0f)
		{
			_stateMachine.ChangeState(_fallState);
		}
	}

	public void ToJumpState()
	{
		if (_playerStats.PlayerStatsSO.canDoubleJump && !_playerMovement.HasDoubleJumped)
		{
			if (_baseController.InputDirection.x == 0.0f)
			{
				if (_baseController.InputDirection.y > 0.0f && !_playerMovement.HasJumped)
				{
					_playerMovement.HasDoubleJumped = true;
					_playerMovement.HasJumped = true;
					_stateMachine.ChangeState(_jumpState);
				}
				else if (_baseController.InputDirection.y <= 0.0f && _playerMovement.HasJumped)
				{
					_playerMovement.HasJumped = false;
				}
			}
		}
	}

	public void ToJumpForwardState()
	{
		if (_playerStats.PlayerStatsSO.canDoubleJump && !_playerMovement.HasDoubleJumped)
		{
			if (_baseController.InputDirection.x != 0.0f)
			{
				if (_baseController.InputDirection.y > 0.0f && !_playerMovement.HasJumped)
				{
					_playerMovement.HasDoubleJumped = true;
					_playerMovement.HasJumped = true;
					_stateMachine.ChangeState(_jumpForwardState);
				}
				else if (_baseController.InputDirection.y <= 0.0f && _playerMovement.HasJumped)
				{
					_playerMovement.HasJumped = false;
				}
			}
		}
	}

	private void ToAirDashState()
	{
		if (!_playerMovement.HasAirDashed)
		{
			if (_baseController.DashForward())
			{
				_airDashState.DashDirection = 1;
				_stateMachine.ChangeState(_airDashState);
			}
			else if (_baseController.DashBackward())
			{
				_airDashState.DashDirection = -1;
				_stateMachine.ChangeState(_airDashState);
			}
		}
	}

	public override bool ToAttackState(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum)
	{
		_attackState.Initialize(inputEnum, false, true);
		_stateMachine.ChangeState(_attackState);
		return true;
	}

	public override bool ToArcanaState(InputDirectionEnum inputDirectionEnum)
	{
		if (_player.Arcana >= 1.0f && _playerComboSystem.GetArcana(true).airOk)
		{
			_arcanaState.Initialize(true);
			_stateMachine.ChangeState(_arcanaState);
			return true;
		}
		return false;
	}

	public override bool ToHurtState(AttackSO attack)
	{
		_airHurtState.Initialize(attack);
		_stateMachine.ChangeState(_airHurtState);
		return true;
	}

	public override bool ToAirborneHurtState(AttackSO attack)
	{
		_airborneHurtState.Initialize(attack);
		_stateMachine.ChangeState(_airborneHurtState);
		return true;
	}

	public override bool ToBlockState(AttackSO attack)
	{
		_blockAirState.Initialize(attack);
		_stateMachine.ChangeState(_blockAirState);
		return true;
	}

	public override bool ToKnockbackState()
	{
		_stateMachine.ChangeState(_knockbackState);
		return true;
	}

	public override bool AssistCall()
	{
		_player.AssistAction();
		return true;
	}
}
