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
        if (player.attackHurtNetwork.attackType == AttackTypeEnum.Break)
        {
            return false;
        }
        if (AIBlocking(player))
        {
            return true;
        }
        if (player.attackHurtNetwork.attackType == AttackTypeEnum.Low && player.direction.y >= 0)
        {
            return false;
        }
        if (player.attackHurtNetwork.attackType == AttackTypeEnum.Overhead && player.direction.y < 0)
        {
            return false;
        }
        if (player.flip == 1 && player.direction.x < 0 || player.flip == -1 && player.direction.x > 0)
        {
            return true;
        }
        return false;
    }
    protected void RedFrenzy(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentInput().pressed && player.inputBuffer.CurrentInput().inputEnum == InputEnum.RedFrenzy)
        {
            if (player.healthRecoverable > player.health)
            {
                AttackSO attack = PlayerComboSystem.GetRedFrenzy(player.playerStats);
                player.attackNetwork = SetAttack(player.attackInput, attack);
                EnterState(player, "RedFrenzy");
            }
        }
    }
    protected void Attack(PlayerNetwork player, bool air = false)
    {
        if ((player.inputBuffer.CurrentInput().inputEnum == InputEnum.Light || player.inputBuffer.CurrentInput().inputEnum == InputEnum.Medium
        || player.inputBuffer.CurrentInput().inputEnum == InputEnum.Heavy))
        {
            player.pushbackDuration = 0;
            player.attackInput = player.inputBuffer.inputItems[player.inputBuffer.index].inputEnum;
            player.isCrouch = false;
            player.isAir = air;
            if (player.direction.y < 0)
            {
                player.isCrouch = true;
            }
            if (player.isAir)
            {
                player.isCrouch = false;
            }
            AttackSO attack = PlayerComboSystem.GetComboAttack(player.playerStats, player.attackInput, player.isCrouch, player.isAir);
            player.attackNetwork = SetAttack(player.attackInput, attack);
            EnterState(player, "Attack");
        }
    }
    protected void Arcana(PlayerNetwork player, bool air = false)
    {
        if (player.arcanaGauge > 1000 && player.inputBuffer.CurrentInput().inputEnum == InputEnum.Special)
        {
            player.attackInput = player.inputBuffer.CurrentInput().inputEnum;
            player.isAir = air;
            player.isCrouch = false;
            if (player.direction.y < 0)
            {
                player.isCrouch = true;
            }
            ArcanaSO attack = PlayerComboSystem.GetArcana(player.playerStats, player.isCrouch, player.isAir);
            if (attack != null)
            {
                player.attackNetwork = SetArcana(player.attackInput, attack);
                EnterState(player, "Arcana");
            }
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
            superArmor = attack.hasSuperArmor,
            startup = attack.startUpFrames,
            active = attack.activeFrames,
            recovery = attack.recoveryFrames
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
        player.shadowGauge = 2000;
        player.arcanaGauge = 0;
        player.player.PlayerUI.CheckDemonLimit(player.health);
        player.enter = false;
        CheckTrainingGauges(player);
    }
    public void ResetPlayerTraining(PlayerNetwork player)
    {
        ResetPlayer(player);
        ResetCombo(player);
        GameSimulation.Hitstop = 0;
        player.shadow.isOnScreen = false;
        player.shadow.projectile.active = false;
        for (int i = 0; i < player.projectiles.Length; i++)
        {
            player.projectiles[i].active = false;
        }
        EnterState(player, "Idle");
    }
    protected int CalculateDamage(PlayerNetwork player, int damage, float defense)
    {
        DemonicsFloat calculatedDamage = (DemonicsFloat)damage / (DemonicsFloat)defense;
        if (player.combo > 1)
        {
            DemonicsFloat damageScale = (DemonicsFloat)1;
            for (int i = 0; i < player.combo; i++)
            {
                damageScale *= (DemonicsFloat)0.97;
            }
            calculatedDamage *= damageScale;
        }
        int calculatedIntDamage = (int)calculatedDamage;
        SetResultAttack(player, calculatedIntDamage, player.attackHurtNetwork);
        return calculatedIntDamage;
    }
    protected int CalculateRecoverableDamage(PlayerNetwork player, int damage, float defense)
    {
        DemonicsFloat calculatedDamage = ((DemonicsFloat)damage / (DemonicsFloat)defense) - 120;
        if (player.combo > 1)
        {
            DemonicsFloat damageScale = (DemonicsFloat)1;
            for (int i = 0; i < player.combo; i++)
            {
                damageScale *= (DemonicsFloat)0.97;
            }
            calculatedDamage *= damageScale;
        }
        int calculatedIntDamage = (int)calculatedDamage;
        return calculatedIntDamage;
    }

    private void SetResultAttack(PlayerNetwork player, int calculatedDamage, AttackNetwork attack)
    {
        player.otherPlayer.resultAttack.startUpFrames = attack.startup;
        player.otherPlayer.resultAttack.activeFrames = attack.active;
        player.otherPlayer.resultAttack.recoveryFrames = attack.recovery;
        player.otherPlayer.resultAttack.attackTypeEnum = attack.attackType;
        player.otherPlayer.resultAttack.damage = calculatedDamage;
        player.otherPlayer.resultAttack.comboDamage += calculatedDamage;
    }

    protected void ThrowEnd(PlayerNetwork player)
    {
        player.combo++;
        player.health -= CalculateDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.healthRecoverable -= CalculateRecoverableDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.player.PlayerUI.Damaged();
        player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
        player.player.OtherPlayerUI.IncreaseCombo(player.combo);
        ResetCombo(player);
        player.pushbox.active = true;
        EnterState(player, "HardKnockdown");
        if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
        {
            player.position = new DemonicsVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
        }
        else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
        {
            player.position = new DemonicsVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
        }
    }
    public void CheckTrainingComboEnd(PlayerNetwork player, bool skipCombo = false)
    {
        if (SceneSettings.IsTrainingMode)
        {
            if (GameplayManager.Instance.InfiniteHealth)
            {
                player.health = 10000;
                player.healthRecoverable = 10000;
            }
            player.player.PlayerUI.CheckDemonLimit(player.health);
            CheckTrainingGauges(player.otherPlayer, skipCombo);
        }
    }
    public void CheckTrainingGauges(PlayerNetwork player, bool skipCombo = false)
    {
        if (SceneSettings.IsTrainingMode && player.otherPlayer.combo == 0 || skipCombo)
        {
            if (GameplayManager.Instance.InfiniteArcana)
            {
                player.arcanaGauge = player.playerStats.Arcana;
            }
            if (GameplayManager.Instance.InfiniteAssist)
            {
                player.shadowGauge = 2000;
            }
        }
    }

    public void EnterState(PlayerNetwork player, string name, bool skipEnter = false)
    {
        player.enter = skipEnter;
        player.state = name;
        StateSimulation.SetState(player);
    }

    protected void HitstopFully(PlayerNetwork player)
    {
        player.hitstop = true;
        player.shadow.projectile.hitstop = true;
        for (int i = 0; i < player.projectiles.Length; i++)
        {
            player.projectiles[i].hitstop = true;
        }
    }

    public bool AIBlocking(PlayerNetwork player)
    {
        if (SceneSettings.IsTrainingMode && player.isAi)
        {
            if (TrainingSettings.BlockAlways)
            {
                return true;
            }
        }
        return false;
    }
    protected void ResetCombo(PlayerNetwork player)
    {
        player.otherPlayer.resultAttack.comboDamage = 0;
        player.player.PlayerUI.SetComboTimerActive(false);
        player.player.StopShakeCoroutine();
        player.player.OtherPlayerUI.ResetCombo();
        player.combo = 0;
        player.comboLocked = false;
        CheckTrainingComboEnd(player);
    }
    protected bool IsColliding(PlayerNetwork player)
    {
        if (player.invincible)
        {
            return false;
        }
        if (player.otherPlayer.shadow.projectile.active)
        {
            if (DemonicsCollider.Colliding(player.otherPlayer.shadow.projectile.hitbox, player.hurtbox) && !player.otherPlayer.shadow.projectile.hitbox.enter)
            {
                player.attackHurtNetwork = player.otherPlayer.shadow.projectile.attackNetwork;
                GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
                player.otherPlayer.shadow.projectile.hitstop = true;
                player.otherPlayer.shadow.projectile.hitbox.enter = true;
                player.otherPlayer.shadow.projectile.hitstop = true;
                player.hurtPosition = new DemonicsVector2(player.otherPlayer.shadow.projectile.hitbox.position.x + ((player.otherPlayer.shadow.projectile.hitbox.size.x / 2) * -player.flip), player.otherPlayer.shadow.projectile.hitbox.position.y);
                if (player.otherPlayer.shadow.projectile.destroyOnHit)
                {
                    player.otherPlayer.shadow.projectile.hitbox.enter = false;
                    player.otherPlayer.shadow.projectile.hitstop = false;
                    player.otherPlayer.shadow.projectile.animationFrames = 0;
                    player.otherPlayer.shadow.projectile.active = false;
                    player.otherPlayer.shadow.projectile.hitbox.active = false;
                }
                return true;
            }
        }
        for (int i = 0; i < player.otherPlayer.projectiles.Length; i++)
        {
            if (player.otherPlayer.projectiles[i].active)
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
        }
        if (DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox) && !player.otherPlayer.hitbox.enter)
        {
            if (player.otherPlayer.attackNetwork.attackType == AttackTypeEnum.Throw && player.stunFrames == 0 && (DemonicsFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
            {
                return false;
            }
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
        if (player.inputBuffer.CurrentInput().pressed && player.inputBuffer.CurrentInput().inputEnum == InputEnum.Assist)
        {
            if (!player.shadow.isOnScreen && player.shadowGauge > 1000)
            {
                player.sound = "Shadow";
                player.shadow.projectile.attackNetwork = SetAttack(player.attackInput, player.shadow.attack);
                player.shadow.projectile.flip = player.flip == 1 ? false : true;
                player.shadow.projectile.fire = true;
                player.shadow.animationFrames = 0;
                player.shadow.isOnScreen = true;
                player.shadow.flip = player.flip;
                player.shadow.position = new DemonicsVector2(player.position.x + (player.shadow.spawnPoint.x * player.flip), player.position.y + player.shadow.spawnPoint.y);
                player.shadowGauge -= 1000;
            }
        }
    }
    protected void ToShadowbreakState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentInput().pressed && player.inputBuffer.CurrentInput().inputEnum == InputEnum.Assist)
        {
            if (player.shadowGauge == 2000)
            {
                ResetCombo(player);
                player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
                player.velocity = DemonicsVector2.Zero;
                player.shadowGauge -= 2000;
                EnterState(player, "Shadowbreak");
            }
        }
    }
};