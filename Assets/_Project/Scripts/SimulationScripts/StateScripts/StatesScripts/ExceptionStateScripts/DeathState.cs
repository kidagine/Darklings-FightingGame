using UnityEngine;

public class DeathState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
            player.player.StopShakeCoroutine();
            GameplayManager.Instance.RoundOver(false);
            GameSimulation.IntroFrame = 360;
            player.health = 1;
            player.healthRecoverable = 0;
            player.player.PlayerUI.UpdateHealthDamaged();
            if (!SceneSettings.IsTrainingMode)
            {
                GameSimulation.Run = false;
            }
        }
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        player.animation = "Knockdown";
        player.animationFrames++;
        if (player.animationFrames >= 510)
        {
            if (player.otherPlayer.state != "Taunt")
            {
                player.otherPlayer.enter = false;
                player.otherPlayer.state = "Taunt";
            }
        }
        if (SceneSettings.IsTrainingMode)
        {
            if (player.animationFrames >= 210)
            {
                player.enter = false;
                player.state = "Idle";
            }
        }
        else
        {
            if (player.animationFrames >= 725)
            {
                player.healthRecoverable = 10000;
                player.otherPlayer.healthRecoverable = 10000;
                player.health = 10000;
                player.otherPlayer.health = 10000;
                player.otherPlayer.enter = false;
                player.otherPlayer.state = "Taunt";
                player.enter = false;
                player.state = "Taunt";
            }
        }
    }
}