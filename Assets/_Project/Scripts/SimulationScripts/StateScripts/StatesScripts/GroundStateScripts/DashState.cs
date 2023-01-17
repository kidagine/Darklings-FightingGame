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
        if (IsColliding(player))
        {
            player.enter = false;
            if (player.attackHurtNetwork.moveName == "Shadowbreak")
            {
                player.enter = false;
                player.state = "Knockback";
                return;
            }
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                player.state = "Grabbed";
                return;
            }
            if (DemonicsPhysics.IsInCorner(player))
            {
                player.otherPlayer.knockback = 0;
                player.otherPlayer.pushbackStart = player.otherPlayer.position;
                player.otherPlayer.pushbackEnd = new DemonicsVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
                player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
            }
            if (IsBlocking(player))
            {
                if (player.direction.y < 0)
                {
                    player.state = "BlockLow";
                }
                else
                {
                    player.state = "Block";
                }
            }
            else
            {
                if (player.attackHurtNetwork.hardKnockdown)
                {
                    player.state = "Airborne";
                }
                else
                {
                    if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                    {
                        player.state = "Hurt";
                    }
                    else
                    {
                        player.state = "HurtAir";
                    }
                }
            }
        }
    }
}
