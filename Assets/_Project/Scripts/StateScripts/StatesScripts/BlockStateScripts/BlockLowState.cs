using System.Collections;
using UnityEngine;

public class BlockLowState : BlockParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.BlockLow();
        _blockCoroutine = StartCoroutine(BlockCoroutine(_blockAttack.hitStun));
    }

    IEnumerator BlockCoroutine(float blockStun)
    {
        yield return new WaitForSeconds(blockStun);
        ToCrouchState();
    }

    private void ToCrouchState()
    {
        _stateMachine.ChangeState(_crouchState);
    }
}
