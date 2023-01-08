using UnityEngine;


public class AttackState : State
{
    private static bool opponentInCorner;
    public override void UpdateLogic(PlayerNetwork player)
    {
        player.dashDirection = 0;
        if (!player.enter)
        {
            player.onAttack = false;
            player.animationFrames = 0;
            SetTopPriority(player);
            player.canChainAttack = false;
            player.enter = true;
            player.sound = player.attackNetwork.attackSound;
            player.animation = player.attackNetwork.name;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            opponentInCorner = false;
            if (DemonicsPhysics.IsInCorner(player.otherPlayer))
            {
                opponentInCorner = true;
            }
            player.knockback = 0;
            player.pushbackStart = player.position;
            player.pushbackEnd = new DemonicsVector2(player.position.x + (player.otherPlayer.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
        }
        if (!player.isAir)
        {
            player.velocity = new DemonicsVector2(player.attackNetwork.travelDistance.x * (DemonicsFloat)player.flip, (DemonicsFloat)0);
        }
        else
        {
            player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - (float)DemonicsPhysics.GRAVITY);
        }
        if (GameSimulation.Hitstop <= 0)
        {
            player.animationFrames++;
            player.attackFrames--;

            if (player.canChainAttack)
            {
                player.inPushback = true;
                if (!player.onAttack)
                {
                    // player.inPushback = true;
                    // player.onAttack = true;
                    // player.knockback = 0;
                    // player.pushbackStart = player.position;
                    // player.pushbackEnd = new DemonicsVector2(player.position.x + (player.otherPlayer.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
                }
                // if (player.otherPlayer.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.otherPlayer.attackHurtNetwork.knockbackDuration)
                // {
                //     DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.otherPlayer.attackHurtNetwork.knockbackDuration;
                //     DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
                //     DemonicsVector2 nextPosition = new DemonicsVector2(nextX, player.position.y);
                //     player.position = nextPosition;
                //     player.knockback++;
                // }
                // if (player.inPushback)
                // {
                //     if (!player.isAir)
                //     {
                //         if (player.otherPlayer.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.otherPlayer.attackHurtNetwork.knockbackDuration)
                //         {
                //             DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.otherPlayer.attackHurtNetwork.knockbackDuration;
                //             DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
                //             DemonicsVector2 nextPosition = new DemonicsVector2(nextX, player.position.y);
                //             player.position = nextPosition;
                //             player.knockback++;
                //         }
                //     }
                // }
                if (player.attackPress)
                {
                    if ((!(player.attackInput == InputEnum.Medium && player.isCrouch)))
                    {
                        if (player.inputBuffer.inputItems[0].frame + 20 >= DemonicsWorld.Frame)
                        {
                            Attack(player, player.isAir);
                        }
                    }
                }
                if (player.arcanaPress)
                {
                    if (player.inputBuffer.inputItems[0].frame + 20 >= DemonicsWorld.Frame)
                    {
                        Arcana(player, player.isAir);
                    }
                }
            }

        }
        ToJumpState(player);
        ToJumpForwardState(player);
        ToIdleState(player);
        ToIdleFallState(player);
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.attackNetwork.jumpCancelable)
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
        if (player.attackNetwork.jumpCancelable)
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
            player.inPushback = false;
            player.attackPress = false;
            player.enter = false;
            if (player.isAir)
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
    public override bool ToHurtState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        if (player.attack.hasSuperArmor && !player.player.PlayerAnimator.InRecovery())
        {
            GameSimulation.Hitstop = attack.hitstop;
            player.player.PlayerAnimator.SpriteSuperArmorEffect();
            player.player.SetHealth(player.player.CalculateDamage(attack));
            player.player.StartShakeContact();
            player.player.PlayerUI.Damaged();
            return false;
        }
        if (attack.causesKnockdown)
        {
            player.state = "Airborne";
        }
        else
        {
            if (attack.knockbackArc == 0 || attack.causesSoftKnockdown)
            {
                player.state = "Hurt";
            }
            else
            {
                player.state = "HurtAir";
            }
        }
        return true;
    }
}