public class RedFrenzyState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            SetTopPriority(player);
            CheckFlip(player);
            player.enter = true;
            player.sound = "Vanish";
            player.animationFrames = 0;
            player.animation = "RedFrenzy";
            player.canChainAttack = false;
            player.dashFrames = 0;
            player.healthRecoverable = player.health;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
        }
        player.velocity = DemonicsVector2.Zero;
        player.dashFrames++;
        if (GameSimulation.Hitstop <= 0)
        {
            player.invincible = true;
            player.animationFrames++;
            player.attackFrames--;
        }
        if (player.dashFrames == 7)
        {
            player.SetEffect("VanishDisappear", player.position);
            GameSimulation.Hitstop = 26;
        }
        if (player.dashFrames == 22)
        {
            player.position = new DemonicsVector2(player.otherPlayer.position.x - (22 * player.flip), player.otherPlayer.position.y);
            player.SetEffect("VanishAppear", player.position);
        }
        if (player.dashFrames == 33)
        {
            player.invincible = false;
            player.sound = player.attackNetwork.attackSound;
        }
        ToIdleState(player);
        ToHurtState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            player.dashFrames = 0;
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox) && !player.invincible)
        {
            player.enter = false;
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
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
            if (IsBlocking(player))
            {
                if (player.direction.y < 0)
                {
                    player.state = "BlockLow";
                }
                else
                {
                    player.state = "Block";
                }
            }
            else
            {
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
}