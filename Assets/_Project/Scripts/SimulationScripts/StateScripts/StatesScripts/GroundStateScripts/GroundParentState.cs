
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
    public override bool ToHurtState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        if (attack.causesKnockdown)
        {
            player.state = "Airborne";
        }
        else
        {
            if (attack.knockbackArc == 0 || attack.causesSoftKnockdown)
            {
                player.state = "Hurt";
            }
            else
            {
                player.state = "HurtAir";
            }
        }
        return true;
    }
    public override bool ToBlockState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        if (player.direction.y < 0)
        {
            player.state = "BlockLow";
        }
        else
        {
            player.state = "Block";
        }
        return true;
    }
}