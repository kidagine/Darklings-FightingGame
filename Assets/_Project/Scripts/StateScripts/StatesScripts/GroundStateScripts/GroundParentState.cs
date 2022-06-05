
using UnityEngine;

public class GroundParentState : State
{
	protected IdleState _idleState;
	protected WalkState _walkState;
	protected CrouchState _crouchState;
	protected JumpState _jumpState;
	protected JumpForwardState _jumpForwardState;
	protected DashState _dashState;
	protected AttackState _attackState;
	protected ArcanaState _arcanaState;
	protected GrabState _grabState;
	protected HurtState _hurtState;
	protected BlockState _blockState;
	protected BlockLowState _blockLowState;
	protected GrabbedState _grabbedState;
	protected KnockbackState _knockbackState;
	protected TauntState _tauntState;

	protected virtual void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_walkState = GetComponent<WalkState>();
		_crouchState = GetComponent<CrouchState>();
		_jumpState = GetComponent<JumpState>();
		_jumpForwardState = GetComponent<JumpForwardState>();
		_dashState = GetComponent<DashState>();
		_attackState = GetComponent<AttackState>();
		_arcanaState = GetComponent<ArcanaState>();
		_grabState = GetComponent<GrabState>();
		_hurtState = GetComponent<HurtState>();
		_blockState = GetComponent<BlockState>();
		_blockLowState = GetComponent<BlockLowState>();
		_grabbedState = GetComponent<GrabbedState>();
		_knockbackState = GetComponent<KnockbackState>();
		_tauntState = GetComponent<TauntState>();
	}

	public override bool ToAttackState(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum)
	{
		if (inputDirectionEnum == InputDirectionEnum.Down || _baseController.Crouch())
		{
			_attackState.Initialize(inputEnum, true, false);
		}
		else
		{
			_attackState.Initialize(inputEnum, false, false);
		}
		_stateMachine.ChangeState(_attackState);
		return true;
	}

	public override bool ToArcanaState()
	{
		if (_player.Arcana >= 1.0f)
		{
			_stateMachine.ChangeState(_arcanaState);
			return true;
		}
		return false;
	}

	public override bool ToGrabState()
	{
		_stateMachine.ChangeState(_grabState);
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

	public override bool ToKnockbackState()
	{
		if (transform.localScale.x == 1.0f && _playerMovement.MovementInput.x < 0.0f
			   || transform.localScale.x == -1.0f && _playerMovement.MovementInput.x > 0.0f)
		{
			_stateMachine.ChangeState(_blockState);
			return false;
		}
		else
		{
			_stateMachine.ChangeState(_knockbackState);
			return true;
		}
	}

	public override bool ToTauntState()
	{
		_stateMachine.ChangeState(_tauntState);
		return true;
	}

	public override bool AssistCall()
	{
		_player.AssistAction();
		return true;
	}
}
