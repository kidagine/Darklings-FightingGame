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

        if (GameSimulation.Hitstop <= 0)
        {
            AfterHitstop(player);
        }
        ToHurtState(player);
    }

    protected virtual void OnEnter(PlayerNetwork player)
    {
        player.otherPlayer.canChainAttack = true;
        player.player.StartShakeContact();
        player.enter = true;
        GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
        player.sound = "Block";
        DemonicsVector2 hurtEffectPosition = new DemonicsVector2(player.position.x + (5 * player.flip), player.otherPlayer.hitbox.position.y);
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
        player.pushbackEnd = new DemonicsVector2(player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
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
            player.knockback++;
        }
        player.player.StopShakeCoroutine();
        player.stunFrames--;
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            player.enter = false;
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
            if (IsBlocking(player))
            {
                if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
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
                    player.state = "BlockAir";
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