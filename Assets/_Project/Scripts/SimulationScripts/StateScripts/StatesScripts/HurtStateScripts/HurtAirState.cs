using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAirState : HurtParentState
{
    private static AttackSO hurtAttack;
    public static Vector2 start;
    private static Vector2 end;
    private static int knockbackFrame;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.velocity = Vector2.zero;
            CheckFlip(player);
            hurtAttack = PlayerComboSystem.GetComboAttack(player.otherPlayer.playerStats, player.otherPlayer.attackInput, player.otherPlayer.isCrouch, player.otherPlayer.isAir);
            // player.health -= player.player.CalculateDamage(hurtAttack);
            player.player.SetHealth(player.player.CalculateDamage(hurtAttack));
            player.player.StartShakeContact();
            player.player.PlayerUI.Damaged();
            player.player.OtherPlayerUI.IncreaseCombo();
            player.enter = true;
            GameSimulation.Hitstop = hurtAttack.hitstop;
            player.sound = hurtAttack.impactSound;
            if (player.otherPlayer.isAir)
            {
                Vector2 hurtEffectPosition = new Vector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y - (0.1f * -player.flip));
                hurtAttack.hurtEffectPosition = hurtEffectPosition;
            }
            else
            {
                Vector2 hurtEffectPosition = new Vector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y);
                hurtAttack.hurtEffectPosition = hurtEffectPosition;
            }
            player.SetEffect(hurtAttack.hurtEffect, hurtAttack.hurtEffectPosition);
            if (hurtAttack.cameraShaker != null && !hurtAttack.causesSoftKnockdown)
            {
                CameraShake.Instance.Shake(hurtAttack.cameraShaker);
            }
            player.animationFrames = 0;
            player.stunFrames = hurtAttack.hitStun;
            knockbackFrame = 0;
            start = player.position;
            end = new Vector2(player.position.x + (hurtAttack.knockbackForce.x * player.otherPlayer.flip), player.position.y);
        }
        player.animation = "HurtAir";
        player.animationFrames++;
        if (GameSimulation.Hitstop <= 0)
        {
            float ratio = 0;
            if (hurtAttack.knockbackDuration > 0)
            {
                ratio = (float)knockbackFrame / (float)hurtAttack.knockbackDuration;
                float distance = end.x - start.x;
                float nextX = Mathf.Lerp(start.x, end.x, ratio);
                float baseY = Mathf.Lerp(start.y, end.y, (nextX - start.x) / distance);
                float arc = hurtAttack.knockbackArc * (nextX - start.x) * (nextX - end.x) / ((-0.25f) * distance * distance);
                Vector2 nextPosition = new Vector2(nextX, baseY + arc);
                if (hurtAttack.knockbackArc == 0)
                {
                    nextPosition = new Vector2(nextX, player.position.y);
                }
                else
                {
                    nextPosition = new Vector2(nextX, baseY + arc);
                }
                player.position = nextPosition;
                knockbackFrame++;
            }
            player.velocity = new Vector2(player.velocity.x, player.velocity.y - (float)DemonicsPhysics.JUGGLE_GRAVITY);
            player.player.StopShakeCoroutine();
            player.stunFrames--;
            if (hurtAttack.knockbackArc == 0)
            {
                ToHurtState(player);
            }
            ToIdleState(player, ratio);
        }
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
    private void ToIdleState(PlayerNetwork player, float ratio)
    {
        if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && ratio >= 0.5f)
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
        if (player.stunFrames > 0)
        {
            if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
            {
                HurtState.skipKnockback = false;
                player.player.StopShakeCoroutine();
                player.player.OtherPlayer.StopComboTimer();
                player.player.PlayerUI.UpdateHealthDamaged();
                player.sound = "Landed";
                player.SetEffect("Fall", player.position);
                player.enter = false;
                player.state = "Hurt";
            }
        }
    }
    public override bool ToHurtState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        if (attack.causesKnockdown || attack.causesSoftKnockdown)
        {
            player.state = "Airborne";
        }
        else
        {
            player.state = "HurtAir";
        }
        return true;
    }
}

