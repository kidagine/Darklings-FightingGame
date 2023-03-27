using UnityEngine;

public class DashAirState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.sound = "Dash";
            player.animationFrames = 0;
            if (player.dashDirection > 0)
            {
                DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x - 1, player.position.y);
                player.SetEffect("Dash", effectPosition, false);
            }
            else
            {
                DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x + 1, player.position.y);
                player.SetEffect("Dash", effectPosition, true);
            }
            player.canDoubleJump = false;
            player.dashFrames = 15;
            player.velocity = new DemonicsVector2(player.dashDirection * player.playerStats.DashForce, 0);
        }
        player.dashDirection = 0;
        Dash(player);
    }

    private void Dash(PlayerNetwork player)
    {
        if (player.dashFrames > 0)
        {
            player.animationFrames = 0;
            player.animation = "Fall";
            if (player.dashFrames % 5 == 0)
            {
                if (player.flip > 0)
                {
                    DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x - 1, player.position.y);
                    player.SetEffect("Ghost", player.position, false);
                }
                else
                {
                    DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x + 1, player.position.y);
                    player.SetEffect("Ghost", player.position, true);
                }
            }
            player.dashFrames--;
        }
        else
        {
            player.velocity = DemonicsVector2.Zero;
            EnterState(player, "Fall");
        }
    }
}