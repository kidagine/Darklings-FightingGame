using UnityEngine;

public class JumpState : AirParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (player.enter)
        {
            player.animationFrames++;
        }
        if (!player.enter)
        {
            player.enter = true;
            player.sound = "Jump";
            player.SetEffect("Jump", player.position);
            player.hasJumped = true;
            player.animationFrames = 0;
            player.velocity = new DemonicsVector2((DemonicsFloat)0, player.playerStats.JumpForce);
        }
        player.animation = "Jump";
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        base.UpdateLogic(player);
        ToFallState(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.velocity.y <= (DemonicsFloat)0)
        {
            EnterState(player, "Fall");
        }
    }
}