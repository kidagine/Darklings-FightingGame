using UnityEngine;

public class HurtState : HurtParentState
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
        player.animation = "Hurt";
        player.velocity = DemonVector2.Zero;

        ToIdleState(player);
        ToHurtState(player);
        ToShadowbreakState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0 || player.comboTimer <= 0)
        {
            if (SceneSettings.IsTrainingMode && player.isAi)
                TrainingSettings.BlockCountCurrent = TrainingSettings.BlockCount;
            ResetCombo(player);
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            player.velocity = DemonVector2.Zero;
            if (player.health <= 0)
            {
                EnterState(player, "Death");
                return;
            }
            if (AIHurt(player))
                return;

            EnterState(player, "Idle");
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
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
            {
                EnterState(player, "Airborne");
            }
            else
            {
                if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                {
                    EnterState(player, "Hurt");
                }
                else
                {
                    EnterState(player, "HurtAir");
                }
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
