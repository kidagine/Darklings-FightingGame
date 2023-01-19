using UnityEngine;

public class BlockLowState : BlockParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.position = new DemonicsVector2(player.position.x, DemonicsPhysics.GROUND_POINT);
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
            if (player.direction.y < 0)
            {
                player.state = "Crouch";
            }
            else
            {
                player.state = "Idle";
            }
        }
    }
}
