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
            if (!SceneSettings.IsTrainingMode)
            {
                GameSimulation.Run = false;
            }
        }
        player.animation = "Knockdown";
        player.animationFrames++;
        if (SceneSettings.IsTrainingMode)
        {
            if (player.animationFrames >= 105)
            {
                player.enter = false;
                player.state = "Idle";
            }
        }
        else
        {
            if (player.animationFrames >= 370)
            {
                player.enter = false;
                player.state = "Idle";
            }
        }
    }
}