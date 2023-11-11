using UnityEngine;

public class KnockbackState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
            player.knockback = 0;
            player.pushbackStart = player.position;
            player.pushbackEnd = new DemonVector2(player.pushbackStart.x + (20 * -player.flip), player.pushbackStart.y + 20);
            return;
        }
        player.animationFrames++;
        player.velocity = DemonVector2.Zero;
        player.animation = "HurtAir";

        DemonFloat ratio = (DemonFloat)player.knockback / (DemonFloat)10;
        DemonFloat distance = player.pushbackEnd.x - player.pushbackStart.x;
        DemonFloat nextX = DemonFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
        DemonFloat baseY = DemonFloat.Lerp(player.pushbackStart.y, player.pushbackEnd.y, (nextX - player.pushbackStart.x) / distance);
        DemonFloat arc = 5 * (nextX - player.pushbackStart.x) * (nextX - player.pushbackEnd.x) / ((-0.25f) * distance * distance);
        DemonVector2 nextPosition = new DemonVector2(nextX, baseY + arc);
        player.position = nextPosition;
        if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
            player.position = new DemonVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
        else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
            player.position = new DemonVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
        player.knockback++;
        ToKnockdown(player, ratio);
    }
    private void ToKnockdown(PlayerNetwork player, DemonFloat ratio)
    {
        if ((DemonFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonFloat)player.velocity.y <= (DemonFloat)0 && player.knockback > 1)
        {
            player.pushbox.active = true;
            EnterState(player, "HardKnockdown");
        }
    }
}