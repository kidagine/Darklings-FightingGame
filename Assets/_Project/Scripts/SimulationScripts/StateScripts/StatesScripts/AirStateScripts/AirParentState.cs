using UnityEngine;

public class AirParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        ToHurtState(player);
        ToAttackState(player);
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
                player.hasJumped = true;
                player.canDoubleJump = false;
                EnterState(player, "Jump");
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
                player.hasJumped = true;
                player.canDoubleJump = false;
                EnterState(player, "JumpForward");
            }
            else if (player.direction.y <= 0 && player.hasJumped)
            {
                player.hasJumped = false;
            }
        }
    }
    private void ToDashAirState(PlayerNetwork player)
    {
        if (player.canDash && player.inputBuffer.CurrentInput().pressed)
        {
            if (player.inputBuffer.CurrentInput().inputEnum == InputEnum.ForwardDash)
            {
                player.dashDirection = 1;
                EnterState(player, "DashAir");
            }
            else if (player.inputBuffer.CurrentInput().inputEnum == InputEnum.BackDash)
            {
                player.dashDirection = -1;
                EnterState(player, "DashAir");
            }
        }
    }
    private void ToRedFrenzyState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentInput().pressed)
        {
            RedFrenzy(player);
        }
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
    public void ToArcanaState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentInput().pressed)
        {
            Arcana(player, true);
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            if (player.attackHurtNetwork.moveName == "Shadowbreak")
            {
                EnterState(player, "Knockback");
                return;
            }
            if (IsBlocking(player))
            {
                EnterState(player, "BlockAir");
            }
            else
            {
                if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.softKnockdown && player.position.y > DemonicsPhysics.GROUND_POINT)
                {
                    EnterState(player, "Airborne");
                }
                else
                {
                    EnterState(player, "HurtAir");
                }
            }
        }
    }
}