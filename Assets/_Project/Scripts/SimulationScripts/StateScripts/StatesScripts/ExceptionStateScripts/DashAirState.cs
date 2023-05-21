using UnityEngine;

public class DashAirState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            bool forwardDash = player.dashDirection * player.flip == 1 ? true : false;
            player.enter = true;
            player.sound = "Dash";
            player.animation = "DashAir";
            player.animationFrames = 0;
            player.canDoubleJump = false;
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
            player.dashFrames = forwardDash == true ? 10 : 15;
            player.velocity = new DemonicsVector2(player.dashDirection * (player.playerStats.DashForce - 0.5), 0);
            player.dashFrames--;
            return;
        }
        player.animationFrames++;
        Dash(player);
        ToAttackState(player);
    }

    public bool ToAttackState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentInput().pressed)
        {
            Attack(player, true);
            return true;
        }
        return false;
    }

    private void Dash(PlayerNetwork player)
    {
        if (player.dashFrames > 0)
        {
            bool forwardDash = player.dashDirection * player.flip == 1 ? true : false;
            int startUpFrames = forwardDash ? 9 : 13;
            int recoveryFrames = forwardDash ? 2 : 3;
            DemonicsFloat dashforce = forwardDash ? player.playerStats.DashAirForce : player.playerStats.DashBackAirForce;
            if (player.dashFrames < startUpFrames && player.dashFrames > recoveryFrames)
            {
                player.velocity = new DemonicsVector2(player.dashDirection * dashforce, 0);
            }
            else
            {
                player.velocity = new DemonicsVector2(player.dashDirection * (dashforce - (DemonicsFloat)1), 0);
            }
            if (player.dashFrames % 3 == 0)
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
            player.dashDirection = 0;
            EnterState(player, "Fall");
        }
    }
}