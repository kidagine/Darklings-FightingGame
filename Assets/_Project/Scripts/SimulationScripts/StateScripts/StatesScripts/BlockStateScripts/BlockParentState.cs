using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParentState : State
{
    public static bool skipKnockback;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            OnEnter(player);
        }

        if (!player.hitstop)
        {
            AfterHitstop(player);
        }
        ToHurtState(player);
        ToShadowbreakState(player);
    }

    protected virtual void OnEnter(PlayerNetwork player)
    {
        CheckFlip(player);
        player.otherPlayer.canChainAttack = true;
        player.player.StartShakeContact();
        player.enter = true;
        GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
        player.sound = "Block";
        DemonicsVector2 hurtEffectPosition = new DemonicsVector2(player.position.x + (5 * player.flip), player.hurtPosition.y);
        if (player.attackHurtNetwork.hardKnockdown)
        {
            player.SetEffect("Chip", hurtEffectPosition);
            player.health -= 200;
            player.healthRecoverable -= 200;
            player.player.PlayerUI.Damaged();
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
        }
        else
        {
            player.SetEffect("Block", hurtEffectPosition);
        }
        player.animationFrames = 0;
        player.stunFrames = player.attackHurtNetwork.hitStun;
        player.knockback = 0;
        player.pushbackStart = player.position;
        if (player.attackHurtNetwork.hardKnockdown)
        {
            player.attackHurtNetwork.knockbackDuration /= 2;
            player.pushbackEnd = new DemonicsVector2((player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip) / 2), DemonicsPhysics.GROUND_POINT);
        }
        else
        {
            player.pushbackEnd = new DemonicsVector2(player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
        }
        player.velocity = DemonicsVector2.Zero;
    }

    protected virtual void AfterHitstop(PlayerNetwork player)
    {
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        if (player.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.attackHurtNetwork.knockbackDuration)
        {
            DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.attackHurtNetwork.knockbackDuration;
            DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
            DemonicsVector2 nextPosition = DemonicsVector2.Zero;
            nextPosition = new DemonicsVector2(nextX, player.position.y);
            player.position = nextPosition;
            if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
            {
                player.position = new DemonicsVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
            }
            else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
            {
                player.position = new DemonicsVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
            }
            player.knockback++;
        }
        player.player.StopShakeCoroutine();
        player.stunFrames--;
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.enter = false;
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                EnterState(player, "Grabbed");
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
                if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
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
                    EnterState(player, "BlockAir");
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