using UnityEngine;


public class ArcanaState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            if (player.CurrentState != this)
                return;
            SetTopPriority(player);
            player.dashFrames = 0;
            if (player.attackNetwork.name.Contains("AR"))
            {
                player.arcanaGauge -= PlayerStatsSO.ARCANA_MULTIPLIER;
                DemonVector2 effectPosition = new(player.position.x, player.position.y + 20);
                player.SetParticle("Arcana", effectPosition);
                player.player.PlayerUI.SetDarkScreen(true);
                GameSimulation.GlobalFreezeFrames = 8;
            }
            player.enter = true;
            player.canChainAttack = false;
            player.animation = player.attackNetwork.name;
            player.sound = player.attackNetwork.attackSound;
            player.soundGroup = player.attackNetwork.name[1..];
            player.animationFrames = 0;
            player.hitbox.enter = false;
            if (player.attackNetwork.teleport)
                player.position = new DemonVector2(player.otherPlayer.position.x + player.attackNetwork.teleportPosition.x, player.otherPlayer.position.y + player.attackNetwork.teleportPosition.y);
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            player.velocity = new DemonVector2(player.attackNetwork.travelDistance.x * (DemonFloat)player.flip, (DemonFloat)player.attackNetwork.travelDistance.y);
            player.InitializeProjectile(player.attackNetwork.moveName, player.attackNetwork, player.attackNetwork.projectileSpeed, player.attackNetwork.projectilePriority, player.attackNetwork.projectileDestroyOnHit);
            player.invincible = player.player.PlayerAnimator.GetInvincible(player.animation, player.animationFrames);
            if (player.invincible)
                player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.Reversal);
            if (player.attackNetwork.superArmor > 0)
                player.player.PlayerAnimator.ArmorMaterial();
            UpdateFramedata(player);
            return;
        }
        if (!player.hitstop)
        {
            if (player.attackNetwork.travelDistance.y > 0)
            {
                player.canChainAttack = false;
                player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
                ToIdleFallState(player);
            }
            player.animationFrames++;
            player.attackFrames--;
        }
        if (player.animationFrames % 3 == 0)
        {
            if (player.flip > 0)
            {
                DemonVector2 effectPosition = new DemonVector2(player.position.x - 1, player.position.y);
                player.SetEffect("GhostArcana", player.position, false);
            }
            else
            {
                DemonVector2 effectPosition = new DemonVector2(player.position.x + 1, player.position.y);
                player.SetEffect("GhostArcana", player.position, true);
            }
        }
        player.invincible = player.player.PlayerAnimator.GetInvincible(player.animation, player.animationFrames);
        player.invincible = player.player.PlayerAnimator.GetInvincible(player.animation, player.animationFrames);
        if (player.invincible)
            player.player.PlayerAnimator.InvincibleMaterial();
        else if (player.attackNetwork.superArmor > 0)
            player.player.PlayerAnimator.ArmorMaterial();
        else
            player.player.PlayerAnimator.NormalMaterial();
        UpdateFramedata(player);
        ToIdleState(player);
        Projectile(player);
        ToHurtState(player);
        // if (!player.hitstop)
        //     AttackCancel(player);
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
                               new DemonVector2(player.position.x + (player.attackNetwork.projectilePosition.x * player.flip), player.position.y + player.attackNetwork.projectilePosition.y), false);
                }
                else
                {
                    player.SetProjectile(player.attackNetwork.moveName,
                               new DemonVector2(player.position.x + (player.attackNetwork.projectilePosition.x * player.flip), player.position.y + player.attackNetwork.projectilePosition.y), true);
                }
            }
        }
    }
    private void ToIdleFallState(PlayerNetwork player)
    {
        if (player.isAir && player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonFloat)player.velocity.y <= (DemonFloat)0)
        {
            player.player.PlayerUI.SetDarkScreen(false);
            GameSimulation.GlobalFreezeFrames = 0;
            player.canChainAttack = false;
            player.SetParticle("Fall", player.position);
            player.sound = "Landed";
            CheckTrainingGauges(player);
            player.invincible = false;
            player.dashFrames = 0;
            player.isCrouch = false;
            player.isAir = false;
            EnterState(player, "Idle");
        }
    }

    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            player.player.PlayerUI.SetDarkScreen(false);
            GameSimulation.GlobalFreezeFrames = 0;
            player.canChainAttack = false;
            CheckTrainingGauges(player);
            player.dashFrames = 0;
            if (player.isAir || player.position.y > DemonicsPhysics.GROUND_POINT)
            {
                player.invincible = false;
                player.isCrouch = false;
                player.isAir = false;
                EnterState(player, "Fall");
            }
            else
            {
                if (player.invincible)
                {
                    player.SetParticle("Fall", player.position);
                    player.sound = "Landed";
                }
                player.invincible = false;
                player.isCrouch = false;
                player.isAir = false;
                EnterState(player, "Idle");
            }
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.player.PlayerUI.SetDarkScreen(false);
            GameSimulation.GlobalFreezeFrames = 0;
            player.canChainAttack = false;
            if (player.attackNetwork.superArmor > 0 && !player.player.PlayerAnimator.InRecovery(player.animation, player.animationFrames))
            {
                SuperArmorHurt(player);
                return;
            }
            player.dashFrames = 0;
            player.enter = false;
            if (DemonicsPhysics.IsInCorner(player))
            {
                player.otherPlayer.knockback = 0;
                player.otherPlayer.pushbackStart = player.otherPlayer.position;
                player.otherPlayer.pushbackEnd = new DemonVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
                player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
            }
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
                EnterState(player, "Grabbed");
            if (player.attackHurtNetwork.moveName == "Shadowbreak")
                EnterState(player, "Knockback");
            if (IsBlocking(player))
            {
                if (player.direction.y < 0)
                    EnterState(player, "BlockLow");
                else
                    EnterState(player, "Block");
            }
            else
            {
                player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.Punish);
                if (player.attackHurtNetwork.hardKnockdown)
                    EnterState(player, "Airborne");
                else
                {
                    if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                        if (player.position.y <= DemonicsPhysics.GROUND_POINT)
                            EnterState(player, "Hurt");
                        else
                            EnterState(player, "HurtAir");
                    else
                        EnterState(player, "HurtAir");
                }
            }
        }
    }
}