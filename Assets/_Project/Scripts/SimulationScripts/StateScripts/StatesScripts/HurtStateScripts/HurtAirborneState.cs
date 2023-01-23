using UnityEngine;

public class HurtAirborneState : HurtParentState
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
            if (player.attackHurtNetwork.hardKnockdown)
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
            if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT && player.flip == 1
            || player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT && player.flip == -1)
            {
                player.player.StopShakeCoroutine();
                EnterState(player, "WallSplat");
            }
        }
    }
    private void ToKnockdownState(PlayerNetwork player)
    {
        if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0 && player.knockback > 1)
        {
            player.comboLocked = false;
            ResetCombo(player);
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
            player.wasWallSplatted = false;
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
        DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.attackHurtNetwork.knockbackDuration;
        DemonicsFloat distance = player.pushbackEnd.x - player.pushbackStart.x;
        DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
        DemonicsFloat baseY = DemonicsFloat.Lerp(player.pushbackStart.y, player.pushbackEnd.y, (nextX - player.pushbackStart.x) / distance);
        DemonicsFloat arc = player.attackHurtNetwork.knockbackArc * (nextX - player.pushbackStart.x) * (nextX - player.pushbackEnd.x) / ((-0.25f) * distance * distance);
        DemonicsVector2 nextPosition = DemonicsVector2.Zero;
        if (player.attackHurtNetwork.softKnockdown)
        {
            nextPosition = new DemonicsVector2(nextX, baseY + arc);
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