using System;
using UnityEngine;

[Serializable]
public class State
{
    public virtual void UpdateLogic(PlayerNetwork player) { }
    public virtual void Exit() { }
    public virtual bool ToHurtState(PlayerNetwork player, AttackSO attack) { return false; }
    public virtual bool ToBlockState(PlayerNetwork player, AttackSO attack) { return false; }
    public void CheckFlip(PlayerNetwork player)
    {
        if (player.otherPlayer.position.x > player.position.x)
        {
            player.flip = 1;
        }
        else if (player.otherPlayer.position.x < player.position.x)
        {
            player.flip = -1;
        }
    }
    public void SetTopPriority(PlayerNetwork player)
    {
        player.spriteOrder = 1;
        player.otherPlayer.spriteOrder = 0;
    }
    public bool IsBlocking(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.attackType == AttackTypeEnum.Low && player.direction.y > 0)
        {
            return false;
        }
        if (player.attackHurtNetwork.attackType == AttackTypeEnum.Overhead && player.direction.y < 0)
        {
            return false;
        }
        if (player.attackHurtNetwork.attackType == AttackTypeEnum.Break)
        {
            return false;
        }
        if (player.flip == 1 && player.direction.x < 0 || player.flip == -1 && player.direction.x > 0)
        {
            return true;
        }
        return false;
    }
    protected void Attack(PlayerNetwork player, bool air = false)
    {
        player.pushbackDuration = 0;
        player.attackInput = player.inputBuffer.inputItems[0].inputEnum;
        player.attackPress = false;
        player.isCrouch = false;
        player.isAir = air;
        if (player.inputBuffer.inputItems[0].inputDirection.y < 0)
        {
            player.isCrouch = true;
        }
        if (player.isAir)
        {
            player.isCrouch = false;
        }
        AttackSO attack = PlayerComboSystem.GetComboAttack(player.playerStats, player.attackInput, player.isCrouch, player.isAir);
        SetAttack(player, attack);
        player.enter = false;
        player.state = "Attack";
    }
    protected void Arcana(PlayerNetwork player, bool air = false)
    {
        //if (player.arcanaGauge >= PlayerStatsSO.ARCANA_MULTIPLIER)
        player.attackInput = player.inputBuffer.inputItems[0].inputEnum;
        player.arcanaPress = false;
        player.isAir = air;
        player.isCrouch = false;
        if (player.inputBuffer.inputItems[0].inputDirection.y < 0)
        {
            player.isCrouch = true;
        }
        ArcanaSO attack = PlayerComboSystem.GetArcana(player.playerStats, player.isCrouch, player.isAir);
        SetArcana(player, attack);
        player.enter = false;
        player.state = "Arcana";
    }
    protected void SetArcana(PlayerNetwork player, ArcanaSO attack)
    {
        player.attackNetwork = new AttackNetwork()
        {
            damage = attack.damage,
            travelDistance = new DemonicsVector2((DemonicsFloat)attack.travelDistance.x, (DemonicsFloat)attack.travelDistance.y),
            name = attack.name,
            moveName = attack.moveName,
            attackSound = attack.attackSound,
            hurtEffect = attack.hurtEffect,
            knockbackForce = (DemonicsFloat)attack.knockbackForce.x,
            knockbackDuration = attack.knockbackDuration,
            hitstop = attack.hitstop,
            impactSound = attack.impactSound,
            hitStun = attack.hitStun,
            knockbackArc = attack.knockbackArc,
            jumpCancelable = attack.jumpCancelable,
            softKnockdown = attack.causesSoftKnockdown,
            hardKnockdown = attack.causesKnockdown,
            projectilePosition = new DemonicsVector2((DemonicsFloat)attack.hitEffectPosition.x, (DemonicsFloat)attack.hitEffectPosition.y),
            comboTimerStarter = player.attackInput == InputEnum.Heavy ? ComboTimerStarterEnum.Red : ComboTimerStarterEnum.Yellow,
            attackType = attack.attackTypeEnum,
            superArmor = attack.hasSuperArmor,
            projectileSpeed = attack.projectileSpeed,
            projectileDestroyOnHit = attack.projectileDestroyOnHit
        };
        if (attack.cameraShaker != null)
        {
            player.attackNetwork.cameraShakerNetwork = new CameraShakerNetwork() { intensity = attack.cameraShaker.intensity, timer = attack.cameraShaker.timer };
        }
    }
    protected void SetAttack(PlayerNetwork player, AttackSO attack)
    {
        player.attackNetwork = new AttackNetwork()
        {
            damage = attack.damage,
            travelDistance = new DemonicsVector2((DemonicsFloat)attack.travelDistance.x, (DemonicsFloat)attack.travelDistance.y),
            name = attack.name,
            moveName = attack.moveName,
            attackSound = attack.attackSound,
            hurtEffect = attack.hurtEffect,
            knockbackForce = (DemonicsFloat)attack.knockbackForce.x,
            knockbackDuration = attack.knockbackDuration,
            hitstop = attack.hitstop,
            impactSound = attack.impactSound,
            hitStun = attack.hitStun,
            knockbackArc = attack.knockbackArc,
            jumpCancelable = attack.jumpCancelable,
            softKnockdown = attack.causesSoftKnockdown,
            hardKnockdown = attack.causesKnockdown,
            projectilePosition = new DemonicsVector2((DemonicsFloat)attack.hitEffectPosition.x, (DemonicsFloat)attack.hitEffectPosition.y),
            comboTimerStarter = player.attackInput == InputEnum.Heavy ? ComboTimerStarterEnum.Red : ComboTimerStarterEnum.Yellow,
            attackType = attack.attackTypeEnum,
            superArmor = attack.hasSuperArmor
        };
        if (attack.cameraShaker != null)
        {
            player.attackNetwork.cameraShakerNetwork = new CameraShakerNetwork() { intensity = attack.cameraShaker.intensity, timer = attack.cameraShaker.timer };
        }
    }
    public void ResetPlayer(PlayerNetwork player)
    {
        player.healthRecoverable = 10000;
        player.health = 10000;
        player.player.PlayerUI.CheckDemonLimit(player.health);
        player.enter = false;
    }
    protected int CalculateDamage(int damage, float defense)
    {
        int calculatedDamage = (int)((DemonicsFloat)damage / (DemonicsFloat)defense);
        return calculatedDamage;
    }
    protected int CalculateRecoverableDamage(int damage, float defense)
    {
        int calculatedDamage = (int)((DemonicsFloat)damage / (DemonicsFloat)defense) - 100;
        return calculatedDamage;
    }
    protected void ThrowEnd(PlayerNetwork player)
    {
        player.enter = false;
        player.state = "HardKnockdown";
        player.combo++;
        player.health -= CalculateDamage(player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.healthRecoverable -= CalculateRecoverableDamage(player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.player.PlayerUI.Damaged();
        player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
        player.player.OtherPlayerUI.IncreaseCombo(player.combo);
        player.pushbox.active = true;
    }
    protected bool IsColliding(PlayerNetwork player)
    {
        for (int i = 0; i < player.otherPlayer.projectiles.Length; i++)
        {
            if (DemonicsCollider.Colliding(player.otherPlayer.projectiles[i].hitbox, player.hurtbox))
            {
                GameSimulation.Hitstop = player.otherPlayer.attackNetwork.hitstop;
                player.hitstop = true;
                player.otherPlayer.projectiles[i].hitstop = true;
                player.hurtPosition = new DemonicsVector2(player.position.x, player.position.y + (player.pushbox.size.y / 2));
                if (player.otherPlayer.projectiles[i].destroyOnHit)
                {
                    player.otherPlayer.projectiles[i].animationFrames = 0;
                    player.otherPlayer.projectiles[i].active = false;
                    player.otherPlayer.projectiles[i].hitbox.active = false;
                }
                return true;
            }
        }
        if (DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            GameSimulation.Hitstop = player.otherPlayer.attackNetwork.hitstop;
            player.hitstop = true;
            player.otherPlayer.hitstop = true;
            if (player.otherPlayer.isAir)
            {
                player.hurtPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y - (0.1f * -player.flip));
            }
            else
            {
                player.hurtPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y);
            }
            return true;
        }
        return false;
    }
};