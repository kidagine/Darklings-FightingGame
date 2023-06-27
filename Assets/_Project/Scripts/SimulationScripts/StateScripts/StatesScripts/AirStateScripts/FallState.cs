public class FallState : AirParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            player.enter = true;
            player.animation = "Fall";
            player.animationFrames = 0;
            return;
        }
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        if (!player.usedShadowbreak)
            base.UpdateLogic(player);
        else
            ToHurtState(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.position.y <= DemonicsPhysics.GROUND_POINT)
        {
            player.usedShadowbreak = false;
            player.sound = "Landed";
            player.SetParticle("Fall", player.position);
            EnterState(player, "Idle");
        }
    }
}