using UnityEngine;

public class DashState : State
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
                Vector2 effectPosition = new Vector2(player.position.x - 1, player.position.y);
                player.SetEffect("Dash", effectPosition, false);
            }
            else
            {
                Vector2 effectPosition = new Vector2(player.position.x + 1, player.position.y);
                player.SetEffect("Dash", effectPosition, true);
            }
            player.dashFrames = 15;
            player.velocity = new Vector2(player.dashDirection * (float)player.playerStats.DashForce, 0);
        }
        Dash(player);
    }

    private void Dash(PlayerNetwork player)
    {
        if (player.dashFrames > 0)
        {
            player.animationFrames = 0;
            player.animation = "Dash";
            if (player.dashFrames % 5 == 0)
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
            player.dashFrames--;
        }
        else
        {
            player.velocity = Vector2.zero;
            player.enter = false;
            if (player.direction.x * player.flip > 0)
            {
                player.sound = "Run";
                player.state = "Run";
            }
            else
            {
                player.state = "Idle";
            }
        }
    }
}
