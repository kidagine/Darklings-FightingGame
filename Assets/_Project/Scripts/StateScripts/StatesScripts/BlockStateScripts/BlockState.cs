using UnityEngine;


public class BlockState : BlockParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Block();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}
