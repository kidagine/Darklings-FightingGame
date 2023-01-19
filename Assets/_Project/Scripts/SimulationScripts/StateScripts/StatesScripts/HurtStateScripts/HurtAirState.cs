using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAirState : HurtParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "HurtAir";
        if (player.animationFrames < 4)
        {
            player.animationFrames++;
        }
        ToHurtState(player);
        ToFallState(player);
        ToShadowbreakState(player);
    }

    protected override void AfterHitstop(PlayerNetwork player)
    {
        base.AfterHitstop(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.knockbackArc > 0 && player.knockback <= 1)
        {
            return;
        }
        if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT)
        {
            player.player.StopShakeCoroutine();
            if (player.stunFrames <= 0 || player.comboTimer <= 0)
            {
                player.combo = 0;
                player.player.OtherPlayerUI.ResetCombo();
                player.player.PlayerUI.SetComboTimerActive(false);
                player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
                player.enter = false;
                player.state = "Idle";
            }
            else
            {
                player.stunFrames = player.attackHurtNetwork.hitStun;
                player.velocity = DemonicsVector2.Zero;
                player.animationFrames = 0;
                player.state = "Hurt";
            }
        }
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0 || player.comboTimer <= 0)
        {
            player.combo = 0;
            player.player.OtherPlayerUI.ResetCombo();
            player.player.StopShakeCoroutine();
            player.player.PlayerUI.SetComboTimerActive(false);
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            player.enter = false;
            player.state = "Fall";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.player.StopShakeCoroutine();
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            player.enter = false;
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                player.state = "Grabbed";
                return;
            }
            if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.softKnockdown)
            {
                player.state = "Airborne";
            }
            else
            {
                player.state = "HurtAir";
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
        DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.attackHurtNetwork.knockbackDuration;
        DemonicsFloat distance = player.pushbackEnd.x - player.pushbackStart.x;
        DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
        DemonicsFloat baseY = DemonicsFloat.Lerp(player.pushbackStart.y, player.pushbackEnd.y, (nextX - player.pushbackStart.x) / distance);
        DemonicsFloat arc = player.attackHurtNetwork.knockbackArc * (nextX - player.pushbackStart.x) * (nextX - player.pushbackEnd.x) / ((-0.25f) * distance * distance);
        DemonicsVector2 nextPosition = DemonicsVector2.Zero;
        if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
        {
            nextPosition = new DemonicsVector2(nextX, player.position.y);
        }
        else
        {
            nextPosition = new DemonicsVector2(nextX, baseY + arc);
        }
        player.position = nextPosition;
        if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
        {
            player.position = new DemonicsVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
        }
        else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
        {
            player.position = new DemonicsVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
        }
        player.knockback++;
    }
}

