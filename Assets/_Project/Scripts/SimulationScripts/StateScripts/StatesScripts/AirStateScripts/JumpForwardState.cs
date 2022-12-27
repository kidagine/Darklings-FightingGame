using UnityEngine;

public class JumpForwardState : AirParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.sound = "Jump";
            player.SetEffect("Jump", player.position);
            player.hasJumped = true;
            player.animationFrames = 0;
            player.velocity = new Vector2(0.14f * player.direction.x, (float)player.playerStats.JumpForce);
        }
        player.animation = "JumpForward";
        player.animationFrames++;
        player.velocity = new Vector2(player.velocity.x, player.velocity.y - player.gravity);
        base.UpdateLogic(player);
        ToFallState(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.velocity.y <= 0)
        {
            player.enter = false;
            player.state = "Fall";
        }
    }
}