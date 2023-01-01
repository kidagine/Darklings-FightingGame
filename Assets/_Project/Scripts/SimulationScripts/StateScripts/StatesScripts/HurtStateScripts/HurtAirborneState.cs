using UnityEngine;

public class HurtAirborneState : HurtParentState
{
    private static AttackSO hurtAttack;
    public static DemonicsVector2 start;
    private static DemonicsVector2 end;
    private static int knockbackFrame;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            if (!player.wasWallSplatted)
            {
                hurtAttack = PlayerComboSystem.GetComboAttack(player.otherPlayer.playerStats, player.otherPlayer.attackInput, player.otherPlayer.isCrouch, player.otherPlayer.isAir);
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
            }
            else
            {
                player.flip = -player.flip;
            }
            player.health -= player.player.CalculateDamage(hurtAttack);
            player.player.SetHealth(player.player.CalculateDamage(hurtAttack));
            player.player.PlayerUI.Damaged();
            player.player.OtherPlayerUI.IncreaseCombo();
            player.sound = hurtAttack.impactSound;
            if (hurtAttack.cameraShaker != null)
            {
                CameraShake.Instance.Shake(hurtAttack.cameraShaker);
            }
            player.animationFrames = 0;
            player.stunFrames = hurtAttack.hitStun;
            player.enter = true;
            player.velocity = DemonicsVector2.Zero;
            player.animationFrames = 0;
            GameSimulation.Hitstop = hurtAttack.hitstop;
            knockbackFrame = 0;
            start = player.position;
            end = new DemonicsVector2(player.position.x + (hurtAttack.knockbackForce.x * -player.flip), DemonicsPhysics.GROUND_POINT);
        }
        if (GameSimulation.Hitstop <= 0)
        {
            DemonicsFloat ratio = (DemonicsFloat)knockbackFrame / (DemonicsFloat)hurtAttack.knockbackDuration;
            DemonicsFloat distance = end.x - start.x;
            DemonicsFloat nextX = DemonicsFloat.Lerp(start.x, end.x, ratio);
            DemonicsFloat baseY = DemonicsFloat.Lerp(start.y, end.y, (nextX - start.x) / distance);
            DemonicsFloat arc = hurtAttack.knockbackArc * (nextX - start.x) * (nextX - end.x) / ((-0.25f) * distance * distance);
            DemonicsVector2 nextPosition = new DemonicsVector2(nextX, baseY + arc);
            player.position = nextPosition;
            knockbackFrame++;
            ToWallSplatState(player);
            ToKnockdownState(ratio, player);
        }
        player.animation = "HurtAir";
        player.animationFrames++;
    }
    private void ToWallSplatState(PlayerNetwork player)
    {
        if (hurtAttack.causesKnockdown)
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
            if (hurtAttack.causesSoftKnockdown)
            {
                player.state = "SoftKnockdown";
            }
            else
            {
                player.state = "HardKnockdown";
            }
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
            player.wasWallSplatted = false;
            player.state = "HurtAir";
        }
        return true;
    }
}