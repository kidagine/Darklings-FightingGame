
using UnityEngine;

public class GroundParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.canDoubleJump = true;
        player.canDash = true;
        player.hasJumped = false;
        player.canJump = true;
        ToBlueFrenzyState(player);
        ToRedFrenzyState(player);
        ToGrabState(player);
        ToHurtState(player);
        Shadow(player);
    }

    private void ToBlueFrenzyState(PlayerNetwork player)
    {
        if (player.blueFrenzyPress)
        {
            player.enter = false;
            player.state = "BlueFrenzy";
        }
    }
    private void ToRedFrenzyState(PlayerNetwork player)
    {
        if (player.redFrenzyPress && player.healthRecoverable > player.health)
        {
            AttackSO attack = PlayerComboSystem.GetRedFrenzy(player.playerStats);
            SetAttack(player, attack);
            player.enter = false;
            player.state = "RedFrenzy";
        }
    }
    private void ToGrabState(PlayerNetwork player)
    {
        if (player.grabPress)
        {
            AttackSO attack = PlayerComboSystem.GetThrow(player.playerStats);
            SetAttack(player, attack);
            player.enter = false;
            player.state = "Grab";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.enter = false;
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
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
    public override bool ToBlockState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        if (player.direction.y < 0)
        {
            player.state = "BlockLow";
        }
        else
        {
            player.state = "Block";
        }
        return true;
    }
}