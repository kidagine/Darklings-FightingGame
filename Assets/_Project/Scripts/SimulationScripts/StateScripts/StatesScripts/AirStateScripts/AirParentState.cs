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
        if (player.canDoubleJump && player.canChainAttack)
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
        if (player.canDoubleJump && player.canChainAttack)
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
        if (player.canDoubleJump && player.inputBuffer.CurrentTrigger().pressed)
        {
            if (player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.ForwardDash)
            {
                player.dashDirection = 1;
                EnterState(player, "DashAir");
            }
            else if (player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.BackDash)
            {
                player.dashDirection = -1;
                EnterState(player, "DashAir");
            }
        }
    }
    private void ToRedFrenzyState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed)
        {
            RedFrenzy(player);
        }
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
    public void ToArcanaState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed)
        {
            Arcana(player, true);
        }
    }
    protected void ToHurtState(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.attackType != AttackTypeEnum.Throw)
        {
            if (IsColliding(player))
            {
                if (IsBlocking(player))
                    EnterState(player, "BlockAir");
                else
                {
                    if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.moveName == "Shadowbreak" || player.attackHurtNetwork.softKnockdown && player.position.y > DemonicsPhysics.GROUND_POINT)
                        EnterState(player, "Airborne");
                    else
                        EnterState(player, "HurtAir");
                }
            }
        }
    }
}