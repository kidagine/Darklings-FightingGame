public class FallState : AirParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animation = "Fall";
            player.animationFrames = 0;
            return;
        }
        player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
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
            player.SetParticle("Fall", new DemonVector2(player.position.x, DemonicsPhysics.GROUND_POINT));
            EnterState(player, "Idle");
        }
    }
}