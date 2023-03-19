using UnityEngine;

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
        if (!player.hitstop)
        {
            player.animationFrames++;
            player.attackFrames--;
        }
        if (player.dashFrames == 7)
        {
            player.invisible = true;
            player.SetEffect("VanishDisappear", player.position);
            GameSimulation.Hitstop = 26;
            HitstopFully(player);
            HitstopFully(player.otherPlayer);
        }
        if (player.dashFrames == 22)
        {
            player.position = new DemonicsVector2(player.otherPlayer.position.x - (22 * player.flip), player.otherPlayer.position.y);
            player.SetEffect("VanishAppear", player.position);
        }
        if (player.dashFrames == 33)
        {
            player.invisible = false;
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
            if (player.position.y > DemonicsPhysics.GROUND_POINT)
            {
                EnterState(player, "Fall");
            }
            else
            {
                EnterState(player, "Idle");
            }
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                EnterState(player, "Grabbed");
                return;
            }
            if (player.attackNetwork.superArmor && !player.player.PlayerAnimator.InRecovery(player.animation, player.animationFrames))
            {
                player.sound = player.attackHurtNetwork.impactSound;
                if (player.attackHurtNetwork.cameraShakerNetwork.timer > 0)
                {
                    CameraShake.Instance.Shake(player.attackHurtNetwork.cameraShakerNetwork);
                }
                player.health -= CalculateDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
                player.healthRecoverable -= CalculateRecoverableDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
                player.canChainAttack = false;
                if (GameSimulation.Hitstop <= 0)
                {
                    GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
                }
                player.player.PlayerAnimator.SpriteSuperArmorEffect();
                player.player.PlayerUI.Damaged();
                player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
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
                    EnterState(player, "BlockLow");
                }
                else
                {
                    EnterState(player, "Block");
                }
            }
            else
            {
                if (player.attackHurtNetwork.hardKnockdown)
                {
                    EnterState(player, "Airborne");
                }
                else
                {
                    if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                    {
                        EnterState(player, "Hurt");
                    }
                    else
                    {
                        EnterState(player, "HurtAir");
                    }
                }
            }
        }
    }
}