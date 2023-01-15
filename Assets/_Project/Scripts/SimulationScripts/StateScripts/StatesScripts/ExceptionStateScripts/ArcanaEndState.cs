using UnityEngine;


public class ArcanaEndState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {

            player.enter = true;

        }
    }
}