using UnityEngine;

public class JumpForwardState : AirParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.animation = "JumpForward";
            player.enter = true;
            player.sound = "Jump";
            if (!player.hasJumped)
                player.SetParticle(player.jumpDirection == 1 ? "JumpLeft" : "JumpRight", new DemonVector2(player.position.x, DemonicsPhysics.GROUND_POINT));
            else
                player.SetParticle("Fall", new DemonVector2(player.position.x, player.position.y + 18));
            player.hasJumped = true;
            player.animationFrames = 0;
            if (player.runJump)
                player.velocity = new DemonVector2((DemonFloat)2.1f * (DemonFloat)player.jumpDirection, player.playerStats.JumpForce);
            else
                player.velocity = new DemonVector2((DemonFloat)1.6f * (DemonFloat)player.jumpDirection, player.playerStats.JumpForce);

            if (player.inputBuffer.CurrentTrigger().frame != 0)
            {
                player.isAir = true;
                player.inputBuffer.triggers[player.inputBuffer.indexTrigger].frame = 0;
                if (player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Special)
                {
                    Arcana(player, player.isAir);
                }
                else
                {
                    if (!(player.attackInput == InputEnum.Medium && player.isCrouch))
                        if (player.inputBuffer.CurrentTrigger().inputEnum != InputEnum.Throw)
                            Attack(player, player.isAir);
                }
            }
            return;
        }
        player.runJump = false;
        player.animationFrames++;
        player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - (float)DemonicsPhysics.GRAVITY);
        ToFallState(player);
        base.UpdateLogic(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.velocity.y <= (DemonFloat)0)
            EnterState(player, "Fall");
    }
}