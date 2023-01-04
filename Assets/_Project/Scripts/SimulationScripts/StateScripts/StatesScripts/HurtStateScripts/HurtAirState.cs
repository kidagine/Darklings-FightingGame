using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAirState : HurtParentState
{
    public static DemonicsVector2 start;
    private static DemonicsVector2 end;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.velocity = DemonicsVector2.Zero;
            CheckFlip(player);
            player.health -= player.otherPlayer.attack.damage;
            player.player.SetHealth(player.otherPlayer.attack.damage);
            player.player.StartShakeContact();
            player.player.PlayerUI.Damaged();
            player.player.OtherPlayerUI.IncreaseCombo();
            player.enter = true;
            GameSimulation.Hitstop = player.otherPlayer.attack.hitstop;
            player.sound = player.otherPlayer.attack.impactSound;
            DemonicsVector2 hurtEffectPosition = DemonicsVector2.Zero;
            if (player.otherPlayer.isAir)
            {
                hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y - (0.1f * -player.flip));
                player.otherPlayer.attack.hurtEffectPosition = new Vector2((float)hurtEffectPosition.x, (float)hurtEffectPosition.y);
            }
            else
            {
                hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y);
                player.otherPlayer.attack.hurtEffectPosition = new Vector2((float)hurtEffectPosition.x, (float)hurtEffectPosition.y);
            }
            player.SetEffect(player.otherPlayer.attack.hurtEffect, hurtEffectPosition);
            if (player.otherPlayer.attack.cameraShaker != null && !player.otherPlayer.attack.causesSoftKnockdown)
            {
                CameraShake.Instance.Shake(player.otherPlayer.attack.cameraShaker);
            }
            player.animationFrames = 0;
            player.stunFrames = player.otherPlayer.attack.hitStun;
            player.knockback = 0;
            start = player.position;
            end = new DemonicsVector2(player.position.x + (player.otherPlayer.attack.knockbackForce.x * player.otherPlayer.flip), player.position.y);
        }
        player.animation = "HurtAir";
        player.animationFrames++;
        if (GameSimulation.Hitstop <= 0)
        {
            DemonicsFloat ratio = (DemonicsFloat)0;
            if (player.otherPlayer.attack.knockbackDuration > 0)
            {
                ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.otherPlayer.attack.knockbackDuration;
                DemonicsFloat distance = end.x - start.x;
                DemonicsFloat nextX = DemonicsFloat.Lerp(start.x, end.x, ratio);
                DemonicsFloat baseY = DemonicsFloat.Lerp(start.y, end.y, (nextX - start.x) / distance);
                DemonicsFloat arc = player.otherPlayer.attack.knockbackArc * (nextX - start.x) * (nextX - end.x) / ((-0.25f) * distance * distance);
                DemonicsVector2 nextPosition = new DemonicsVector2(nextX, baseY + arc);
                if (player.otherPlayer.attack.knockbackArc == 0)
                {
                    nextPosition = new DemonicsVector2(nextX, player.position.y);
                }
                else
                {
                    nextPosition = new DemonicsVector2(nextX, baseY + arc);
                }
                player.position = nextPosition;
                player.knockback++;
            }
            player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.JUGGLE_GRAVITY);
            player.player.StopShakeCoroutine();
            player.stunFrames--;
            if (player.otherPlayer.attack.knockbackArc == 0)
            {
            }
            ToIdleState(player, ratio);
        }
        ToHurtState(player);
        ToFallState(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            if ((int)player.position.y > (int)DemonicsPhysics.GROUND_POINT)
            {
                player.player.StopShakeCoroutine();
                player.player.OtherPlayer.StopComboTimer();
                player.player.PlayerUI.UpdateHealthDamaged();
                player.enter = false;
                player.state = "Fall";
            }
        }
    }
    private void ToIdleState(PlayerNetwork player, DemonicsFloat ratio)
    {
        if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && ratio >= (DemonicsFloat)0.5f)
        {
            player.player.StopShakeCoroutine();
            player.player.OtherPlayer.StopComboTimer();
            player.player.PlayerUI.UpdateHealthDamaged();
            player.sound = "Landed";
            player.SetEffect("Fall", player.position);
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            player.otherPlayer.attack = player.otherPlayer.attack;
            player.enter = false;
            player.otherPlayer.canChainAttack = true;
            if (player.otherPlayer.attack.isArcana)
            {
                player.state = "Airborne";
            }
            else
            {
                player.state = "HurtAir";
            }
        }
    }
}

