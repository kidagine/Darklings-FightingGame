using UnityEngine;

public class IdleState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
        }
        player.animation = "Idle";
        player.animationFrames++;
        player.velocity = DemonicsVector2.Zero;
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
        {
            EnterState(player, "Crouch");
        }
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.direction.y > 0)
        {
            EnterState(player, "Jump");
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.direction.y > 0 && player.direction.x != 0)
        {
            player.jumpDirection = (int)player.direction.x;
            EnterState(player, "JumpForward");
        }
    }
    private void ToWalkState(PlayerNetwork player)
    {
        if (player.direction.x != 0)
        {
            EnterState(player, "Walk");
        }
    }
    private void ToDashState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentInput().pressed)
        {
            if (player.inputBuffer.CurrentInput().inputEnum == InputEnum.ForwardDash)
            {
                player.dashDirection = 1;
                EnterState(player, "Dash");

            }
            else if (player.inputBuffer.CurrentInput().inputEnum == InputEnum.BackDash)
            {
                player.dashDirection = -1;
                EnterState(player, "Dash");
            }
        }
    }

}