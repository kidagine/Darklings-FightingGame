using UnityEngine;

public class AirParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        ToJumpForwardState(player);
        ToJumpState(player);
        //ToFallState(player);
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
    private void ToFallState(PlayerNetwork player)
    {
        if (player.velocity.y <= 0)
        {
            player.state = "Fall";
        }
    }
    public override bool ToDashState(PlayerNetwork player)
    {
        if (player.canDash)
        {
            player.enter = false;
            player.velocity = Vector2.zero;
            player.state = "DashAir";
            return true;
        }
        return false;
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
        player.isAir = true;
        player.state = "Arcana";
        return true;
    }
}