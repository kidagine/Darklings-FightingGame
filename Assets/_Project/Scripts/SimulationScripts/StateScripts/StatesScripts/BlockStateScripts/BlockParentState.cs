using System.Collections;
using System.Collections.Generic;
using Demonics;
using UnityEngine;

public class BlockParentState : State
{
    public static bool skipKnockback;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            CameraShake.Instance.Shake(new CameraShakerNetwork()
            { intensity = player.attackHurtNetwork.cameraShakerNetwork.intensity - 4, timer = player.attackHurtNetwork.cameraShakerNetwork.timer });
            OnEnter(player);
            return;
        }
        if (SceneSettings.IsTrainingMode)
            player.framedataEnum = FramedataTypesEnum.Block;
        if (!player.hitstop)
        {
            AfterHitstop(player);
        }
        ToHurtState(player);
        ToShadowbreakState(player);
    }

    protected virtual void OnEnter(PlayerNetwork player)
    {
        player.otherPlayer.ArcanaGain(ArcanaGainTypes.AttackOnBlock);
        player.ArcanaGain(ArcanaGainTypes.DefendOnBlock);
        CheckFlip(player);
        player.otherPlayer.canChainAttack = true;
        player.player.StartShakeContact();
        player.enter = true;
        GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
        player.sound = "Block";
        DemonVector2 hurtEffectPosition = new DemonVector2(player.position.x + (5 * player.flip), player.hurtPosition.y);
        if (player.attackHurtNetwork.hardKnockdown)
        {
            player.SetParticle("Chip", hurtEffectPosition);
            player.health -= 200;
            player.healthRecoverable -= 200;
            player.player.PlayerUI.Damaged();
            player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
        }
        else
        {
            player.SetParticle("Block", hurtEffectPosition);
        }
        player.animationFrames = 0;
        player.stunFrames = player.attackHurtNetwork.blockStun;
        player.knockback = 0;
        player.pushbackStart = player.position;
        if (player.attackHurtNetwork.hardKnockdown)
        {
            player.attackHurtNetwork.knockbackDuration /= 2;
            player.pushbackEnd = new DemonVector2((player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip) / 2), DemonicsPhysics.GROUND_POINT);
        }
        else
        {
            player.pushbackEnd = new DemonVector2(player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
        }
        player.velocity = DemonVector2.Zero;
    }

    protected virtual void AfterHitstop(PlayerNetwork player)
    {
        player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        if (player.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.attackHurtNetwork.knockbackDuration)
        {
            DemonFloat ratio = (DemonFloat)player.knockback / (DemonFloat)player.attackHurtNetwork.knockbackDuration;
            DemonFloat nextX = DemonFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
            DemonVector2 nextPosition = DemonVector2.Zero;
            nextPosition = new DemonVector2(nextX, player.position.y);
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
        player.player.StopShakeCoroutine();
        player.stunFrames--;
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            player.enter = false;
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
            if (IsBlocking(player, true))
            {
                if ((DemonFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonFloat)player.velocity.y <= (DemonFloat)0)
                {
                    if (player.direction.y < 0)
                    {
                        EnterState(player, "BlockLow");
                    }
                    else
                    {
                        EnterState(player, "Block");
                    }
                }
                else
                {
                    EnterState(player, "BlockAir");
                }
            }
            else
            {
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
    }
}