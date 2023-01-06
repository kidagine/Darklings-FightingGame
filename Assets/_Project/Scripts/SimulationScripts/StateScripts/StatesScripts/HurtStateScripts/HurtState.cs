using UnityEngine;

public class HurtState : HurtParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "Hurt";
        if (player.animationFrames < 4)
        {
            player.animationFrames++;
        }
        ToHurtState(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0 || player.comboTimer <= 0)
        {
            player.combo = 0;
            player.player.OtherPlayerUI.ResetCombo();
            player.player.StopShakeCoroutine();
            player.player.PlayerUI.SetComboTimerActive(false);
            player.player.PlayerUI.UpdateHealthDamaged();
            player.velocity = DemonicsVector2.Zero;
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            player.player.StopShakeCoroutine();
            player.player.PlayerUI.UpdateHealthDamaged();
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
            player.enter = false;
            player.state = "Hurt";
        }
    }
}
