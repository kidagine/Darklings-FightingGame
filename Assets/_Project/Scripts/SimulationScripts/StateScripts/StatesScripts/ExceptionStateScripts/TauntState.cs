using UnityEngine;

public class TauntState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
            player.animation = "Taunt";
        }
        player.velocity = DemonicsVector2.Zero;
        player.animationFrames++;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.animationFrames >= 160)
        {
            if (player.health == 0 || player.otherPlayer.health == 0)
            {
                return;
            }
            GameSimulation.Run = true;
            EnterState(player, "Idle");
        }
    }
}