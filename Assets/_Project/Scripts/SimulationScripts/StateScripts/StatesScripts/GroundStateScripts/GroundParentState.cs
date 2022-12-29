
using UnityEngine;

public class GroundParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        CheckFlip(player);
        player.canDoubleJump = true;
        player.canDash = true;
        player.hasJumped = false;
        player.canJump = true;
    }
    public override bool ToAttackState(PlayerNetwork player)
    {
        player.enter = false;
        player.state = "Attack";
        return true;
    }
    public override bool ToArcanaState(PlayerNetwork player)
    {
        player.enter = false;
        player.state = "Arcana";
        return true;
    }
    public override bool ToBlueFrenzyState(PlayerNetwork player)
    {
        player.enter = false;
        player.state = "BlueFrenzy";
        return true;
    }
    public override bool ToRedFrenzyState(PlayerNetwork player)
    {
        player.enter = false;
        player.state = "RedFrenzy";
        return true;
    }
    public override bool ToHurtState(PlayerNetwork player)
    {
        player.enter = false;
        player.state = "Hurt";
        return true;
    }
}