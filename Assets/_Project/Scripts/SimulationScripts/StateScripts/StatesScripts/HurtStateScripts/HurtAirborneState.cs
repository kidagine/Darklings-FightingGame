using Demonics;
using UnityEngine;

public class HurtAirborneState : HurtParentState
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
        ToKnockdownState(player);
        ToWallSplatState(player);
    }

    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.wasWallSplatted = false;
            CheckFlip(player);
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
                player.comboLocked = false;
                EnterState(player, "HurtAir");
            }
        }
    }
    private void ToWallSplatState(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.hardKnockdown && !player.wasWallSplatted)
        {
            if (player.health <= 0)
            {
                return;
            }
            if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT && player.flip == 1
            || player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT && player.flip == -1)
            {
                player.pushbox.active = true;
                player.player.StopShakeCoroutine();
                EnterState(player, "WallSplat");
            }
        }
    }
    private void ToKnockdownState(PlayerNetwork player)
    {
        if ((DemonFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonFloat)player.velocity.y <= (DemonFloat)0 && player.knockback > 1)
        {
            if (SceneSettings.IsTrainingMode && player.isAi)
                TrainingSettings.BlockCountCurrent = TrainingSettings.BlockCount;
            player.pushbox.active = true;
            player.comboLocked = false;
            ResetCombo(player);
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            player.wasWallSplatted = false;
            if (player.health <= 0)
            {
                EnterState(player, "Death");
                return;
            }
            if (player.attackHurtNetwork.softKnockdown)
            {
                EnterState(player, "SoftKnockdown");
            }
            else
            {
                EnterState(player, "HardKnockdown");
            }
        }
    }
    protected override void Knockback(PlayerNetwork player)
    {
        DemonFloat ratio = (DemonFloat)player.knockback / (DemonFloat)player.attackHurtNetwork.knockbackDuration;
        DemonFloat distance = player.pushbackEnd.x - player.pushbackStart.x;
        DemonFloat nextX = DemonFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
        DemonFloat baseY = DemonFloat.Lerp(player.pushbackStart.y, player.pushbackEnd.y, (nextX - player.pushbackStart.x) / distance);
        DemonFloat arc = player.attackHurtNetwork.knockbackArc * (nextX - player.pushbackStart.x) * (nextX - player.pushbackEnd.x) / ((-0.25f) * distance * distance);
        DemonVector2 nextPosition = DemonVector2.Zero;
        if (player.attackHurtNetwork.softKnockdown)
        {
            nextPosition = new DemonVector2(nextX, baseY + arc);
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