using UnityEngine;

public class AirParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (ToHurtState(player))
        {
            return;
        }
        if (ToAttackState(player))
        {
            // return;
        }
        ToArcanaState(player);
        ToRedFrenzyState(player);
        ToDashAirState(player);
        ToJumpForwardState(player);
        ToJumpState(player);
        Shadow(player);
    }

    private void ToJumpState(PlayerNetwork player)
    {
        if (player.canDoubleJump)
        {
            if (player.direction.y > 0 && !player.hasJumped)
            {
                player.enter = false;
                player.hasJumped = true;
                player.canDoubleJump = false;
                player.state = "Jump";
            }
            else if (player.direction.y <= 0 && player.hasJumped)
            {
                player.hasJumped = false;
            }
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.canDoubleJump)
        {
            if (player.direction.y > 0 && player.direction.x != 0 && !player.hasJumped)
            {
                player.jumpDirection = (int)player.direction.x;
                player.enter = false;
                player.hasJumped = true;
                player.canDoubleJump = false;
                player.state = "JumpForward";
            }
            else if (player.direction.y <= 0 && player.hasJumped)
            {
                player.hasJumped = false;
            }
        }
    }
    private void ToDashAirState(PlayerNetwork player)
    {
        if (player.canDash && player.inputBuffer.inputItems[0].pressed)
        {
            if (player.inputBuffer.inputItems[0].inputEnum == InputEnum.ForwardDash)
            {
                player.dashDirection = 1;
                player.enter = false;
                player.state = "DashAir";
            }
            else if (player.inputBuffer.inputItems[0].inputEnum == InputEnum.BackDash)
            {
                player.dashDirection = -1;
                player.enter = false;
                player.state = "DashAir";
            }
        }
    }
    public override bool ToBlockState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        player.state = "BlockAir";
        return true;
    }
    private void ToRedFrenzyState(PlayerNetwork player)
    {
        if (player.inputBuffer.inputItems[0].pressed)
        {
            RedFrenzy(player);
        }
    }
    public bool ToAttackState(PlayerNetwork player)
    {
        if (player.inputBuffer.inputItems[0].pressed)
        {
            Attack(player, true);
            return true;
        }
        return false;
    }
    public void ToArcanaState(PlayerNetwork player)
    {
        if (player.inputBuffer.inputItems[0].pressed)
        {
            Arcana(player, true);
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
            }
            if (IsBlocking(player))
            {
                player.state = "BlockAir";
            }
            else
            {
                if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.softKnockdown && (DemonicsFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
                {
                    player.state = "Airborne";
                }
                else
                {
                    player.state = "HurtAir";
                }
            }
            return true;
        }
        return false;
    }
}