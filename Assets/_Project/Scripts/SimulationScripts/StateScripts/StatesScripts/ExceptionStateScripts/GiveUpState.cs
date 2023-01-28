using UnityEngine;

public class GiveUpState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            GameSimulation.Run = false;
            GameSimulation.Timer = 99;
            GameSimulation.GlobalHitstop = 1;
            player.velocity = DemonicsVector2.Zero;
            player.enter = true;
            player.animationFrames = 0;
            player.player.StopShakeCoroutine();
            GameplayManager.Instance.RoundOver(true);
            GameSimulation.IntroFrame = 360;
            player.healthRecoverable = 0;
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            ResetCombo(player);
        }
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        player.animation = "Death";
        player.animationFrames++;
        if (player.animationFrames >= 725)
        {
            ResetPlayer(player);
            ResetPlayer(player.otherPlayer);
            EnterState(player, "Taunt");
            EnterState(player.otherPlayer, "Taunt");
        }
    }
}