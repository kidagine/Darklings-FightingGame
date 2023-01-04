using UnityEngine;

public class AirParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        ToDashAirState(player);
        ToJumpForwardState(player);
        ToJumpState(player);
        ToHurtState(player);
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
        if (player.dashDirection != 0 && player.canDash)
        {
            player.enter = false;
            player.velocity = DemonicsVector2.Zero;
            player.state = "DashAir";
        }
    }
    public override bool ToBlockState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        player.state = "BlockAir";
        return true;
    }
    public override void ToAttackState(PlayerNetwork player)
    {
        player.isCrouch = false;
        player.isAir = true;
        player.canChainAttack = false;
        player.enter = false;
        player.state = "Attack";
    }
    public override void ToArcanaState(PlayerNetwork player)
    {
        if (player.arcana >= PlayerStatsSO.ARCANA_MULTIPLIER)
        {
            player.isCrouch = false;
            player.isAir = true;
            player.canChainAttack = false;
            player.enter = false;
            player.state = "Arcana";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            player.enter = false;
            player.otherPlayer.canChainAttack = true;
            if (player.otherPlayer.attack.isArcana)
            {
                player.state = "Airborne";
            }
            else
            {
                player.state = "HurtAir";
            }
        }
    }
}