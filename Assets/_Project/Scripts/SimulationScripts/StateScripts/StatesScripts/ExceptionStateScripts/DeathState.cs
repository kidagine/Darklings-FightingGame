using UnityEngine;

public class DeathState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.velocity = DemonicsVector2.Zero;
            player.enter = true;
            player.animationFrames = 0;
            player.player.StopShakeCoroutine();
            GameplayManager.Instance.RoundOver(false);
            GameSimulation.IntroFrame = 360;
            player.healthRecoverable = 0;
            player.player.PlayerUI.UpdateHealthDamaged();
            if (!SceneSettings.IsTrainingMode)
            {
                GameSimulation.Run = false;
            }
        }
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        player.animation = "Knockdown";
        player.animationFrames++;
        if (player.animationFrames >= 510)
        {
            if (player.otherPlayer.state != "Taunt")
            {
                player.otherPlayer.enter = false;
                player.otherPlayer.state = "Taunt";
            }
        }
        if (SceneSettings.IsTrainingMode)
        {
            if (player.animationFrames >= 190)
            {
                ResetPlayer(player);
                ResetPlayer(player.otherPlayer);
                player.state = "Idle";
            }
        }
        else
        {
            if (player.animationFrames >= 725)
            {
                ResetPlayer(player);
                ResetPlayer(player.otherPlayer);
                player.enter = false;
                player.state = "Taunt";
                player.otherPlayer.state = "Taunt";
            }
        }
        if (GameSimulation.Hitstop <= 0)
        {
            Knockback(player);
        }
    }
    private void Knockback(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.attackHurtNetwork.knockbackDuration)
        {
            DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.attackHurtNetwork.knockbackDuration;
            DemonicsFloat distance = player.pushbackEnd.x - player.pushbackStart.x;
            DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
            DemonicsFloat baseY = DemonicsFloat.Lerp(player.pushbackStart.y, player.pushbackEnd.y, (nextX - player.pushbackStart.x) / distance);
            DemonicsFloat arc = player.attackHurtNetwork.knockbackArc * (nextX - player.pushbackStart.x) * (nextX - player.pushbackEnd.x) / ((-0.25f) * distance * distance);
            DemonicsVector2 nextPosition = DemonicsVector2.Zero;
            if (player.attackHurtNetwork.softKnockdown)
            {
                nextPosition = new DemonicsVector2(nextX, baseY + arc);
            }
            else
            {
                nextPosition = new DemonicsVector2(nextX, baseY + arc);
            }
            player.position = nextPosition;
            player.knockback++;
        }
    }
}