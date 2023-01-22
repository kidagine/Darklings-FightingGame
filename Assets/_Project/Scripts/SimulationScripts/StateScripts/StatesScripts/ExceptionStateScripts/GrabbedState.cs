using UnityEngine;

public class GrabbedState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.otherPlayer.enter = false;
            if (player.otherPlayer.state == "Arcana")
            {
                player.otherPlayer.state = "ArcanaEnd";
            }
            else
            {
                player.otherPlayer.state = "Throw";
            }
            player.enter = true;
            player.animationFrames = 0;
            player.player.StopShakeCoroutine();
        }
        player.velocity = DemonicsVector2.Zero;
        player.animation = "HurtAir";
        player.animationFrames++;
        ThrowBreak(player);
    }

    private void ThrowBreak(PlayerNetwork player)
    {
        if (player.inputBuffer.inputItems[0].pressed && player.inputBuffer.inputItems[0].inputEnum == InputEnum.Throw)
        {
            if (player.otherPlayer.state != "Arcana" && player.animationFrames <= 6)
            {
                player.SetEffect("Impact", new DemonicsVector2((player.position.x + player.otherPlayer.position.x) / 2, player.position.y + (player.pushbox.size.y / 2)));
                player.enter = false;
                player.otherPlayer.enter = false;
                player.state = "Knockback";
                player.otherPlayer.state = "Knockback";
            }
        }
    }
}