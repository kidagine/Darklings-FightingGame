using UnityEngine;

public class ShadowbreakState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            SetTopPriority(player);
            CheckFlip(player);
            player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.Knockdown);
            player.enter = true;
            player.animationFrames = 0;
            player.sound = "Landed";
            player.SetEffect("Fall", player.position);
        }
        player.hurtbox.active = false;
        player.animation = "HurtAir";
        player.animationFrames++;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.animationFrames >= 60)
        {
            player.enter = false;
            player.state = "WakeUp";
        }
    }
}