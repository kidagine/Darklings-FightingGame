using UnityEngine;

public class BlockLowState : BlockParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.BlockLow();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}
