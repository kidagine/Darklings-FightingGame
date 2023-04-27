using UnityEngine;

public class AttackState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            if (player.juggleBounce & player.isAir)
            {
                player.position = new DemonicsVector2((DemonicsFloat)player.position.x, (DemonicsFloat)player.position.y + 7);
                player.juggleBounce = false;
            }
            player.animationFrames = 0;
            SetTopPriority(player);
            player.canChainAttack = false;
            player.enter = true;
            player.hitbox.enter = false;
            player.sound = player.attackNetwork.attackSound;
            player.animation = player.attackNetwork.name;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            UpdateFramedata(player);
            return;
        }
        if (player.animationFrames == 0)
            player.inputBuffer.inputItems[player.inputBuffer.index].frame = 0;
        if (player.dashFrames <= 0)
        {
            player.dashDirection = 0;
        }
        if (!player.isAir)
        {
            player.velocity = new DemonicsVector2(player.attackNetwork.travelDistance.x * (DemonicsFloat)player.flip, (DemonicsFloat)player.attackNetwork.travelDistance.y);
        }
        else
        {
            if (player.dashFrames > 0)
                Dash(player);
            else
            {
                player.dashDirection = 0;
                player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - (float)DemonicsPhysics.GRAVITY);
            }
        }
        if (!player.hitstop)
        {
            AttackCancel(player);
        }
        if (!player.isAir)
        {
            if (player.pushbackDuration > 0 && player.knockback <= player.pushbackDuration)
            {
                DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.pushbackDuration;
                DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
                DemonicsVector2 nextPosition = new DemonicsVector2(nextX, player.position.y);
                player.position = nextPosition;
                player.knockback++;
                if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
                {
                    player.position = new DemonicsVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
                }
                else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
                {
                    player.position = new DemonicsVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
                }
            }
        }
        UpdateFramedata(player);
        ToJumpState(player);
        ToJumpForwardState(player);
        ToIdleState(player);
        ToIdleFallState(player);
        ToHurtState(player);
        Shadow(player);
    }

    private void Dash(PlayerNetwork player)
    {
        bool forwardDash = player.dashDirection * player.flip == 1 ? true : false;
        int startUpFrames = forwardDash ? 9 : 13;
        int recoveryFrames = forwardDash ? 2 : 3;
        DemonicsFloat dashforce = forwardDash ? player.playerStats.DashAirForce : player.playerStats.DashBackAirForce;
        if (player.dashFrames < startUpFrames && player.dashFrames > recoveryFrames)
        {
            player.velocity = new DemonicsVector2(player.dashDirection * dashforce, 0);
        }
        else
        {
            player.velocity = new DemonicsVector2(player.dashDirection * (dashforce - (DemonicsFloat)1), 0);
        }
        if (player.dashFrames % 3 == 0)
        {
            if (player.flip > 0)
            {
                DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x - 1, player.position.y);
                player.SetEffect("Ghost", player.position, false);
            }
            else
            {
                DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x + 1, player.position.y);
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
            InputItemNetwork input = player.inputBuffer.CurrentInput();
            if (input.frame != 0)
            {
                if ((DemonicsFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
                {
                    player.isAir = true;
                }
                if (input.inputEnum == InputEnum.Special)
                {
                    Arcana(player, player.isAir);
                }
                else
                {
                    if (input.inputEnum == InputEnum.Heavy && input.inputDirection != InputDirectionEnum.Down && player.attackInput != InputEnum.Light)
                        return;
                    if (!(player.attackInput == InputEnum.Medium && player.isCrouch))
                    {
                        if (input.inputEnum != InputEnum.Throw)
                        {
                            if (!(player.attackInput == InputEnum.Heavy && !player.isCrouch && input.inputEnum == InputEnum.Heavy && player.direction.y >= 0))
                            {
                                Attack(player, player.isAir);
                            }
                        }
                    }
                }
            }
        }
    }

    private void ToJumpState(PlayerNetwork player)
    {
        if (player.attackNetwork.jumpCancelable && player.canChainAttack || player.isAir && player.canDoubleJump && player.canChainAttack)
        {
            if (player.direction.y > 0)
            {
                if (player.isAir)
                {
                    player.canDoubleJump = false;
                }
                player.juggleBounce = true;
                player.isCrouch = false;
                player.isAir = false;
                GameSimulation.Hitstop = 0;
                EnterState(player, "Jump");
            }
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.attackNetwork.jumpCancelable && player.canChainAttack || player.isAir && player.canDoubleJump && player.canChainAttack)
        {
            if (player.direction.y > 0 && player.direction.x != 0)
            {
                if (player.isAir)
                {
                    player.canDoubleJump = false;
                }
                player.jumpDirection = (int)player.direction.x;
                player.juggleBounce = true;
                player.isCrouch = false;
                player.isAir = false;
                GameSimulation.Hitstop = 0;
                EnterState(player, "JumpForward");
            }
        }
    }
    private void ToIdleFallState(PlayerNetwork player)
    {
        if (player.isAir && (DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
        {
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
            if (player.isAir || (DemonicsFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
            {
                player.isCrouch = false;
                player.isAir = false;
                EnterState(player, "Fall");
            }
            else
            {
                if (player.direction.y < 0)
                {
                    player.isCrouch = false;
                    player.isAir = false;
                    EnterState(player, "Crouch");
                }
                else
                {
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


            if (player.attackHurtNetwork.hardKnockdown)
            {
                EnterState(player, "Airborne");
            }
            else
            {
                if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT)
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
                else
                {
                    EnterState(player, "HurtAir");
                }
            }
        }
    }
}