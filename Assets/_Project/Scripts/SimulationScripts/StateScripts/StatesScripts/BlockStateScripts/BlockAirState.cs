using UnityEngine;

public class BlockAirState : BlockParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "BlockAir";
        player.animationFrames++;
        ToFallState(player);
    }
    protected override void AfterHitstop(PlayerNetwork player)
    {
        base.AfterHitstop(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0 && player.knockback > 1)
        {
            player.player.StopShakeCoroutine();
            if (player.stunFrames <= 0)
            {
                player.enter = false;
                player.state = "Idle";
            }
            else
            {
                player.stunFrames = player.attackHurtNetwork.hitStun;
                player.velocity = DemonicsVector2.Zero;
                player.animationFrames = 0;
                player.state = "Block";
            }
        }
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            player.enter = false;
            player.state = "Fall";
        }
    }
    protected override void OnEnter(PlayerNetwork player)
    {
        base.OnEnter(player);
    }
}