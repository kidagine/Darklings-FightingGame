public class KnockdownState : State
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
        player.animation = "Knockdown";
        player.animationFrames++;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.animationFrames >= 60)
        {
            player.enter = false;
            player.state = "WakeUp";
        }
    }
}