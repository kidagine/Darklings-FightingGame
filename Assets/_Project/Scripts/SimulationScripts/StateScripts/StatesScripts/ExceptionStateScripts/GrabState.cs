
using UnityEngine;

public class GrabState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.soundGroup = "Throw";
            SetTopPriority(player);
            CheckFlip(player);
            player.enter = true;
            player.hitbox.enter = false;
            player.animationFrames = 0;
            player.animation = "Grab";
            player.sound = "Hit";
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            UpdateFramedata(player);
            return;
        }

        UpdateFramedata(player);
        player.animationFrames++;
        player.attackFrames--;
        player.velocity = DemonVector2.Zero;
        ToIdleState(player);
        ToHurtState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            EnterState(player, "Idle");
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.enter = false;
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                EnterState(player, "Grabbed");
                return;
            }
            if (DemonicsPhysics.IsInCorner(player))
            {
                player.otherPlayer.knockback = 0;
                player.otherPlayer.pushbackStart = player.otherPlayer.position;
                player.otherPlayer.pushbackEnd = new DemonVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
                player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
            }
            player.player.StopShakeCoroutine();
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            if (player.attackHurtNetwork.hardKnockdown)
                EnterState(player, "Airborne");
            else
            {
                if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                    EnterState(player, "Hurt");
                else
                    EnterState(player, "HurtAir");
            }
        }
    }
}