using System.Collections;
using UnityEngine;

public class BlockAirState : BlockParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.BlockAir();
        _blockCoroutine = StartCoroutine(BlockCoroutine(_blockAttack.hitStun));
    }

    IEnumerator BlockCoroutine(float blockStun)
    {
        yield return new WaitForSeconds(blockStun);
        ToFallState();
    }

    private void ToFallState()
    {
        _playerAnimator.Jump();
        _stateMachine.ChangeState(_fallState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}
