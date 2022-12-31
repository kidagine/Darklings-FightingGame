using UnityEngine;

public class WalkState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
        }
        CheckFlip(player);
        player.canDoubleJump = true;
        player.animation = "Walk";
        player.animationFrames++;
        player.velocity = new DemonicsVector2(player.direction.x * player.playerStats.SpeedWalk, 0);
        ToIdleState(player);
        ToJumpState(player);
        ToJumpForwardState(player);
        ToCrouchState(player);
        ToDashState(player);
    }
    private void ToCrouchState(PlayerNetwork player)
    {
        if (player.direction.y < 0)
        {
            player.enter = false;
            player.state = "Crouch";
        }
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.direction.y > 0)
        {
            player.enter = false;
            player.state = "Jump";
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.direction.y > 0 && player.direction.x != 0)
        {
            player.jumpDirection = (int)player.direction.x;
            player.enter = false;
            player.state = "JumpForward";
        }
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.direction.x == 0)
        {
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToDashState(PlayerNetwork player)
    {
        Debug.Log(player.dashDirection);
        if (player.dashDirection != 0)
        {
            player.enter = false;
            player.state = "Dash";
        }
    }
}