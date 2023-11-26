using UnityEngine;

public class GrabbedState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            CameraShake.Instance.Zoom(20, 0.05f);
            if (player.otherPlayer.state == "Arcana")
                EnterState(player.otherPlayer, "ArcanaEnd");
            else
                EnterState(player.otherPlayer, "Throw");
            player.animation = "HurtAir";
            player.enter = true;
            player.animationFrames = 0;
            player.player.StopShakeCoroutine();
            return;
        }
        player.animationFrames++;
        player.velocity = DemonVector2.Zero;
        ThrowBreak(player);
    }

    private void ThrowBreak(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed && player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Throw)
        {
            if (player.otherPlayer.state != "Arcana" && player.animationFrames <= 6)
            {
                CameraShake.Instance.ZoomDefault(0.1f);
                player.SetParticle("ThrowTech", new DemonVector2((player.position.x + player.otherPlayer.position.x) / 2, player.position.y + (player.pushbox.size.y / 2)));
                player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.ThrowBreak);
                player.sound = "ParryStart";
                // player.SetEffect("Impact", new DemonicsVector2((player.position.x + player.otherPlayer.position.x) / 2, player.position.y + (player.pushbox.size.y / 2)));
                EnterState(player, "Knockback");
                EnterState(player.otherPlayer, "Knockback");
            }
        }
    }
}