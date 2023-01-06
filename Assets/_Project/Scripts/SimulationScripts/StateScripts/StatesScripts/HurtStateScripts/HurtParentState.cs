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
        player.animationFrames = 0;
        if (player.combo == 0)
        {
            player.comboTimer = ComboTimerStarterTypes.GetComboTimerStarterValue(player.attackHurtNetwork.comboTimerStarter);
        }
        player.player.OtherPlayerUI.SetComboTimerActive(true);
        player.combo++;
        CheckFlip(player);
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
        player.SetEffect(player.attackHurtNetwork.hurtEffect, hurtEffectPosition);
        if (player.attackHurtNetwork.cameraShakerNetwork.timer > 0)
        {
            CameraShake.Instance.Shake(player.attackHurtNetwork.cameraShakerNetwork);
        }
        player.stunFrames = player.attackHurtNetwork.hitStun;
        player.knockback = 0;
        player.attackHurtNetwork.knockbackStart = player.position;
        player.attackHurtNetwork.knockbackEnd = new DemonicsVector2(player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
    }

    protected virtual void AfterHitstop(PlayerNetwork player)
    {
        if (!DemonicsPhysics.IsInCorner(player))
        {
            if (player.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.attackHurtNetwork.knockbackDuration)
            {
                DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.attackHurtNetwork.knockbackDuration;
                DemonicsFloat nextX = DemonicsFloat.Lerp(player.attackHurtNetwork.knockbackStart.x, player.attackHurtNetwork.knockbackEnd.x, ratio);
                DemonicsVector2 nextPosition = new DemonicsVector2(nextX, player.position.y);
                player.position = nextPosition;
                player.knockback++;
            }
        }
        player.comboTimer--;
        player.stunFrames--;
        player.player.OtherPlayerUI.SetComboTimer
       (DemonicsFloat.Lerp((DemonicsFloat)1, (DemonicsFloat)0,
        (DemonicsFloat)player.comboTimer / (DemonicsFloat)ComboTimerStarterTypes.GetComboTimerStarterValue(player.attackHurtNetwork.comboTimerStarter)), ComboTimerStarterTypes.GetComboTimerStarterColor(player.attackHurtNetwork.comboTimerStarter));
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
