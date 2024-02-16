using UnityEngine;

public class BlockLowState : BlockParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "BlockLow";
        player.animationFrames++;
        player.velocity = new DemonVector2(player.velocity.x, 0);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            player.player.StopShakeCoroutine();
            CheckTrainingComboEnd(player);
            if (player.direction.y < 0)
                EnterState(player, "Crouch");
            else
                EnterState(player, "Idle");
        }
    }
}
