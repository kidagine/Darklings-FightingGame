using UnityEngine;

public class HurtAirborneState : State
{
    public static Vector2 start;
    private static Vector2 end;
    private static int knockbackFrame;
    public override void UpdateLogic(PlayerNetwork player)
    {
        AttackSO hurtAttack = player.player.OtherPlayer.CurrentAttack;
        if (!player.enter)
        {
            player.health -= hurtAttack.damage;
            player.player.SetHealth(player.health);
            player.player.PlayerUI.Damaged();
            player.player.OtherPlayerUI.IncreaseCombo();
            player.sound = hurtAttack.impactSound;
            player.SetEffect(hurtAttack.hurtEffect, hurtAttack.hurtEffectPosition);
            if (hurtAttack.cameraShaker != null && !hurtAttack.causesSoftKnockdown)
            {
                CameraShake.Instance.Shake(hurtAttack.cameraShaker);
            }
            player.animationFrames = 0;
            player.stunFrames = hurtAttack.hitStun;
            player.enter = true;
            player.velocity = Vector2.zero;
            player.animationFrames = 0;
            GameSimulation.Hitstop = hurtAttack.hitstop;
            start = player.position;
            end = new Vector2(player.position.x + (hurtAttack.knockbackForce.x * -player.flip), player.position.y + end.y);
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
            ToIdleState(ratio, player);
        }
        player.animation = "HurtAir";
        player.animationFrames++;
    }
    private void ToIdleState(float ratio, PlayerNetwork player)
    {
        if (ratio >= 1)
        {
            player.enter = false;
            player.state = "Knockdown";
        }
    }
}