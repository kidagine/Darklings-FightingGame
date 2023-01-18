using UnityEngine;

public class RunState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
        }
        player.position = new DemonicsVector2(player.position.x, DemonicsPhysics.GROUND_POINT);
        player.animation = "Run";
        player.animationFrames++;
        player.velocity = new DemonicsVector2(player.direction.x * player.playerStats.SpeedRun, 0);
        if (DemonicsWorld.Frame % 5 == 0)
        {
            if (player.flip > 0)
            {
                DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x - 1, player.position.y);
                player.SetEffect("Ghost", effectPosition, false);
            }
            else
            {
                DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x + 1, player.position.y);
                player.SetEffect("Ghost", effectPosition, true);
            }
        }
        base.UpdateLogic(player);
        ToIdleState(player);
        ToJumpState(player);
        ToJumpForwardState(player);
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.direction.y > 0)
        {
            player.enter = false;
            player.soundStop = "Run";
            player.state = "Jump";
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.direction.y > 0 && player.direction.x != 0)
        {
            player.jumpDirection = (int)player.direction.x;
            player.enter = false;
            player.soundStop = "Run";
            player.state = "JumpForward";
        }
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.direction.x == 0)
        {
            player.enter = false;
            player.soundStop = "Run";
            player.state = "Idle";
        }
    }
}