using UnityEngine;


public class ArcanaEndState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            if (player.inputBuffer.inputItems[0].inputDirection == InputDirectionEnum.Left && player.flip == 1 || player.inputBuffer.inputItems[0].inputDirection == InputDirectionEnum.Right && player.flip == -1)
            {
                player.flip *= -1;
            }
            player.enter = true;
            player.animationFrames = 0;
            player.animation = "5AEnd";
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            player.otherPlayer.pushbox.active = false;
        }
        player.animationFrames++;
        player.attackFrames--;
        if (player.otherPlayer.state == "Grabbed")
        {
            DemonicsVector2 grabPoint = player.player.PlayerAnimator.GetGrabPoint(player.animation, player.animationFrames);
            player.otherPlayer.position = new DemonicsVector2(player.position.x + (grabPoint.x * player.flip), player.position.y + grabPoint.y);
        }

        if (player.player.PlayerAnimator.GetThrowArcanaEnd(player.animation, player.animationFrames) && player.otherPlayer.state == "Grabbed")
        {
            player.otherPlayer.pushbox.active = true;
            player.otherPlayer.enter = false;
            player.otherPlayer.state = "Airborne";
        }
        DemonicsVector2 jump = player.player.PlayerAnimator.GetJump(player.animation, player.animationFrames);
        if (player.pushbackStart != jump)
        {
            player.pushbackStart = jump;
            player.velocity = new DemonicsVector2(jump.x, jump.y);
        }
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        ToFallState(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            if (player.attackNetwork.cameraShakerNetwork.timer > 0)
            {
                CameraShake.Instance.Shake(player.attackNetwork.cameraShakerNetwork);
            }
            player.enter = false;
            player.state = "Fall";
            player.sound = "Impact1";
        }
    }
}