using UnityEngine;


public class ArcanaState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.dashFrames = 0;
            player.arcanaGauge -= PlayerStatsSO.ARCANA_MULTIPLIER;
            player.enter = true;
            player.canChainAttack = false;
            player.animation = player.attackNetwork.name;
            player.sound = player.attackNetwork.attackSound;
            player.animationFrames = 0;
            player.hitbox.enter = false;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            player.velocity = new DemonicsVector2(player.attackNetwork.travelDistance.x * (DemonicsFloat)player.flip, (DemonicsFloat)player.attackNetwork.travelDistance.y);
            player.InitializeProjectile(player.attackNetwork.moveName, player.attackNetwork, player.attackNetwork.projectileSpeed, player.attackNetwork.projectilePriority, player.attackNetwork.projectileDestroyOnHit);
        }
        ToIdleState(player);
        if (!player.hitstop)
        {
            if (player.attackNetwork.travelDistance.y > 0)
            {
                player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
                ToIdleFallState(player);
            }
            player.animationFrames++;
            player.attackFrames--;
        }
        Projectile(player);
        ToHurtState(player);
    }
    private void Projectile(PlayerNetwork player)
    {
        if (player.dashFrames == 0)
        {
            bool isProjectile = player.player.PlayerAnimator.GetProjectile(player.animation, player.animationFrames);
            if (isProjectile)
            {
                player.dashFrames = 1;
                if (player.flip == 1)
                {
                    player.SetProjectile(player.attackNetwork.moveName,
                               new DemonicsVector2(player.position.x + (player.attackNetwork.projectilePosition.x * player.flip), player.position.y + player.attackNetwork.projectilePosition.y), false);
                }
                else
                {
                    player.SetProjectile(player.attackNetwork.moveName,
                               new DemonicsVector2(player.position.x + (player.attackNetwork.projectilePosition.x * player.flip), player.position.y + player.attackNetwork.projectilePosition.y), true);
                }
            }
        }
    }
    private void ToIdleFallState(PlayerNetwork player)
    {
        if (player.isAir && player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
        {
            player.dashFrames = 0;
            player.isCrouch = false;
            player.isAir = false;
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            player.dashFrames = 0;
            player.enter = false;
            if (player.isAir || player.position.y > DemonicsPhysics.GROUND_POINT)
            {
                player.isCrouch = false;
                player.isAir = false;
                player.state = "Fall";
            }
            else
            {
                player.isCrouch = false;
                player.isAir = false;
                player.state = "Idle";
            }
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && IsColliding(player))
        {
            player.dashFrames = 0;
            player.enter = false;
            if (DemonicsPhysics.IsInCorner(player))
            {
                player.otherPlayer.knockback = 0;
                player.otherPlayer.pushbackStart = player.otherPlayer.position;
                player.otherPlayer.pushbackEnd = new DemonicsVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
                player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
            }
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                player.state = "Grabbed";
                return;
            }
            if (player.attackHurtNetwork.moveName == "Shadowbreak")
            {
                player.enter = false;
                player.state = "Knockback";
                return;
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