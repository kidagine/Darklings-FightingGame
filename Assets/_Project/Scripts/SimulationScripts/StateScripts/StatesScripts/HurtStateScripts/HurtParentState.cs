using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            OnEnter(player);
        }
        if (!player.hitstop)
        {
            AfterHitstop(player);
        }
    }
    protected virtual void OnEnter(PlayerNetwork player)
    {
        CheckTrainingGauges(player);
        player.shadow.isOnScreen = false;
        player.velocity = DemonicsVector2.Zero;
        player.animationFrames = 0;
        if (player.combo == 0)
        {
            player.comboTimerStarter = player.attackHurtNetwork.comboTimerStarter;
            player.comboTimer = ComboTimerStarterTypes.GetComboTimerStarterValue(player.comboTimerStarter);
        }
        player.player.OtherPlayerUI.SetComboTimerActive(true);
        player.combo++;
        player.health -= CalculateDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.healthRecoverable -= CalculateRecoverableDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.player.StartShakeContact();
        player.player.PlayerUI.Damaged();
        player.player.OtherPlayerUI.IncreaseCombo(player.combo);
        player.enter = true;
        player.otherPlayer.canChainAttack = true;
        player.sound = player.attackHurtNetwork.impactSound;
        if (!player.wasWallSplatted)
        {
            player.SetEffect(player.attackHurtNetwork.hurtEffect, player.hurtPosition);
        }
        if (player.attackHurtNetwork.cameraShakerNetwork.timer > 0)
        {
            CameraShake.Instance.Shake(player.attackHurtNetwork.cameraShakerNetwork);
        }
        player.stunFrames = player.attackHurtNetwork.hitStun;
        player.knockback = 0;
        player.pushbackStart = player.position;
        player.pushbackEnd = new DemonicsVector2(player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
        if (player.health <= 0)
        {
            EnterState(player, "Death");
        }
    }

    protected virtual void AfterHitstop(PlayerNetwork player)
    {
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        if (player.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.attackHurtNetwork.knockbackDuration)
        {
            Knockback(player);
        }
        if (!player.comboLocked)
        {
            player.comboTimer--;
        }
        player.stunFrames--;
        player.player.OtherPlayerUI.SetComboTimer
       (DemonicsFloat.Lerp((DemonicsFloat)0, (DemonicsFloat)1,
        (DemonicsFloat)player.comboTimer / (DemonicsFloat)ComboTimerStarterTypes.GetComboTimerStarterValue(player.comboTimerStarter)), ComboTimerStarterTypes.GetComboTimerStarterColor(player.comboTimerStarter));
    }

    protected virtual void Knockback(PlayerNetwork player)
    {
    }
}
