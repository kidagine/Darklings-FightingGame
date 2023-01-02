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
            GameSimulation.Run = false;
            player.health = 1;
        }
        player.animation = "Knockdown";
        player.animationFrames++;
        if (player.animationFrames >= 370)
        {
            player.enter = false;
            player.state = "Idle";
        }
    }
}