using System.Collections;
using UnityEngine;


public class BlockState : BlockParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Block();
        _blockCoroutine = StartCoroutine(BlockCoroutine(_blockAttack.hitStun));
    }

    IEnumerator BlockCoroutine(float blockStun)
    {
        yield return new WaitForSeconds(blockStun);
        ToIdleState();
    }

    private void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }
}
