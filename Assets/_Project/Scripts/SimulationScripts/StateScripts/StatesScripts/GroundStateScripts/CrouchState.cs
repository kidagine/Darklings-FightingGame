using UnityEngine;

public class CrouchState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);

        player.enter = true;
        player.canDoubleJump = true;
        player.dashFrames = 0;
        player.animationFrames = 0;
        player.animation = "Crouch";
        player.velocity = new Vector2(0, 0);
        ToIdleState(player);
    }

    private void ToIdleState(PlayerNetwork player)
    {
        if (player.direction.y >= 0)
        {
            player.enter = false;
            player.state = "Idle";
        }
    }
    public override bool ToAttackState(PlayerNetwork player)
    {
        player.enter = false;
        player.isCrouch = true;
        player.state = "Attack";
        return true;
    }
    public override bool ToArcanaState(PlayerNetwork player)
    {
        player.enter = false;
        player.isCrouch = true;
        player.state = "Arcana";
        return true;
    }
    public override bool ToDashState(PlayerNetwork player)
    {
        if (player.canDash)
        {
            player.enter = false;
            player.state = "Dash";
            return true;
        }
        return false;
    }
}