using UnityEngine;

public class HurtState : HurtParentState
{
    public static bool skipKnockback;
    private static AttackSO hurtAttack;
    public static DemonicsVector2 start;
    private static DemonicsVector2 end;
    private static int knockbackFrame;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.animationFrames = 0;
            //player.player.OtherPlayer.StartComboTimer(ComboTimerStarterEnum.Yellow);
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
            DemonicsVector2 hurtEffectPosition = DemonicsVector2.Zero;
            if (player.otherPlayer.isAir)
            {
                hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y - (0.1f * -player.flip));
                hurtAttack.hurtEffectPosition = new Vector2((float)hurtEffectPosition.x, (float)hurtEffectPosition.y);
            }
            else
            {
                hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y);
                hurtAttack.hurtEffectPosition = new Vector2((float)hurtEffectPosition.x, (float)hurtEffectPosition.y);
            }
            player.SetEffect(hurtAttack.hurtEffect, hurtEffectPosition);
            if (hurtAttack.cameraShaker != null && !hurtAttack.causesSoftKnockdown)
            {
                CameraShake.Instance.Shake(hurtAttack.cameraShaker);
            }
            player.stunFrames = hurtAttack.hitStun;
            knockbackFrame = 0;
            start = player.position;
            end = new DemonicsVector2(player.position.x + (hurtAttack.knockbackForce.x * -player.flip), DemonicsPhysics.GROUND_POINT - 0.5f);
        }
        player.velocity = DemonicsVector2.Zero;
        player.animation = "Hurt";
        player.animationFrames++;
        if (GameSimulation.Hitstop <= 0)
        {
            if (!skipKnockback)
            {
                if (hurtAttack.knockbackDuration > 0 && knockbackFrame <= hurtAttack.knockbackDuration)
                {
                    DemonicsFloat ratio = (DemonicsFloat)knockbackFrame / (DemonicsFloat)hurtAttack.knockbackDuration;
                    DemonicsFloat distance = end.x - start.x;
                    DemonicsFloat nextX = DemonicsFloat.Lerp(start.x, end.x, ratio);
                    DemonicsFloat baseY = DemonicsFloat.Lerp(start.y, end.y, (nextX - start.x) / distance);
                    DemonicsFloat arc = hurtAttack.knockbackArc * (nextX - start.x) * (nextX - end.x) / ((-0.25f) * distance * distance);
                    DemonicsVector2 nextPosition = new DemonicsVector2(nextX, baseY + arc);
                    if (hurtAttack.causesSoftKnockdown)
                    {
                        nextPosition = new DemonicsVector2(nextX, player.position.y);
                    }
                    else
                    {
                        nextPosition = new DemonicsVector2(nextX, baseY + arc);
                    }
                    player.position = nextPosition;
                    knockbackFrame++;
                }
            }
            player.player.StopShakeCoroutine();
            player.stunFrames--;
        }
        player.stunFrames--;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            skipKnockback = false;
            player.player.StopShakeCoroutine();
            player.player.OtherPlayer.StopComboTimer();
            player.player.PlayerUI.UpdateHealthDamaged();
            player.enter = false;
            player.state = "Idle";
        }
    }

    public override bool ToHurtState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
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
