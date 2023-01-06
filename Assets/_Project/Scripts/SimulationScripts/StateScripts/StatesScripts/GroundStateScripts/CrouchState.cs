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
    public void ToAttackState(PlayerNetwork player)
    {
        if (player.start)
        {
            Attack(player);
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