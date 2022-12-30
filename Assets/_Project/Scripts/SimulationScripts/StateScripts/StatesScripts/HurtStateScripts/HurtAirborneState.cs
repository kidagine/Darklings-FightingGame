using UnityEngine;

public class HurtAirborneState : HurtParentState
{
    private static AttackSO hurtAttack;
    public static Vector2 start;
    private static Vector2 end;
    private static int knockbackFrame;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            if (!player.wasWallSplatted)
            {
                hurtAttack = PlayerComboSystem.GetComboAttack(player.otherPlayer.playerStats, player.otherPlayer.attackInput, player.otherPlayer.isCrouch, player.otherPlayer.isAir);
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
            }
            else
            {
                player.flip = -player.flip;
            }
            //player.health -= hurtAttack.damage;
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
            player.velocity = Vector2.zero;
            player.animationFrames = 0;
            GameSimulation.Hitstop = hurtAttack.hitstop;
            knockbackFrame = 0;
            start = player.position;
            end = new Vector2(player.position.x + (hurtAttack.knockbackForce.x * -player.flip), (float)DemonicsPhysics.GROUND_POINT);
        }
        if (GameSimulation.Hitstop <= 0)
        {
            float ratio = (float)knockbackFrame / (float)hurtAttack.knockbackDuration;
            float distance = end.x - start.x;
            float nextX = Mathf.Lerp(start.x, end.x, ratio);
            float baseY = Mathf.Lerp(start.y, end.y, (nextX - start.x) / distance);
            float arc = hurtAttack.knockbackArc * (nextX - start.x) * (nextX - end.x) / ((-0.25f) * distance * distance);
            Vector2 nextPosition = new Vector2(nextX, baseY + arc);
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
        if (player.position.x <= (float)DemonicsPhysics.WALL_LEFT_POINT && player.flip == 1
        || player.position.x >= (float)DemonicsPhysics.WALL_RIGHT_POINT && player.flip == -1)
        {
            player.enter = false;
            player.state = "WallSplat";
        }
    }
    private void ToKnockdownState(float ratio, PlayerNetwork player)
    {
        if (ratio >= 1)
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