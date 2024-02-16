using UnityEngine;

public class TauntState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            if (player.otherPlayer.health <= 0)
                player.comboLocked = true;
            else
                player.comboLocked = false;
            player.enter = true;
            player.animationFrames = 0;
            player.animation = "Taunt";
        }
        player.velocity = DemonVector2.Zero;
        player.animationFrames++;
        if (GameSimulation.Timer <= 0)
            return;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.animationFrames >= 100 && !player.comboLocked)
        {
            GameSimulation.Run = true;
            EnterState(player, "Idle");
        }
    }
}