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
        if (GameSimulation.Hitstop <= 0)
        {
            AfterHitstop(player);
        }
    }
    protected virtual void OnEnter(PlayerNetwork player)
    {
        player.velocity = DemonicsVector2.Zero;
        player.animationFrames = 0;
        if (player.combo == 0)
        {
            player.comboTimerStarter = player.attackHurtNetwork.comboTimerStarter;
            player.comboTimer = ComboTimerStarterTypes.GetComboTimerStarterValue(player.comboTimerStarter);
        }
        player.player.OtherPlayerUI.SetComboTimerActive(true);
        player.combo++;
        player.health -= CalculateDamage(player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.healthRecoverable -= CalculateRecoverableDamage(player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.player.StartShakeContact();
        player.player.PlayerUI.Damaged();
        player.player.OtherPlayerUI.IncreaseCombo(player.combo);
        player.enter = true;
        GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
        player.otherPlayer.canChainAttack = true;
        player.sound = player.attackHurtNetwork.impactSound;
        DemonicsVector2 hurtEffectPosition = DemonicsVector2.Zero;
        if (player.otherPlayer.isAir)
        {
            hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y - (0.1f * -player.flip));
        }
        else
        {
            hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y);
        }
        if (!player.wasWallSplatted)
        {
            player.SetEffect(player.attackHurtNetwork.hurtEffect, hurtEffectPosition);
        }
        if (player.attackHurtNetwork.cameraShakerNetwork.timer > 0)
        {
            CameraShake.Instance.Shake(player.attackHurtNetwork.cameraShakerNetwork);
        }
        player.stunFrames = player.attackHurtNetwork.hitStun;
        player.knockback = 0;
        player.pushbackStart = player.position;
        player.pushbackEnd = new DemonicsVector2(player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
    }

    protected virtual void AfterHitstop(PlayerNetwork player)
    {
        player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        if (player.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.attackHurtNetwork.knockbackDuration)
        {
            Knockback(player);
        }
        player.comboTimer--;
        player.stunFrames--;
        player.player.OtherPlayerUI.SetComboTimer
       (DemonicsFloat.Lerp((DemonicsFloat)1, (DemonicsFloat)0,
        (DemonicsFloat)player.comboTimer / (DemonicsFloat)ComboTimerStarterTypes.GetComboTimerStarterValue(player.comboTimerStarter)), ComboTimerStarterTypes.GetComboTimerStarterColor(player.comboTimerStarter));
    }

    protected virtual void Knockback(PlayerNetwork player)
    {
    }

    public int CalculateDamage(int damage, float defense)
    {
        int calculatedDamage = (int)((DemonicsFloat)damage / (DemonicsFloat)defense);
        return calculatedDamage;
    }
    public int CalculateRecoverableDamage(int damage, float defense)
    {
        int calculatedDamage = (int)((DemonicsFloat)damage / (DemonicsFloat)defense) - 200;
        return calculatedDamage;
    }
}
