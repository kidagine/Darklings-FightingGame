using UnityEngine;

public class WallSplatState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x + ((DemonicsFloat)2.25 * player.flip), player.position.y);
            if (player.flip == 1)
            {
                player.SetEffect("WallSplat", effectPosition, true);
            }
            else
            {
                player.SetEffect("WallSplat", effectPosition, false);
            }
            player.sound = "WallSplat";
            player.enter = true;
            player.animationFrames = 0;
        }
        player.velocity = DemonicsVector2.Zero;
        player.hurtbox.active = false;
        player.animation = "Wallsplat";
        player.animationFrames++;
        ToAirborneState(player);
    }
    private void ToAirborneState(PlayerNetwork player)
    {
        if (player.animationFrames >= 10)
        {
            player.wasWallSplatted = true;
            player.hurtbox.active = true;
            player.enter = false;
            player.state = "Airborne";
        }
    }
}