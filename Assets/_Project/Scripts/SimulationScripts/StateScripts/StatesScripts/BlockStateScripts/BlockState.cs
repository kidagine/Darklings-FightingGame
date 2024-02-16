using UnityEngine;

public class BlockState : BlockParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "Block";
        player.animationFrames++;
        player.velocity = new DemonVector2(player.velocity.x, 0);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            BlockParentState.skipKnockback = false;
            player.player.StopShakeCoroutine();
            CheckTrainingComboEnd(player);
            EnterState(player, "Idle");
        }
    }
    protected override void OnEnter(PlayerNetwork player)
    {
        base.OnEnter(player);
    }
}
