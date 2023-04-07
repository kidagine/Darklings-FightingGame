using UnityEngine;

public class WallSplatState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            if (player.CurrentState != this)
                player.comboLocked = true;
            player.wasWallSplatted = true;
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
            player.attackHurtNetwork.knockbackArc = 35;
            player.attackHurtNetwork.knockbackDuration = 27;
            return;
        }
        player.animationFrames++;
        player.velocity = DemonicsVector2.Zero;
        player.hurtbox.active = false;
        player.animation = "Wallsplat";
        ToAirborneState(player);
    }
    private void ToAirborneState(PlayerNetwork player)
    {
        if (player.animationFrames >= 15)
        {
            player.flip *= -1;
            player.hurtbox.active = true;
            EnterState(player, "Airborne");
        }
    }
}