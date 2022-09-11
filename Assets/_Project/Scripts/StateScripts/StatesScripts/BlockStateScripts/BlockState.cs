public class BlockState : BlockParentState
{
	private int _blockFrame;

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Block();
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		_blockFrame++;
		if (_blockFrame == _blockAttack.hitStun)
		{
			if (_brainController.ControllerInputName == ControllerTypeEnum.Cpu.ToString() && TrainingSettings.OnHit)
			{
				ToAttackState();
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

	private void ToAttackState()
	{
		_attackState.Initialize(InputEnum.Light, false, false);
		_stateMachine.ChangeState(_attackState);
	}
}
