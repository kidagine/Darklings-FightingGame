using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAirState : HurtParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            OnEnter(player);
            return;
        }
        if (player.enter)
            if (player.animationFrames < 4)
            {
                player.animationFrames++;
            }
        base.UpdateLogic(player);
        player.animation = "HurtAir";
        ToHurtState(player);
        ToFallState(player);
        ToShadowbreakState(player);
    }

    protected override void AfterHitstop(PlayerNetwork player)
    {
        if (player.stunFrames == player.attackHurtNetwork.hitStun && player.attackHurtNetwork.knockbackArc == 0)
        {
            player.velocity = new DemonVector2((DemonFloat)0, (DemonFloat)1.1);
        }
        base.AfterHitstop(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.knockbackArc > 0 && player.knockback <= 1)
        {
            return;
        }
        if ((DemonFloat)player.position.y <= DemonicsPhysics.GROUND_POINT)
        {
            player.player.StopShakeCoroutine();
            if (player.health <= 0)
            {
                EnterState(player, "Death");
                return;
            }
            if (player.stunFrames <= 0 || player.comboTimer <= 0)
            {
                if (SceneSettings.IsTrainingMode && player.isAi)
                    TrainingSettings.BlockCountCurrent = TrainingSettings.BlockCount;
                ResetCombo(player);
                player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
                EnterState(player, "Idle");

            }
            else
            {
                player.stunFrames = player.attackHurtNetwork.hitStun;
                player.velocity = DemonVector2.Zero;
                player.animationFrames = 0;
                EnterState(player, "Hurt", true);
            }
        }
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.health <= 0)
        {
            return;
        }
        if (player.stunFrames <= 0 || player.comboTimer <= 0)
        {
            if (SceneSettings.IsTrainingMode && player.isAi)
                TrainingSettings.BlockCountCurrent = TrainingSettings.BlockCount;
            ResetCombo(player);
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            if (AIHurt(player))
                return;
            EnterState(player, "Fall");
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.player.StopShakeCoroutine();
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                EnterState(player, "Grabbed");
                return;
            }
            if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.softKnockdown)
            {
                EnterState(player, "Airborne");
            }
            else
            {
                EnterState(player, "HurtAir");
            }
        }
    }
    protected override void OnEnter(PlayerNetwork player)
    {
        CheckFlip(player);
        base.OnEnter(player);
    }
    protected override void Knockback(PlayerNetwork player)
    {
        DemonFloat ratio = (DemonFloat)player.knockback / (DemonFloat)player.attackHurtNetwork.knockbackDuration;
        DemonFloat distance = player.pushbackEnd.x - player.pushbackStart.x;
        DemonFloat nextX = DemonFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
        DemonFloat baseY = DemonFloat.Lerp(player.pushbackStart.y, player.pushbackEnd.y, (nextX - player.pushbackStart.x) / distance);
        DemonFloat arc = player.attackHurtNetwork.knockbackArc * (nextX - player.pushbackStart.x) * (nextX - player.pushbackEnd.x) / ((-0.25f) * distance * distance);
        DemonVector2 nextPosition = DemonVector2.Zero;
        if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
        {
            nextPosition = new DemonVector2(nextX, player.position.y);
        }
        else
        {
            nextPosition = new DemonVector2(nextX, baseY + arc);
        }
        player.position = nextPosition;
        if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
        {
            player.position = new DemonVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
        }
        else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
        {
            player.position = new DemonVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
        }
        player.knockback++;
    }
}

