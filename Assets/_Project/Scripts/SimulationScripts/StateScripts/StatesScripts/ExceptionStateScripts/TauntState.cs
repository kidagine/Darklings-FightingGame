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
        player.animationFrames++;
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.animationFrames >= 160)
        {
            player.enter = false;
            player.state = "Idle";
        }
    }
}