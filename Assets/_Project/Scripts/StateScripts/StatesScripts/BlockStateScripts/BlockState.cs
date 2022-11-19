public class BlockState : BlockParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Block();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (DemonicsWorld.WaitFrames(ref _blockFrame))
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

    private new void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    private void ToAttackState()
    {
        _attackState.Initialize(InputEnum.Light, false, false);
        _stateMachine.ChangeState(_attackState);
    }
}
