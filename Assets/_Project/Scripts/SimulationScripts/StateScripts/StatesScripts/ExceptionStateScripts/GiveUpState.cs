using UnityEngine;

public class GiveUpState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
        }
    }
}