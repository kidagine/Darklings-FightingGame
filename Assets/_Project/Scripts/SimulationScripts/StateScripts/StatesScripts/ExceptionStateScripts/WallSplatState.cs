using UnityEngine;

public class WallSplatState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            Vector2 effectPosition = new Vector2(player.position.x + (2.25f * player.flip), player.position.y);
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
        player.velocity = Vector2.zero;
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