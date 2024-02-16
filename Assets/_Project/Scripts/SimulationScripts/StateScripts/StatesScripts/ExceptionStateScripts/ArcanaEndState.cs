using UnityEngine;


public class ArcanaEndState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            SetTopPriority(player);
            if (player.direction.x < 0 && player.flip == 1 || player.direction.x > 0 && player.flip == -1)
            {
                player.flip *= -1;
            }
            player.enter = true;
            player.animationFrames = 0;
            player.animation = "5AEnd";
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            player.otherPlayer.pushbox.active = false;
            player.velocity = DemonVector2.Zero;
            return;
        }
        player.animationFrames++;
        player.attackFrames--;
        if (player.otherPlayer.state == "Grabbed")
        {
            DemonVector2 grabPoint = player.player.PlayerAnimator.GetGrabPoint(player.animation, player.animationFrames);
            player.otherPlayer.position = new DemonVector2(player.position.x + (grabPoint.x * player.flip), player.position.y + grabPoint.y);
        }

        bool arcanaEnd = player.player.PlayerAnimator.GetThrowArcanaEnd(player.animation, player.animationFrames);
        if (arcanaEnd && player.otherPlayer.state == "Grabbed")
        {
            EnterState(player.otherPlayer, "Airborne");
        }
        DemonVector2 jump = player.player.PlayerAnimator.GetJump(player.animation, player.animationFrames);
        if (player.pushbackStart != jump)
        {
            player.pushbackStart = jump;
            player.velocity = new DemonVector2(jump.x, jump.y);
        }
        player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        ToFallState(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            if (player.attackNetwork.cameraShakerNetwork.timer > 0)
                CameraShake.Instance.Shake(player.attackNetwork.cameraShakerNetwork);
            player.sound = "Impact1";
            EnterState(player, "Fall");
        }
    }
}