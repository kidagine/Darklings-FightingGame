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
        player.attackNetwork = SetAttack(player.attackInput, attack);
        player.enter = false;
        player.state = "Attack";
    }
    protected void Arcana(PlayerNetwork player, bool air = false)
    {
        player.attackInput = player.inputBuffer.inputItems[0].inputEnum;
        player.arcanaPress = false;
        player.isAir = air;
        player.isCrouch = false;
        if (player.inputBuffer.inputItems[0].inputDirection.y < 0)
        {
            player.isCrouch = true;
        }
        ArcanaSO attack = PlayerComboSystem.GetArcana(player.playerStats, player.isCrouch, player.isAir);
        if (attack != null)
        {
            player.attackNetwork = SetArcana(player.attackInput, attack);
            player.enter = false;
            player.state = "Arcana";
        }
    }
    protected AttackNetwork SetArcana(InputEnum input, ArcanaSO attack)
    {
        AttackNetwork attackNetwork = new AttackNetwork()
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
            comboTimerStarter = input == InputEnum.Heavy ? ComboTimerStarterEnum.Red : ComboTimerStarterEnum.Yellow,
            attackType = attack.attackTypeEnum,
            superArmor = attack.hasSuperArmor,
            projectileSpeed = (DemonicsFloat)attack.projectileSpeed,
            projectileDestroyOnHit = attack.projectileDestroyOnHit,
            projectilePriority = attack.projectilePriority
        };
        if (attack.cameraShaker != null)
        {
            attackNetwork.cameraShakerNetwork = new CameraShakerNetwork() { intensity = attack.cameraShaker.intensity, timer = attack.cameraShaker.timer };
        }
        return attackNetwork;
    }
    protected AttackNetwork SetAttack(InputEnum input, AttackSO attack)
    {
        AttackNetwork attackNetwork = new AttackNetwork()
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
            comboTimerStarter = input == InputEnum.Heavy ? ComboTimerStarterEnum.Red : ComboTimerStarterEnum.Yellow,
            attackType = attack.attackTypeEnum,
            superArmor = attack.hasSuperArmor
        };
        if (attack.cameraShaker != null)
        {
            attackNetwork.cameraShakerNetwork = new CameraShakerNetwork() { intensity = attack.cameraShaker.intensity, timer = attack.cameraShaker.timer };
        }
        return attackNetwork;
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
        player.combo++;
        player.health -= CalculateDamage(player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.healthRecoverable -= CalculateRecoverableDamage(player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.player.PlayerUI.Damaged();
        player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
        player.player.OtherPlayerUI.IncreaseCombo(player.combo);
        player.pushbox.active = true;
        player.enter = false;
        player.state = "HardKnockdown";
    }
    protected bool IsColliding(PlayerNetwork player)
    {
        if (DemonicsCollider.Colliding(player.otherPlayer.shadow.projectile.hitbox, player.hurtbox))
        {
            player.attackHurtNetwork = player.otherPlayer.shadow.projectile.attackNetwork;
            GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
            player.hitstop = true;
            player.hitbox.enter = true;
            player.otherPlayer.shadow.projectile.hitstop = true;
            player.hurtPosition = new DemonicsVector2(player.position.x, player.position.y + (player.pushbox.size.y / 2));
            if (player.otherPlayer.shadow.projectile.destroyOnHit)
            {
                player.otherPlayer.shadow.projectile.hitstop = false;
                player.otherPlayer.shadow.projectile.animationFrames = 0;
                player.otherPlayer.shadow.projectile.active = false;
                player.otherPlayer.shadow.projectile.hitbox.active = false;
            }
            return true;
        }
        for (int i = 0; i < player.otherPlayer.projectiles.Length; i++)
        {
            if (DemonicsCollider.Colliding(player.otherPlayer.projectiles[i].hitbox, player.hurtbox) && !player.otherPlayer.projectiles[i].hitbox.enter)
            {
                player.attackHurtNetwork = player.otherPlayer.projectiles[i].attackNetwork;
                GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
                player.hitstop = true;
                player.otherPlayer.projectiles[i].hitbox.enter = true;
                player.otherPlayer.projectiles[i].hitstop = true;
                player.hurtPosition = new DemonicsVector2(player.position.x, player.position.y + (player.pushbox.size.y / 2));
                if (player.otherPlayer.projectiles[i].destroyOnHit)
                {
                    player.otherPlayer.projectiles[i].hitbox.enter = false;
                    player.otherPlayer.projectiles[i].hitstop = false;
                    player.otherPlayer.projectiles[i].animationFrames = 0;
                    player.otherPlayer.projectiles[i].active = false;
                    player.otherPlayer.projectiles[i].hitbox.active = false;
                }
                return true;
            }
        }
        if (DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox) && !player.otherPlayer.hitbox.enter)
        {
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
            GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
            player.hitstop = true;
            player.otherPlayer.hitbox.enter = true;
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

    protected void Shadow(PlayerNetwork player)
    {
        if (player.shadowPress && !player.shadow.isOnScreen)
        {
            player.shadow.projectile.attackNetwork = SetAttack(player.attackInput, player.shadow.attack);
            player.shadow.projectile.flip = player.flip == 1 ? false : true;
            player.shadow.animationFrames = 0;
            player.shadow.isOnScreen = true;
            player.shadow.flip = player.flip;
            player.shadow.position = new DemonicsVector2(player.position.x + (player.shadow.spawnPoint.x * player.flip), player.position.y + player.shadow.spawnPoint.y);
            if (player.shadowGauge > 1000)
            {
                player.shadowGauge -= 1000;
            }
        }
    }
};