public class WakeUpState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
        }
        player.hurtbox.active = false;
        player.animation = "WakeUp";
        player.animationFrames++;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.animationFrames >= 30)
        {
            player.hurtbox.active = true;
            player.enter = false;
            player.state = "Idle";
        }
    }
}