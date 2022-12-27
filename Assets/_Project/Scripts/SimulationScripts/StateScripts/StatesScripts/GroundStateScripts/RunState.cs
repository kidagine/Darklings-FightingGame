using UnityEngine;

public class RunState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        player.animation = "Run";
        player.animationFrames++;
        player.velocity = new Vector2(player.direction.x * (float)player.playerStats.SpeedRun, 0);
        if (DemonicsWorld.Frame % 5 == 0)
        {
            if (player.flip > 0)
            {
                Vector2 effectPosition = new Vector2(player.position.x - 1, player.position.y);
                player.SetEffect("Ghost", player.position, false);
            }
            else
            {
                Vector2 effectPosition = new Vector2(player.position.x + 1, player.position.y);
                player.SetEffect("Ghost", player.position, true);
            }
        }
        ToIdleState(player);
        ToJumpState(player);
        ToJumpForwardState(player);
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.direction.y > 0)
        {
            player.state = "Jump";
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.direction.y > 0 && player.direction.x != 0)
        {
            player.state = "JumpForward";
        }
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.direction.x == 0)
        {
            player.soundStop = "Run";
            player.state = "Idle";
        }
    }
}