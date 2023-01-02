public class KnockdownSoftState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.SoftKnockdown);
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
        if (player.animationFrames >= 30)
        {
            player.enter = false;
            player.state = "WakeUp";
        }
    }
}