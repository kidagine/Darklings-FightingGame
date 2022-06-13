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
        if (_brainController.ControllerInputName == ControllerTypeEnum.Cpu.ToString() && TrainingSettings.OnHit)
        {
            ToAttackState();
        }
        else
        {
            ToIdleState();
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
