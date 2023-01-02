using UnityEngine;

public class BlockLowState : BlockParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "BlockLow";
        player.animationFrames++;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            player.player.StopShakeCoroutine();
            player.enter = false;
            player.state = "Idle";
        }
    }
    protected override void OnEnter(PlayerNetwork player)
    {
        base.OnEnter(player);
        end = new DemonicsVector2(player.position.x + (hurtAttack.knockbackForce.x * -player.flip), DemonicsPhysics.GROUND_POINT - 0.5f);
    }
}
