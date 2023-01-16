using UnityEngine;


public class AttackState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        player.dashDirection = 0;
        if (!player.enter)
        {
            player.inputBuffer.inputItems[0].frame = 0;
            player.animationFrames = 0;
            SetTopPriority(player);
            player.canChainAttack = false;
            player.enter = true;
            player.hitbox.enter = false;
            player.sound = player.attackNetwork.attackSound;
            player.animation = player.attackNetwork.name;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
        }
        if (!player.isAir)
        {
            player.velocity = new DemonicsVector2(player.attackNetwork.travelDistance.x * (DemonicsFloat)player.flip, (DemonicsFloat)player.attackNetwork.travelDistance.y);
        }
        else
        {
            player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - (float)DemonicsPhysics.GRAVITY);
        }
        if (!player.hitstop)
        {
            player.animationFrames++;
            player.attackFrames--;
            if (player.canChainAttack)
            {
                if (player.inputBuffer.inputItems[0].frame + 20 >= DemonicsWorld.Frame)
                {
                    if ((DemonicsFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
                    {
                        player.isAir = true;
                    }
                    if (player.inputBuffer.inputItems[0].inputEnum == InputEnum.Special)
                    {
                        Arcana(player, player.isAir);
                    }
                    else
                    {
                        if ((!(player.attackInput == InputEnum.Medium && player.isCrouch)) && player.inputBuffer.inputItems[0].inputEnum != InputEnum.Throw)
                        {
                            Attack(player, player.isAir);
                        }
                    }
                }
            }
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
        ToJumpState(player);
        ToJumpForwardState(player);
        ToIdleState(player);
        ToIdleFallState(player);
        ToHurtState(player);
        Shadow(player);
    }

    private void ToJumpState(PlayerNetwork player)
    {
        if (player.attackNetwork.jumpCancelable && player.canChainAttack || player.isAir && player.canDoubleJump && player.canChainAttack)
        {
            if (player.direction.y > 0)
            {
                player.enter = false;
                player.isCrouch = false;
                player.isAir = false;
                GameSimulation.Hitstop = 0;
                player.state = "Jump";
            }
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.attackNetwork.jumpCancelable && player.canChainAttack || player.isAir && player.canDoubleJump && player.canChainAttack)
        {
            if (player.direction.y > 0 && player.direction.x != 0)
            {
                player.jumpDirection = (int)player.direction.x;
                player.enter = false;
                player.isCrouch = false;
                player.isAir = false;
                GameSimulation.Hitstop = 0;
                player.state = "JumpForward";
            }
        }
    }
    private void ToIdleFallState(PlayerNetwork player)
    {
        if (player.isAir && (DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
        {
            player.inPushback = false;
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
            player.attackPress = false;
            player.enter = false;
            if (player.isAir || (DemonicsFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
            {
                player.isCrouch = false;
                player.isAir = false;
                player.state = "Fall";
            }
            else
            {
                if (player.direction.y < 0)
                {
                    player.isCrouch = false;
                    player.isAir = false;
                    player.state = "Crouch";
                }
                else
                {
                    player.isCrouch = false;
                    player.isAir = false;
                    player.state = "Idle";
                }
            }
        }
    }

    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && IsColliding(player))
        {
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;

            if (player.attackHurtNetwork.moveName == "Shadowbreak")
            {
                player.enter = false;
                player.state = "Knockback";
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
                player.otherPlayer.canChainAttack = true;
                GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
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
                player.enter = false;
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
                player.enter = false;
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