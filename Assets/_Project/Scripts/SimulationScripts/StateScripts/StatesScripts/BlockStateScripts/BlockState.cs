using UnityEngine;

public class BlockState : BlockParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.position = new DemonicsVector2(player.position.x, DemonicsPhysics.GROUND_POINT);
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
            player.enter = false;
            player.state = "Idle";
        }
    }
    protected override void OnEnter(PlayerNetwork player)
    {
        base.OnEnter(player);
    }
}
