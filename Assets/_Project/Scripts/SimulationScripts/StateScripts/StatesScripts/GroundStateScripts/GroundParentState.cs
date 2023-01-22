
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
        ToAttackState(player);
        ToArcanaState(player);
        Shadow(player);
        if (ToHurtState(player))
        {
            return;
        }
    }

    private void ToBlueFrenzyState(PlayerNetwork player)
    {
        if (player.inputBuffer.inputItems[0].pressed && player.inputBuffer.inputItems[0].inputEnum == InputEnum.Parry)
        {
            player.enter = false;
            player.state = "BlueFrenzy";
        }
    }
    public void ToAttackState(PlayerNetwork player)
    {
        if (player.inputBuffer.inputItems[0].pressed)
        {
            Attack(player);
        }
    }
    public void ToArcanaState(PlayerNetwork player)
    {
        if (player.inputBuffer.inputItems[0].pressed)
        {
            Arcana(player);
        }
    }
    private void ToRedFrenzyState(PlayerNetwork player)
    {
        if (player.inputBuffer.inputItems[0].pressed)
        {
            RedFrenzy(player);
        }
    }
    private void ToGrabState(PlayerNetwork player)
    {
        if (player.inputBuffer.inputItems[0].pressed && player.inputBuffer.inputItems[0].inputEnum == InputEnum.Throw)
        {
            AttackSO attack = PlayerComboSystem.GetThrow(player.playerStats);
            player.attackNetwork = SetAttack(player.attackInput, attack);
            player.enter = false;
            player.state = "Grab";
        }
    }
    private bool ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.enter = false;
            if (player.attackHurtNetwork.moveName == "Shadowbreak")
            {
                player.enter = false;
                player.state = "Knockback";
                return false;
            }
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                player.state = "Grabbed";
                return false;
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
            return true;
        }
        return false;
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