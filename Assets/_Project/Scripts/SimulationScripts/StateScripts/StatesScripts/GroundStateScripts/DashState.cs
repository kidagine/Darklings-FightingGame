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
                DemonVector2 effectPosition = new DemonVector2(player.position.x - 1, player.position.y);
                player.SetParticle("Dash", effectPosition, new Vector2(-90, 90));
            }
            else
            {
                DemonVector2 effectPosition = new DemonVector2(player.position.x + 1, player.position.y);
                player.SetParticle("Dash", effectPosition, new Vector2(-90, -90));
            }
            player.dashFrames = 15;
            player.velocity = new DemonVector2(player.dashDirection * player.playerStats.DashForce, 0);
            return;
        }
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
                    DemonVector2 effectPosition = new DemonVector2(player.position.x - 1, player.position.y);
                    player.SetEffect("Ghost", effectPosition, false);
                }
                else
                {
                    DemonVector2 effectPosition = new DemonVector2(player.position.x + 1, player.position.y);
                    player.SetEffect("Ghost", effectPosition, true);
                }
            }
            player.dashFrames--;
        }
        else
        {
            player.velocity = DemonVector2.Zero;
            if (player.direction.x * player.flip > 0 && player.dashDirection == player.flip)
            {
                player.dashDirection = 0;
                player.sound = "Run";
                EnterState(player, "Run");
            }
            else
            {
                player.dashDirection = 0;
                EnterState(player, "Idle");
            }
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            if (player.attackHurtNetwork.moveName == "Shadowbreak")
            {
                player.dashDirection = 0;
                EnterState(player, "Knockback");
            }
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                bool forwardDash = player.dashDirection * player.flip == 1 ? true : false;
                if (!forwardDash)
                    return;
                player.dashDirection = 0;
                EnterState(player, "Grabbed");
            }
            if (DemonicsPhysics.IsInCorner(player))
            {
                player.otherPlayer.knockback = 0;
                player.otherPlayer.pushbackStart = player.otherPlayer.position;
                player.otherPlayer.pushbackEnd = new DemonVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
                player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
            }
            if (IsBlocking(player))
            {
                player.dashDirection = 0;
                if (player.direction.y < 0)
                {
                    EnterState(player, "BlockLow");
                }
                else
                {
                    EnterState(player, "Block");
                }
            }
            else
            {
                player.dashDirection = 0;
                if (player.attackHurtNetwork.hardKnockdown)
                {
                    EnterState(player, "Airborne");
                }
                else
                {
                    if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                    {
                        EnterState(player, "Hurt");
                    }
                    else
                    {
                        EnterState(player, "HurtAir");
                    }
                }
            }
        }
    }
}
