public class BlockLowState : BlockParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.BlockLow();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (DemonicsPhysics.WaitFrames(ref _blockFrame))
        {
            ToCrouchState();
        }
    }

    private void ToCrouchState()
    {
        _stateMachine.ChangeState(_crouchState);
    }
}
