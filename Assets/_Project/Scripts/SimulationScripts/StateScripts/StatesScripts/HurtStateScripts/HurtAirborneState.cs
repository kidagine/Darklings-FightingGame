using UnityEngine;

public class HurtAirborneState : HurtParentState
{
    public static DemonicsVector2 start;
    private static DemonicsVector2 end;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            if (!player.wasWallSplatted)
            {
                DemonicsVector2 hurtEffectPosition = DemonicsVector2.Zero;
                if (player.otherPlayer.isAir)
                {
                    hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y - (0.1f * -player.flip));
                    player.hurtAttack.hurtEffectPosition = new Vector2((float)hurtEffectPosition.x, (float)hurtEffectPosition.y);
                }
                else
                {
                    hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y);
                    player.hurtAttack.hurtEffectPosition = new Vector2((float)hurtEffectPosition.x, (float)hurtEffectPosition.y);
                }
                player.SetEffect(player.hurtAttack.hurtEffect, hurtEffectPosition);
            }
            else
            {
                player.flip = -player.flip;
            }
            player.health -= player.hurtAttack.damage;
            player.player.SetHealth(player.hurtAttack.damage);
            player.player.PlayerUI.Damaged();
            player.player.OtherPlayerUI.IncreaseCombo();
            player.sound = player.hurtAttack.impactSound;
            if (player.hurtAttack.cameraShaker != null)
            {
                CameraShake.Instance.Shake(player.hurtAttack.cameraShaker);
            }
            player.animationFrames = 0;
            player.stunFrames = player.hurtAttack.hitStun;
            player.enter = true;
            player.velocity = DemonicsVector2.Zero;
            player.animationFrames = 0;
            GameSimulation.Hitstop = player.hurtAttack.hitstop;
            player.knockback = 0;
            start = player.position;
            end = new DemonicsVector2(player.position.x + (player.hurtAttack.knockbackForce.x * -player.flip), DemonicsPhysics.GROUND_POINT);
        }
        player.animation = "HurtAir";
        if (player.animationFrames < 4)
        {
            player.animationFrames++;
        }
        if (GameSimulation.Hitstop <= 0)
        {
            DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.hurtAttack.knockbackDuration;
            DemonicsFloat distance = end.x - start.x;
            DemonicsFloat nextX = DemonicsFloat.Lerp(start.x, end.x, ratio);
            DemonicsFloat baseY = DemonicsFloat.Lerp(start.y, end.y, (nextX - start.x) / distance);
            DemonicsFloat arc = player.hurtAttack.knockbackArc * (nextX - start.x) * (nextX - end.x) / ((-0.25f) * distance * distance);
            DemonicsVector2 nextPosition = new DemonicsVector2(nextX, baseY + arc);
            player.position = nextPosition;
            player.knockback++;
            ToWallSplatState(player);
            ToKnockdownState(ratio, player);
        }
        ToHurtState(player);
    }
    private void ToWallSplatState(PlayerNetwork player)
    {
        if (player.hurtAttack.causesKnockdown)
        {
            if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT && player.flip == 1
            || player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT && player.flip == -1)
            {
                player.enter = false;
                player.state = "WallSplat";
            }
        }
    }
    private void ToKnockdownState(DemonicsFloat ratio, PlayerNetwork player)
    {
        if (ratio >= (DemonicsFloat)1)
        {
            player.wasWallSplatted = false;
            player.enter = false;
            if (player.hurtAttack.causesSoftKnockdown)
            {
                player.state = "SoftKnockdown";
            }
            else
            {
                player.state = "HardKnockdown";
            }
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            player.hurtAttack = player.otherPlayer.attack;
            player.enter = false;
            player.otherPlayer.canChainAttack = true;
            if (player.hurtAttack.isArcana)
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