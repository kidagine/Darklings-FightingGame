using UnityEngine;

public class BlockState : BlockParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "Block";
        player.animationFrames++;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            BlockParentState.skipKnockback = false;
            player.player.StopShakeCoroutine();
            EnterState(player, "Idle");
        }
    }
    protected override void OnEnter(PlayerNetwork player)
    {
        base.OnEnter(player);
    }
}
