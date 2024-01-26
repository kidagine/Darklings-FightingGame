using UnityEngine;

public class RunState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animation = "Run";
            player.animationFrames = 0;
            return;
        }
        player.position = new DemonVector2(player.position.x, DemonicsPhysics.GROUND_POINT);
        player.animation = "Run";
        player.animationFrames++;
        player.velocity = new DemonVector2(player.direction.x * player.playerStats.SpeedRun, 0);
        if (DemonicsWorld.Frame % 5 == 0)
        {
            if (player.flip > 0)
            {
                DemonVector2 effectPosition = new DemonVector2(player.position.x - 1, player.position.y);
                player.SetEffect("Ghost", effectPosition, false);
            }
            else
            {
                DemonVector2 effectPosition = new DemonVector2(player.position.x + 1, player.position.y);
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
            player.jumpDirection = 0;
            EnterState(player, "PreJump");
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.direction.y > 0 && player.direction.x != 0)
        {
            player.runJump = true;
            player.jumpDirection = (int)player.direction.x;
            EnterState(player, "PreJump");
        }
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.direction.x == 0)
        {
            EnterState(player, "Idle");
        }
    }

    public override void Exit(PlayerNetwork player)
    {
        base.Exit(player);
        player.soundStop = "Run";
    }
}