using UnityEngine;

public class BlockAirState : BlockParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "BlockAir";
        player.animationFrames++;
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.JUGGLE_GRAVITY);
        ToFallState(player);
        ToIdleState(player);
        ToBlockState(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            player.player.StopShakeCoroutine();
            player.enter = false;
            player.state = "Fall";
        }
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
            {
                player.player.StopShakeCoroutine();
                player.sound = "Landed";
                player.SetEffect("Fall", player.position);
                player.enter = false;
                player.state = "Idle";
            }
        }
    }
    private void ToBlockState(PlayerNetwork player)
    {
        if (player.stunFrames > 0)
        {
            if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
            {
                BlockParentState.skipKnockback = true;
                player.player.StopShakeCoroutine();
                player.sound = "Landed";
                player.SetEffect("Fall", player.position);
                player.enter = true;
                player.state = "Block";
            }
        }
    }

    protected override void OnEnter(PlayerNetwork player)
    {
        base.OnEnter(player);
        end = new DemonicsVector2(player.position.x + (hurtAttack.knockbackForce.x * -player.flip), player.position.y);
    }
    protected override void AfterHitstop(PlayerNetwork player)
    {
        base.AfterHitstop(player);
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - 0.013f);
    }
}