using UnityEngine;

public class ThrowState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            if (player.inputBuffer.inputItems[0].inputDirection.x < 0 && player.flip == 1 || player.inputBuffer.inputItems[0].inputDirection.x > 0 && player.flip == -1)
            {
                player.flip *= -1;
            }
            player.enter = true;
            player.animationFrames = 0;
            player.animation = "Throw";
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
        }
        player.otherPlayer.pushbox.active = false;
        player.animationFrames++;
        player.attackFrames--;
        DemonicsVector2 grabPoint = player.player.PlayerAnimator.GetGrabPoint(player.animation, player.animationFrames);
        player.otherPlayer.position = new DemonicsVector2(player.position.x + (grabPoint.x * player.flip), player.position.y + grabPoint.y);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            if (player.attackNetwork.cameraShakerNetwork.timer > 0)
            {
                CameraShake.Instance.Shake(player.attackNetwork.cameraShakerNetwork);
            }
            player.enter = false;
            player.state = "Idle";
            player.sound = "Impact1";
            player.otherPlayer.enter = false;
            player.otherPlayer.state = "HardKnockdown";
            player.otherPlayer.combo++;
            player.otherPlayer.health -= CalculateDamage(player.otherPlayer.attackHurtNetwork.damage, player.otherPlayer.playerStats.Defense);
            player.otherPlayer.healthRecoverable -= CalculateRecoverableDamage(player.otherPlayer.attackHurtNetwork.damage, player.otherPlayer.playerStats.Defense);
            player.otherPlayer.player.PlayerUI.Damaged();
            player.otherPlayer.player.PlayerUI.UpdateHealthDamaged(player.otherPlayer.healthRecoverable);
            player.otherPlayer.player.OtherPlayerUI.IncreaseCombo(player.combo);
            player.otherPlayer.pushbox.active = true;
        }
    }
}