using Demonics;
using UnityEngine;

public class HurtParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.hitstop)
        {
            AfterHitstop(player);
        }
        if (SceneSettings.IsTrainingMode)
            player.framedataEnum = FramedataTypesEnum.Hurt;
    }
    protected virtual void OnEnter(PlayerNetwork player)
    {
        player.soundGroup = "Hurt";
        player.otherPlayer.ArcanaGain(ArcanaGainTypes.AttackOnHit);
        player.ArcanaGain(ArcanaGainTypes.DefendOnHit);
        CheckTrainingGauges(player);
        player.shadow.isOnScreen = false;
        player.velocity = DemonVector2.Zero;
        player.animationFrames = 0;
        if (player.combo == 0)
        {
            player.comboTimerStarter = player.attackHurtNetwork.comboTimerStarter;
            player.comboTimer = ComboTimerStarterTypes.GetComboTimerStarterValue(player.comboTimerStarter);
        }
        if (player.attackHurtNetwork.moveMaterial == "Fire")
            player.player.PlayerAnimator.FireMaterial();
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
            player.SetParticle(player.attackHurtNetwork.hurtEffect, player.hurtPosition);
        if (player.attackHurtNetwork.cameraShakerNetwork.timer > 0)
            CameraShake.Instance.Shake(player.attackHurtNetwork.cameraShakerNetwork);
        player.stunFrames = player.attackHurtNetwork.hitStun;
        player.knockback = 0;
        player.pushbackStart = player.position;
        player.pushbackEnd = new DemonVector2(player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
        if (player.health <= 0)
        {
            player.soundGroup = "Death";
            CameraShake.Instance.Zoom(30, 0.2f);
            player.invincible = true;
            CameraShake.Instance.Shake(new CameraShakerNetwork() { intensity = 30, timer = 0.5f });
            if (!SceneSettings.IsTrainingMode)
            {
                GameSimulation.GlobalHitstop = 4;
                GameSimulation.Run = false;
                GameSimulation.Timer = GameSimulation._timerMax;
            }
        }
        if (SceneSettings.IsTrainingMode)
            player.framedataEnum = FramedataTypesEnum.Hurt;
    }

    protected virtual void AfterHitstop(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.name.Contains("AR"))
        {
            player.otherPlayer.player.PlayerUI.SetDarkScreen(false);
            GameSimulation.GlobalFreezeFrames = 0;
        }

        GameSimulation.GlobalHitstop = 1;
        if (player.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.attackHurtNetwork.knockbackDuration)
        {
            Knockback(player);
        }
        else
        {
            player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        }
        if (!player.comboLocked)
        {
            player.comboTimer--;
        }
        player.stunFrames--;
        player.player.OtherPlayerUI.SetComboTimer
       (DemonFloat.Lerp((DemonFloat)0, (DemonFloat)1,
        (DemonFloat)player.comboTimer / (DemonFloat)ComboTimerStarterTypes.GetComboTimerStarterValue(player.comboTimerStarter)), ComboTimerStarterTypes.GetComboTimerStarterColor(player.comboTimerStarter));
    }

    protected virtual void Knockback(PlayerNetwork player)
    {
    }
}
