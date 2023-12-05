using UnityEngine;

public class DeathState : State
{
    //TODO: Refactor frame checks
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            CameraShake.Instance.ZoomDefault(0.3f);
            if (!SceneSettings.IsTrainingMode)
                GameSimulation.Run = false;
            GameSimulation.GlobalHitstop = 1;
            player.velocity = DemonVector2.Zero;
            player.enter = true;
            player.animationFrames = 0;
            player.player.StopShakeCoroutine();
            GameplayManager.Instance.RoundOver(false);
            player.healthRecoverable = 0;
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            ResetCombo(player);
        }
        if (player.position.y > DemonicsPhysics.GROUND_POINT)
            player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        else
            player.velocity = new DemonVector2(player.velocity.x, 0);
        player.animation = "Death";
        player.animationFrames++;
        if (player.animationFrames >= 255)
            if (player.otherPlayer.state != "Taunt" && player.otherPlayer.health > 0)
                EnterState(player.otherPlayer, "Taunt");
        if (SceneSettings.IsTrainingMode)
        {
            if (player.animationFrames >= 95)
            {
                player.invincible = false;
                ResetPlayer(player);
                ResetPlayer(player.otherPlayer);
                EnterState(player, "Idle");
            }
        }
        else
        {
            if (player.animationFrames >= 370)
            {
                GameSimulation.Timer = GameSimulation._timerMax;
                player.invincible = false;
                ResetPlayer(player);
                ResetPlayer(player.otherPlayer);
                EnterState(player, "Taunt");
                EnterState(player.otherPlayer, "Taunt");
            }
        }
        if (!player.hitstop)
            Knockback(player);
    }
    private void Knockback(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.attackHurtNetwork.knockbackDuration)
        {
            DemonFloat ratio = (DemonFloat)player.knockback / (DemonFloat)player.attackHurtNetwork.knockbackDuration;
            DemonFloat distance = player.pushbackEnd.x - player.pushbackStart.x;
            DemonFloat nextX = DemonFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
            DemonFloat baseY = DemonFloat.Lerp(player.pushbackStart.y, player.pushbackEnd.y, (nextX - player.pushbackStart.x) / distance);
            DemonFloat arc = player.attackHurtNetwork.knockbackArc * (nextX - player.pushbackStart.x) * (nextX - player.pushbackEnd.x) / ((-0.25f) * distance * distance);
            DemonVector2 nextPosition = DemonVector2.Zero;
            if (player.attackHurtNetwork.softKnockdown)
                nextPosition = new DemonVector2(nextX, baseY + arc);
            else
                nextPosition = new DemonVector2(nextX, baseY + arc);
            player.position = nextPosition;
            player.knockback++;
        }
    }
}