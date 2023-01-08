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
                DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x - 1, player.position.y);
                player.SetEffect("Dash", effectPosition, false);
            }
            else
            {
                DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x + 1, player.position.y);
                player.SetEffect("Dash", effectPosition, true);
            }
            player.dashFrames = 15;
            player.velocity = new DemonicsVector2(player.dashDirection * player.playerStats.DashForce, 0);
        }
        player.dashDirection = 0;
        ToHurtState(player);
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
                    DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x - 1, player.position.y);
                    player.SetEffect("Ghost", effectPosition, false);
                }
                else
                {
                    DemonicsVector2 effectPosition = new DemonicsVector2(player.position.x + 1, player.position.y);
                    player.SetEffect("Ghost", effectPosition, true);
                }
            }
            player.dashFrames--;
        }
        else
        {
            player.velocity = DemonicsVector2.Zero;
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
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
            player.enter = false;
            player.state = "Hurt";
        }
    }
}
