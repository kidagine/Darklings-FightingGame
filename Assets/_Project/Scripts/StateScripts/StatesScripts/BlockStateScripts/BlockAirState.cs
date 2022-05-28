using UnityEngine;

public class BlockAirState : BlockParentState
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.BlockAir();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}
