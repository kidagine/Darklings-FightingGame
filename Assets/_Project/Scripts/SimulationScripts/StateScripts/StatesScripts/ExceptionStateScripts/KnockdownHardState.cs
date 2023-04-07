using Demonics;
using UnityEngine;

public class KnockdownHardState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.Knockdown);
            player.enter = true;
            player.animationFrames = 0;
            player.sound = "Landed";
            player.SetEffect("Fall", player.position);
            player.framedataEnum = FramedataTypesEnum.Knockdown;
            return;
        }
        player.animationFrames++;
        player.framedataEnum = FramedataTypesEnum.Knockdown;
        player.hurtbox.active = false;
        player.animation = "Knockdown";
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.animationFrames >= 60)
        {
            EnterState(player, "WakeUp");
        }
    }
}