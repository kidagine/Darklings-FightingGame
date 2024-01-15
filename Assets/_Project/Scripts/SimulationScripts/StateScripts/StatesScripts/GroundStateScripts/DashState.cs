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
                DemonVector2 effectPosition = new DemonVector2(player.position.x - 1, DemonicsPhysics.GROUND_POINT);
                player.SetParticle("Dash", effectPosition, new Vector2(-90, 90));
            }
            else
            {
                DemonVector2 effectPosition = new DemonVector2(player.position.x + 1, DemonicsPhysics.GROUND_POINT);
                player.SetParticle("Dash", effectPosition, new Vector2(-90, -90));
            }
            player.dashFrames = 15;
            return;
        }
        Dash(player);
        ToHurtState(player);
    }

    private void Dash(PlayerNetwork player)
    {
        if (player.dashFrames > 0)
        {
            bool forwardDash = player.dashDirection * player.flip == 1 ? true : false;
            player.animationFrames = 0;
            player.animation = "Dash";
            if (!forwardDash)
            {
                if (player.dashFrames == 15)
                    player.velocity = new DemonVector2((DemonFloat)0.0f, 0);
                else if (player.dashFrames == 14)
                    player.velocity = new DemonVector2((DemonFloat)0.0f, 0);
                else if (player.dashFrames == 13)
                    player.velocity = new DemonVector2((DemonFloat)2.5f * player.dashDirection, 0);
                else if (player.dashFrames == 12)
                    player.velocity = new DemonVector2((DemonFloat)2.5f * player.dashDirection, 0);
                else if (player.dashFrames == 11)
                {
                    player.velocity = new DemonVector2((DemonFloat)2.5f * player.dashDirection, 0);
                    player.position += new DemonVector2(0, 1);
                }
                else if (player.dashFrames == 10)
                    player.velocity = new DemonVector2((DemonFloat)2.8f * player.dashDirection, 0);
                else if (player.dashFrames == 9)
                {
                    player.velocity = new DemonVector2((DemonFloat)3.3f * player.dashDirection, 0);
                    player.position += new DemonVector2(0, 1);
                }
                else if (player.dashFrames == 8)
                    player.velocity = new DemonVector2((DemonFloat)5.5f * player.dashDirection, 0);
                else if (player.dashFrames == 7)
                    player.velocity = new DemonVector2((DemonFloat)7.5f * player.dashDirection, 0);
                else if (player.dashFrames == 6)
                    player.velocity = new DemonVector2((DemonFloat)5.0f * player.dashDirection, 0);
                else if (player.dashFrames == 5)
                {
                    player.velocity = new DemonVector2((DemonFloat)3.85f * player.dashDirection, 0);
                    player.position -= new DemonVector2(0, 1);
                }
                else if (player.dashFrames == 4)
                    player.velocity = new DemonVector2((DemonFloat)2.8f * player.dashDirection, 0);
                else if (player.dashFrames == 3)
                    player.velocity = new DemonVector2((DemonFloat)2.5f * player.dashDirection, 0);
                else if (player.dashFrames == 2)
                {
                    player.velocity = new DemonVector2((DemonFloat)1.5f * player.dashDirection, 0);
                    player.position -= new DemonVector2(0, 1);
                }
                else
                    player.velocity = new DemonVector2((DemonFloat)0.0f, 0);
            }
            else
            {
                if (player.dashFrames > 13)
                    player.velocity = new DemonVector2((DemonFloat)3.3f * player.dashDirection, 0);
                if (player.dashFrames > 2)
                    player.velocity = new DemonVector2((DemonFloat)3.9f * player.dashDirection, 0);
                else
                    player.velocity = new DemonVector2((DemonFloat)2.6f * player.dashDirection, 0);
            }
            if (player.dashFrames % 5 == 0)
            {
                if (player.flip > 0)
                {
                    DemonVector2 effectPosition = new(player.position.x - 1, player.position.y);
                    player.SetEffect("Ghost", effectPosition, false);
                }
                else
                {
                    DemonVector2 effectPosition = new(player.position.x + 1, player.position.y);
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
            player.position = new DemonVector2(player.position.x, DemonicsPhysics.GROUND_POINT);
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
