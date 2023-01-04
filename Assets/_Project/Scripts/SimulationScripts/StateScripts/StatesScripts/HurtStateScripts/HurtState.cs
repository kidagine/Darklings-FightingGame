using UnityEngine;

public class HurtState : HurtParentState
{
    public static DemonicsVector2 start;
    private static DemonicsVector2 end;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.animationFrames = 0;
            //player.player.OtherPlayer.StartComboTimer(ComboTimerStarterEnum.Yellow);
            CheckFlip(player);
            player.health -= player.hurtAttack.damage;
            // player.player.SetHealth(200);
            player.player.StartShakeContact();
            player.player.PlayerUI.Damaged();
            player.player.OtherPlayerUI.IncreaseCombo();
            player.enter = true;
            GameSimulation.Hitstop = player.hurtAttack.hitstop;
            player.sound = player.hurtAttack.impactSound;
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
            player.SetEffect(player.hurtAttack.hurtEffect, hurtEffectPosition);
            if (player.hurtAttack.cameraShaker != null && !player.hurtAttack.causesSoftKnockdown)
            {
                CameraShake.Instance.Shake(player.hurtAttack.cameraShaker);
            }
            player.stunFrames = player.hurtAttack.hitStun;
            player.knockback = 0;
            start = player.position;
            end = new DemonicsVector2(player.position.x + (player.hurtAttack.knockbackForce.x * -player.flip), DemonicsPhysics.GROUND_POINT - 0.5f);
        }
        player.velocity = DemonicsVector2.Zero;
        player.animation = "Hurt";
        if (player.animationFrames < 4)
        {
            player.animationFrames++;
        }
        if (GameSimulation.Hitstop <= 0)
        {
            // if (!DemonicsPhysics.IsInCorner(player))
            // {
            //     if (player.hurtAttack.knockbackDuration > 0 && player.knockback <= player.hurtAttack.knockbackDuration)
            //     {
            //         DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.hurtAttack.knockbackDuration;
            //         DemonicsFloat distance = end.x - start.x;
            //         DemonicsFloat nextX = DemonicsFloat.Lerp(start.x, end.x, ratio);
            //         DemonicsVector2 nextPosition = new DemonicsVector2(nextX, player.position.y);
            //         player.position = nextPosition;
            //         player.knockback++;
            //     }
            // }
            player.player.StopShakeCoroutine();
        }
        player.stunFrames--;
        ToHurtState(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            player.player.StopShakeCoroutine();
            player.player.OtherPlayer.StopComboTimer();
            player.player.PlayerUI.UpdateHealthDamaged();
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            player.hurtAttack = player.otherPlayer.attack;
            player.enter = false;
            player.otherPlayer.canChainAttack = true;
            if (player.otherPlayer.attack.isArcana)
            {
                player.state = "Airborne";
            }
            else
            {
                player.state = "Hurt";
            }
        }
    }
}
