public class WakeUpState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            player.animation = "WakeUp";
            player.enter = true;
            player.animationFrames = 0;
            return;
        }
        player.hurtbox.active = false;
        player.animationFrames++;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.animationFrames >= DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation))
        {
            player.hurtbox.active = true;
            if (AIHurt(player))
                return;
            EnterState(player, "Idle");
        }
    }
}