public class GrabState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
            player.player.StopShakeCoroutine();
        }
        player.animation = "Knockdown";
        player.animationFrames++;
    }
}