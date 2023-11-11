using UnityEngine;

public class WallSplatState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.WallSplat);
            CameraShake.Instance.Zoom(-8, 0.2f);
            player.comboLocked = true;
            player.wasWallSplatted = true;
            DemonVector2 effectPosition = new(player.position.x + ((DemonFloat)(-10) * player.flip), player.position.y);
            if (player.flip == 1)
                player.SetParticle("WallSplat", effectPosition, new Vector3(0, 0, 270));
            else
                player.SetParticle("WallSplat", effectPosition, new Vector3(0, 0, 90));
            player.sound = "WallSplat";
            player.enter = true;
            player.animationFrames = 0;
            player.attackHurtNetwork.knockbackArc = 35;
            player.attackHurtNetwork.knockbackDuration = 27;
            return;
        }
        player.animationFrames++;
        player.velocity = DemonVector2.Zero;
        player.hurtbox.active = false;
        player.animation = "Wallsplat";
        ToAirborneState(player);
    }
    private void ToAirborneState(PlayerNetwork player)
    {
        if (player.animationFrames >= 20)
        {
            CameraShake.Instance.ZoomDefault(0.1f);
            player.flip *= -1;
            player.hurtbox.active = true;
            EnterState(player, "Airborne");
        }
    }
}