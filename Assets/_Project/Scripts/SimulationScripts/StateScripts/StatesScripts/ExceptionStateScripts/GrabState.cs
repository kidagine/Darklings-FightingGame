public class GrabState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            SetTopPriority(player);
            CheckFlip(player);
            player.enter = true;
            player.hitbox.enter = false;
            player.animationFrames = 0;
            player.animation = "Grab";
            player.sound = "Hit";
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
        }
        player.velocity = DemonicsVector2.Zero;
        player.animationFrames++;
        player.attackFrames--;
        ToIdleState(player);
        ToHurtState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.enter = false;
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                player.state = "Grabbed";
                return;
            }
            if (DemonicsPhysics.IsInCorner(player))
            {
                player.otherPlayer.knockback = 0;
                player.otherPlayer.pushbackStart = player.otherPlayer.position;
                player.otherPlayer.pushbackEnd = new DemonicsVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
                player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
            }
            player.player.StopShakeCoroutine();
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            if (player.attackHurtNetwork.hardKnockdown)
            {
                player.state = "Airborne";
            }
            else
            {
                if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                {
                    player.state = "Hurt";
                }
                else
                {
                    player.state = "HurtAir";
                }
            }
        }
    }
}