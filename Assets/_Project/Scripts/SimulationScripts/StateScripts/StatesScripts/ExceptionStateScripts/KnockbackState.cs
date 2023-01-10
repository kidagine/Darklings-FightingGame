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
            player.pushbackEnd = new DemonicsVector2(player.pushbackStart.x + (20 * -player.flip), player.pushbackStart.y + 20);
        }
        player.velocity = DemonicsVector2.Zero;
        player.animation = "HurtAir";
        player.animationFrames++;

        DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)15;
        DemonicsFloat distance = player.pushbackEnd.x - player.pushbackStart.x;
        DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
        DemonicsFloat baseY = DemonicsFloat.Lerp(player.pushbackStart.y, player.pushbackEnd.y, (nextX - player.pushbackStart.x) / distance);
        DemonicsFloat arc = 5 * (nextX - player.pushbackStart.x) * (nextX - player.pushbackEnd.x) / ((-0.25f) * distance * distance);
        DemonicsVector2 nextPosition = new DemonicsVector2(nextX, baseY + arc);
        player.position = nextPosition;
        player.knockback++;
        ToKnockdown(player, ratio);
    }
    private void ToKnockdown(PlayerNetwork player, DemonicsFloat ratio)
    {
        if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0 && player.knockback > 1)
        {
            player.pushbox.active = true;
            player.enter = false;
            player.state = "HardKnockdown";
        }
    }
}