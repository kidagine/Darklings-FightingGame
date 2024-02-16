using UnityEngine;

public class DashAirState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            bool forwardDash = player.dashDirection * player.flip == 1 ? true : false;
            string dashParticle = forwardDash ? "DashAirForward" : "DashAirBackwards";
            player.enter = true;
            player.sound = "Dash";
            player.animation = "DashAir";
            player.animationFrames = 0;
            player.canDoubleJump = false;
            if (player.dashDirection > 0)
            {
                DemonVector2 effectPosition = new DemonVector2(player.position.x + 20, player.position.y + 30);
                player.SetParticle(dashParticle, effectPosition, new Vector2(0, 90));
            }
            else
            {
                DemonVector2 effectPosition = new DemonVector2(player.position.x - 20, player.position.y + 30);
                player.SetParticle(dashParticle, effectPosition, new Vector2(0, -90));
            }
            player.dashFrames = forwardDash == true ? 10 : 15;
            player.velocity = new DemonVector2(player.dashDirection * (player.playerStats.DashForce - 0.5), 0);
            player.dashFrames--;
            return;
        }
        player.animationFrames++;
        Dash(player);
        ToAttackState(player);
    }

    public bool ToAttackState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed)
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
            DemonFloat dashforce = forwardDash ? player.playerStats.DashAirForce : player.playerStats.DashBackAirForce;
            if (player.dashFrames < startUpFrames && player.dashFrames > recoveryFrames)
            {
                player.velocity = new DemonVector2(player.dashDirection * dashforce, 0);
            }
            else
            {
                player.velocity = new DemonVector2(player.dashDirection * (dashforce - (DemonFloat)1), 0);
            }
            if (player.dashFrames % 3 == 0)
            {
                if (player.flip > 0)
                {
                    DemonVector2 effectPosition = new DemonVector2(player.position.x - 1, player.position.y);
                    player.SetEffect("Ghost", player.position, false);
                }
                else
                {
                    DemonVector2 effectPosition = new DemonVector2(player.position.x + 1, player.position.y);
                    player.SetEffect("Ghost", player.position, true);
                }
            }
            player.dashFrames--;
        }
        else
        {
            CheckFlip(player);
            player.dashDirection = 0;
            EnterState(player, "Fall");
        }
    }
}