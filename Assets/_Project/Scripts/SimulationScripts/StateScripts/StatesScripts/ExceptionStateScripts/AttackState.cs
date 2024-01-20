using UnityEngine;

public class AttackState : State
{
    private readonly int _guardBreakSelfDamage = 350;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            if (player.juggleBounce & player.isAir)
            {
                player.position = new DemonVector2(player.position.x, player.position.y + 7);
                player.juggleBounce = false;
            }
            player.attackNetwork.guardBreak = false;
            if (player.attackNetwork.attackType == AttackTypeEnum.Break && !player.isAir)
            {
                if (player.healthRecoverable == player.health)
                {
                    player.attackNetwork.guardBreak = true;
                    if (player.health > _guardBreakSelfDamage)
                        player.health -= _guardBreakSelfDamage;
                    else
                        player.health = 1;
                    player.healthRecoverable -= _guardBreakSelfDamage - 100;
                    player.player.StartShakeContact();
                    player.player.PlayerUI.Damaged();
                    player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
                    DemonVector2 effectPosition = new(player.position.x, player.position.y + 20);
                    player.SetParticle("GuardBreak", effectPosition);
                }
                //CheckTrainingComboEnd(player);
            }
            if (player.attackNetwork.superArmor > 0)
                player.player.PlayerAnimator.ArmorMaterial();
            player.animationFrames = 0;
            SetTopPriority(player);
            player.canChainAttack = false;
            player.enter = true;
            player.hitbox.enter = false;
            player.sound = player.attackNetwork.attackSound;
            player.soundGroup = player.attackNetwork.name[1..];
            player.animation = player.attackNetwork.name;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            UpdateFramedata(player);
            return;
        }
        if (player.animationFrames == 0)
            player.inputBuffer.triggers[player.inputBuffer.indexTrigger].frame = 0;
        if (player.dashFrames <= 0)
            player.dashDirection = 0;
        if (!player.isAir)
            player.velocity = new DemonVector2(player.attackNetwork.travelDistance.x * (DemonFloat)player.flip, (DemonFloat)player.attackNetwork.travelDistance.y);
        else
        {
            if (player.dashFrames > 0)
                Dash(player);
            else
            {
                player.dashDirection = 0;
                player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - (float)DemonicsPhysics.GRAVITY);
            }
        }
        if (!player.hitstop)
            AttackCancel(player);
        if (!player.isAir)
        {
            if (player.pushbackDuration > 0 && player.knockback <= player.pushbackDuration)
            {
                DemonFloat ratio = (DemonFloat)player.knockback / (DemonFloat)player.pushbackDuration;
                DemonFloat nextX = DemonFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
                DemonVector2 nextPosition = new DemonVector2(nextX, player.position.y);
                player.position = nextPosition;
                player.knockback++;
                if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
                    player.position = new DemonVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
                else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
                    player.position = new DemonVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
            }
        }
        UpdateFramedata(player);
        ToJumpForwardState(player);
        ToJumpState(player);
        ToIdleState(player);
        ToIdleFallState(player);
        ToHurtState(player);
        Shadow(player);
    }

    private void Dash(PlayerNetwork player)
    {
        bool forwardDash = player.dashDirection * player.flip == 1;
        int startUpFrames = forwardDash ? 9 : 13;
        int recoveryFrames = forwardDash ? 2 : 3;
        DemonFloat dashforce = forwardDash ? player.playerStats.DashAirForce : player.playerStats.DashBackAirForce;
        if (player.dashFrames < startUpFrames && player.dashFrames > recoveryFrames)
            player.velocity = new DemonVector2(player.dashDirection * dashforce, 0);
        else
            player.velocity = new DemonVector2(player.dashDirection * (dashforce - (DemonFloat)1), 0);
        if (player.dashFrames % 3 == 0)
        {
            if (player.flip > 0)
            {
                DemonVector2 effectPosition = new(player.position.x - 1, player.position.y);
                player.SetEffect("Ghost", player.position, false);
            }
            else
            {
                DemonVector2 effectPosition = new(player.position.x + 1, player.position.y);
                player.SetEffect("Ghost", player.position, true);
            }
        }
        player.dashFrames--;
    }

    private void AttackCancel(PlayerNetwork player)
    {
        player.animationFrames++;
        player.attackFrames--;
        if (player.canChainAttack)
        {
            InputItemNetwork input = player.inputBuffer.CurrentTrigger();
            if (input.frame == 0)
                return;

            if (input.inputEnum == InputEnum.Throw)
                return;
            if ((DemonFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
                player.isAir = true;
            if (!player.isAir)
            {
                bool crouching = player.attackNetwork.name.Contains("2");
                if (player.attackInput != InputEnum.Heavy && !crouching)
                {
                    if ((int)input.inputEnum == (int)player.attackInput && input.crouch == crouching)
                        return;
                    if ((int)input.inputEnum < (int)player.attackInput)
                        return;
                }
            }

            if (input.inputEnum == InputEnum.Special)
                Arcana(player, player.isAir);
            else
            {
                if (input.inputEnum == InputEnum.Heavy && player.attackInput != InputEnum.Light)
                    return;
                if (!(player.attackInput == InputEnum.Medium && player.isCrouch))
                    if (!(player.attackInput == InputEnum.Heavy && !player.isCrouch && input.inputEnum == InputEnum.Heavy && player.direction.y >= 0))
                        Attack(player, player.isAir);

            }

        }
    }

    private void ToJumpState(PlayerNetwork player)
    {
        if (player.attackNetwork.jumpCancelable && player.canChainAttack || player.isAir && player.canDoubleJump && player.canChainAttack && !player.hasJumped)
        {
            if (player.direction.y > 0)
            {
                if (player.isAir)
                    player.canDoubleJump = false;
                player.juggleBounce = true;
                player.isCrouch = false;
                player.isAir = false;
                player.canChainAttack = false;
                GameSimulation.Hitstop = 0;
                EnterState(player, "Jump");
            }
        }
        else if (player.direction.y <= 0 && player.hasJumped)
            player.hasJumped = false;
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.attackNetwork.jumpCancelable && player.canChainAttack || player.isAir && player.canDoubleJump && player.canChainAttack && !player.hasJumped)
        {
            if (player.direction.y > 0 && player.direction.x != 0)
            {
                if (player.isAir)
                    player.canDoubleJump = false;
                player.jumpDirection = (int)player.direction.x;
                player.juggleBounce = true;
                player.isCrouch = false;
                player.isAir = false;
                player.canChainAttack = false;
                GameSimulation.Hitstop = 0;
                EnterState(player, "JumpForward");
            }
        }
        else if (player.direction.y <= 0 && player.hasJumped)
            player.hasJumped = false;
    }
    private void ToIdleFallState(PlayerNetwork player)
    {
        if (!player.hitstop)
            if (player.isAir && (DemonFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonFloat)player.velocity.y <= (DemonFloat)0)
            {
                player.canChainAttack = false;
                player.SetParticle("Fall", player.position);
                player.sound = "Landed";
                player.inPushback = false;
                player.isCrouch = false;
                player.isAir = false;
                EnterState(player, "Idle");
            }
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            if (player.isAir || (DemonFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
            {
                player.canChainAttack = false;
                player.isCrouch = false;
                player.isAir = false;
                EnterState(player, "Fall");
            }
            else
            {
                if (player.direction.y < 0)
                {
                    player.canChainAttack = false;
                    player.isCrouch = false;
                    player.isAir = false;
                    EnterState(player, "Crouch");
                }
                else
                {
                    player.canChainAttack = false;
                    player.isCrouch = false;
                    player.isAir = false;
                    EnterState(player, "Idle");
                }
            }
        }
    }

    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.canChainAttack = false;
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                EnterState(player, "Grabbed");
                return;
            }
            if (player.attackHurtNetwork.moveName == "Shadowbreak")
            {
                EnterState(player, "Knockback");
                return;
            }
            if (player.attackNetwork.superArmor > 0 && !player.player.PlayerAnimator.InRecovery(player.animation, player.animationFrames))
            {
                SuperArmorHurt(player);
                return;
            }
            if (DemonicsPhysics.IsInCorner(player))
            {
                player.otherPlayer.knockback = 0;
                player.otherPlayer.pushbackStart = player.otherPlayer.position;
                player.otherPlayer.pushbackEnd = new DemonVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
                player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
            }

            player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.Punish);
            if (player.attackHurtNetwork.hardKnockdown)
            {
                EnterState(player, "Airborne");
            }
            else
            {
                if ((DemonFloat)player.position.y <= DemonicsPhysics.GROUND_POINT)
                {
                    if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                        EnterState(player, "Hurt");
                    else
                        EnterState(player, "HurtAir");
                }
                else
                    EnterState(player, "HurtAir");
            }
        }
    }
}