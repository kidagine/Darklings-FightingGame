using UnityEngine;

public class IdleState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        if (!player.enter)
        {
            player.enter = true;
            player.animationFrames = 0;
        }
        player.animation = "Idle";
        player.animationFrames++;
        player.velocity = DemonicsVector2.Zero;
        ToWalkState(player);
        ToJumpState(player);
        ToJumpForwardState(player);
        ToCrouchState(player);
        ToDashState(player);
        ToAttackStates(player);
        ToHurtState(player);
    }

    private void ToCrouchState(PlayerNetwork player)
    {
        if (player.direction.y < 0)
        {
            player.enter = false;
            player.state = "Crouch";
        }
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.direction.y > 0)
        {
            player.enter = false;
            player.state = "Jump";
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.direction.y > 0 && player.direction.x != 0)
        {
            player.jumpDirection = (int)player.direction.x;
            player.enter = false;
            player.state = "JumpForward";
        }
    }
    private void ToWalkState(PlayerNetwork player)
    {
        if (player.direction.x != 0)
        {
            player.enter = false;
            player.state = "Walk";
        }
    }
    private void ToDashState(PlayerNetwork player)
    {
        if (player.dashDirection != 0)
        {
            player.enter = false;
            player.state = "Dash";
        }
    }
    private void ToAttackStates(PlayerNetwork player)
    {
        if (player.start && player.state != "Attack")
        {
            player.canChainAttack = false;
            player.attackInput = player.inputBuffer.inputItems[0].inputEnum;
            player.enter = false;
            player.state = "Attack";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (player.otherPlayer.start && !player.otherPlayer.canChainAttack && player.animationFrames >= 5)
        {
            player.otherPlayer.start = false;
            player.otherPlayer.canChainAttack = true;
            player.enter = false;
            player.state = "Hurt";
        }
    }
}