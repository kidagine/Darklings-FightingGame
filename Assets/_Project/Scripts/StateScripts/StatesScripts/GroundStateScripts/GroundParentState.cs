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
	protected ThrowState _throwState;

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_walkState = GetComponent<WalkState>();
		_crouchState = GetComponent<CrouchState>();
		_jumpState = GetComponent<JumpState>();
		_jumpForwardState = GetComponent<JumpForwardState>();
		_dashState = GetComponent<DashState>();
		_attackState = GetComponent<AttackState>();
		_arcanaState = GetComponent<ArcanaState>();
		_throwState = GetComponent<ThrowState>();
	}

	public override bool ToAttackState(InputEnum inputEnum)
	{
		_attackState.Initialize(inputEnum, false, false);
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

	public override bool ToThrowState()
	{
		_stateMachine.ChangeState(_throwState);
		return true;
	}
}
