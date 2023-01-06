using UnityEngine;

public class IdleState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
        }
        player.animation = "Idle";
        player.animationFrames++;
        player.velocity = DemonicsVector2.Zero;
        ToWalkState(player);
        ToJumpState(player);
        ToJumpForwardState(player);
        ToCrouchState(player);
        ToDashState(player);
        ToHurtState(player);
        ToAttackState(player);
    }

    private void ToCrouchState(PlayerNetwork player)
    {
        if (player.direction.y < 0)
        {
            player.enter = false;
            player.state = "Crouch";
        }
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.direction.y > 0)
        {
            player.enter = false;
            player.state = "Jump";
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.direction.y > 0 && player.direction.x != 0)
        {
            player.jumpDirection = (int)player.direction.x;
            player.enter = false;
            player.state = "JumpForward";
        }
    }
    private void ToWalkState(PlayerNetwork player)
    {
        if (player.direction.x != 0)
        {
            player.enter = false;
            player.state = "Walk";
        }
    }
    private void ToDashState(PlayerNetwork player)
    {
        if (player.dashDirection != 0)
        {
            player.enter = false;
            player.state = "Dash";
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
    public void ToAttackState(PlayerNetwork player)
    {
        if (player.start)
        {
            player.attackInput = player.inputBuffer.inputItems[0].inputEnum;
            player.start = false;
            player.isAir = false;
            player.isCrouch = false;
            if (player.direction.y < 0)
            {
                player.isCrouch = true;
            }
            AttackSO atk = PlayerComboSystem.GetComboAttack(player.playerStats, player.attackInput, player.isCrouch, false);
            player.attackNetwork = new AttackNetwork()
            {
                damage = atk.damage,
                travelDistance = (DemonicsFloat)atk.travelDistance.x,
                name = atk.name,
                attackSound = atk.attackSound,
                hurtEffect = atk.hurtEffect,
                knockbackForce = (DemonicsFloat)atk.knockbackForce.x,
                knockbackDuration = atk.knockbackDuration,
                hitstop = atk.hitstop,
                impactSound = atk.impactSound,
                hitStun = atk.hitStun
            };
            player.canChainAttack = false;
            player.enter = false;
            player.state = "Attack";
        }
    }
    public override void ToArcanaState(PlayerNetwork player)
    {
        if (player.arcana >= PlayerStatsSO.ARCANA_MULTIPLIER)
        {
            player.isAir = false;
            player.canChainAttack = false;
            player.enter = false;
            player.state = "Arcana";
        }
    }
}