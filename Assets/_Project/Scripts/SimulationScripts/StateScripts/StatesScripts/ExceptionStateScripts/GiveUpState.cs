using UnityEngine;

public class GiveUpState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            GameSimulation.Run = false;
            GameSimulation.GlobalHitstop = 1;
            player.velocity = DemonVector2.Zero;
            player.enter = true;
            player.animationFrames = 0;
            player.player.StopShakeCoroutine();
            GameplayManager.Instance.RoundOver(true);
            player.healthRecoverable = 0;
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            ResetCombo(player);
            return;
        }
        player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        player.animation = "Death";
        player.animationFrames++;
        if (player.animationFrames >= 725)
        {
            GameSimulation.Timer = GameSimulation._timerMax;
            ResetPlayer(player);
            ResetPlayer(player.otherPlayer);
            EnterState(player, "Taunt");
            EnterState(player.otherPlayer, "Taunt");
        }
    }
}