
public class ThrowState : State
{
	private IdleState _idleState;

	private void Awake()
	{
		_idleState = GetComponent<IdleState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
		_playerAnimator.OnCurrentAnimationFinished.AddListener(() => { _player.OtherPlayerStateManager.TryToKnockdownState(); });
		_playerAnimator.Throw();
	}

	private void ToIdleState()
	{
		_stateMachine.ChangeState(_idleState);
	}

	public override void Exit()
	{
		base.Exit();
	}
}