using UnityEngine;

public class WalkState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
        }
        player.canDoubleJump = true;
        player.animation = "Walk";
        player.animationFrames++;
        player.velocity = new DemonicsVector2(player.direction.x * player.playerStats.SpeedWalk, 0);
        bool footstep = player.player.PlayerAnimator.GetFootstep(player.animation, player.animationFrames, out int cel);
        if (cel != player.cel && footstep)
        {
            player.cel = cel;
            player.soundGroup = "Footsteps";
        }
        CheckFlip(player);
        base.UpdateLogic(player);
        ToIdleState(player);
        ToJumpState(player);
        ToJumpForwardState(player);
        ToCrouchState(player);
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
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.direction.x == 0)
        {
            EnterState(player, "Idle");
        }
    }
}