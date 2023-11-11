using UnityEngine;

public class ThrowState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            SetTopPriority(player);
            if (player.direction.x < 0 && player.flip == 1 || player.direction.x > 0 && player.flip == -1)
                player.flip *= -1;
            player.enter = true;
            player.animationFrames = 0;
            player.animation = "Throw";
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            return;
        }
        player.otherPlayer.pushbox.active = false;
        player.animationFrames++;
        player.attackFrames--;
        DemonVector2 grabPoint = player.player.PlayerAnimator.GetGrabPoint(player.animation, player.animationFrames);
        player.otherPlayer.position = new DemonVector2(player.position.x + (grabPoint.x * player.flip), player.position.y + grabPoint.y);
        // if (player.player.PlayerAnimator.GetThrowArcanaEnd(player.animation, player.animationFrames) && player.otherPlayer.state == "Grabbed")
        // {
        // }
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            if (player.attackFrames <= -1 && !player.hitstop)
            {
                ResetCombo(player.otherPlayer);

                player.otherPlayer.pushbox.active = true;
                if (player.health <= 0)
                    EnterState(player.otherPlayer, "Death");
                else
                    EnterState(player.otherPlayer, "HardKnockdown");
                EnterState(player, "Idle");
                return;
            }
            if (!player.hitstop)
            {
                player.otherPlayer.player.StartShakeContact();
                if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT && player.flip == 1)
                    player.position = new DemonVector2(DemonicsPhysics.WALL_RIGHT_POINT - player.pushbox.size.x, player.position.y);
                else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT && player.flip == -1)
                    player.position = new DemonVector2(DemonicsPhysics.WALL_LEFT_POINT + player.pushbox.size.x, player.position.y);
                ThrowEnd(player.otherPlayer);
                CameraShake.Instance.Shake(player.attackNetwork.cameraShakerNetwork);
                player.sound = "Impact1";
            }
        }
    }
}