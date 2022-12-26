using UnityEngine;

public class FallState : AirParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        player.animationFrames = 0;
        player.animation = "Fall";
        player.velocity = new Vector2(player.velocity.x, player.velocity.y - player.gravity);
        base.UpdateLogic(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
        {
            player.sound = "Landed";
            player.SetEffect("Fall", player.position);
            player.state = "Idle";
        }
    }
}