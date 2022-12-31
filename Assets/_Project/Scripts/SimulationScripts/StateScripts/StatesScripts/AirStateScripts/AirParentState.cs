using UnityEngine;

public class AirParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        ToDashAirState(player);
        ToJumpForwardState(player);
        ToJumpState(player);
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.canDoubleJump)
        {
            if (player.direction.y > 0 && !player.hasJumped)
            {
                player.enter = false;
                player.hasJumped = true;
                player.canDoubleJump = false;
                player.state = "Jump";
            }
            else if (player.direction.y <= 0 && player.hasJumped)
            {
                player.hasJumped = false;
            }
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.canDoubleJump)
        {
            if (player.direction.y > 0 && player.direction.x != 0 && !player.hasJumped)
            {
                player.jumpDirection = (int)player.direction.x;
                player.enter = false;
                player.hasJumped = true;
                player.canDoubleJump = false;
                player.state = "JumpForward";
            }
            else if (player.direction.y <= 0 && player.hasJumped)
            {
                player.hasJumped = false;
            }
        }
    }
    private void ToDashAirState(PlayerNetwork player)
    {
        if (player.dashDirection != 0 && player.canDash)
        {
            player.enter = false;
            player.velocity = DemonicsVector2.Zero;
            player.state = "DashAir";
        }
    }
    public override bool ToAttackState(PlayerNetwork player)
    {
        player.enter = false;
        player.isAir = true;
        player.state = "Attack";
        return true;
    }
    public override bool ToArcanaState(PlayerNetwork player)
    {
        player.enter = false;
        player.isAir = true;
        player.state = "Arcana";
        return true;
    }
    public override bool ToHurtState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        if (attack.causesKnockdown || attack.causesSoftKnockdown)
        {
            player.state = "Airborne";
        }
        else
        {
            player.state = "HurtAir";
        }
        return true;
    }
    public override bool ToBlockState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        player.state = "BlockAir";
        return true;
    }
}