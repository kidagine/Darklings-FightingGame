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
        base.UpdateLogic(player);
        ToIdleState(player);
        ToAttackState(player);
        ToArcanaState(player);
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
        if (player.attackPress)
        {
            Attack(player);
        }
    }
    public void ToArcanaState(PlayerNetwork player)
    {
        if (player.arcanaPress)
        {
            Arcana(player);
        }
    }
}