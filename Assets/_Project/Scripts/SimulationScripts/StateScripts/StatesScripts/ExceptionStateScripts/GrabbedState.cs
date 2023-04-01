using UnityEngine;

public class GrabbedState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (player.enter)
        {
            player.animationFrames++;
        }
        if (!player.enter)
        {
            if (player.otherPlayer.state == "Arcana")
            {
                EnterState(player.otherPlayer, "ArcanaEnd");
            }
            else
            {
                EnterState(player.otherPlayer, "Throw");
            }
            player.enter = true;
            player.animationFrames = 0;
            player.player.StopShakeCoroutine();
        }
        player.velocity = DemonicsVector2.Zero;
        player.animation = "HurtAir";
        ThrowBreak(player);
    }

    private void ThrowBreak(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentInput().pressed && player.inputBuffer.CurrentInput().inputEnum == InputEnum.Throw)
        {
            if (player.otherPlayer.state != "Arcana" && player.animationFrames <= 6)
            {
                player.sound = "ParryStart";
                player.SetEffect("Impact", new DemonicsVector2((player.position.x + player.otherPlayer.position.x) / 2, player.position.y + (player.pushbox.size.y / 2)));
                EnterState(player, "Knockback");
                EnterState(player.otherPlayer, "Knockback");
            }
        }
    }
}