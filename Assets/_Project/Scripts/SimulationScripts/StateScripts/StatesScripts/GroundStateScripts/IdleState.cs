using UnityEngine;

public class IdleState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.animation = "Idle";
            player.enter = true;
            player.animationFrames = 0;
            return;
        }
        player.animationFrames++;
        player.velocity = DemonVector2.Zero;
        CheckFlip(player);
        base.UpdateLogic(player);
        ToWalkState(player);
        ToJumpState(player);
        ToJumpForwardState(player);
        ToCrouchState(player);
        ToDashState(player);
    }

    private void ToCrouchState(PlayerNetwork player)
    {
        if (player.direction.y < 0)
            EnterState(player, "Crouch");
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.direction.y > 0)
        {
            player.jumpDirection = 0;
            EnterState(player, "PreJump");
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.direction.y > 0 && player.direction.x != 0)
        {
            player.jumpDirection = (int)player.direction.x;
            EnterState(player, "PreJump");
        }
    }
    private void ToWalkState(PlayerNetwork player)
    {
        if (player.direction.x != 0)
            EnterState(player, "Walk");
    }
    private void ToDashState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed)
        {
            if (player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.ForwardDash)
            {
                player.dashDirection = 1;
                EnterState(player, "Dash");

            }
            else if (player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.BackDash)
            {
                player.dashDirection = -1;
                EnterState(player, "Dash");
            }
        }
    }

}