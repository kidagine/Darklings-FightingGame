using UnityEngine;

public class CrouchState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
        }
        player.position = new DemonicsVector2(player.position.x, DemonicsPhysics.GROUND_POINT);
        CheckFlip(player);
        player.canDoubleJump = true;
        player.dashFrames = 0;
        player.animationFrames = 0;
        player.animation = "Crouch";
        player.velocity = DemonicsVector2.Zero;
        ToIdleState(player);
        ToAttackState(player);
    }

    private void ToIdleState(PlayerNetwork player)
    {
        if (player.direction.y >= 0)
        {
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToAttackState(PlayerNetwork player)
    {
        if (player.start)
        {
            player.start = false;
            player.isAir = false;
            player.isCrouch = true;
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
                impactSound = atk.impactSound
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