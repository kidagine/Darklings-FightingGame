using System;
using UnityEngine;
using static TrainingSettings;

[Serializable]
public class State
{
    public virtual void UpdateLogic(PlayerNetwork player) { }
    public virtual void Exit(PlayerNetwork player) { }
    public virtual bool ToHurtState(PlayerNetwork player, AttackSO attack) { return false; }
    public virtual bool ToBlockState(PlayerNetwork player, AttackSO attack) { return false; }
    public string StateName { get; set; }
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
    public bool IsBlocking(PlayerNetwork player, bool ignoreGuardBreak = false)
    {
        if (ignoreGuardBreak && player.attackHurtNetwork.guardBreak)
            player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.Lock);
        if (player.attackHurtNetwork.guardBreak && !ignoreGuardBreak)
            return false;
        if (AIBlocking(player, player.attackHurtNetwork.attackType))
            return true;
        if (player.attackHurtNetwork.attackType == AttackTypeEnum.Low && player.direction.y >= 0)
            return false;
        if (player.attackHurtNetwork.attackType == AttackTypeEnum.Overhead && player.direction.y < 0)
            return false;
        if (player.flip == 1 && player.direction.x < 0 || player.flip == -1 && player.direction.x > 0)
            return true;
        return false;
    }
    protected bool RedFrenzy(PlayerNetwork player)
    {
        if (player.inputBuffer.GetRedFrenzy())
            if (player.healthRecoverable > player.health)
            {
                AttackSO attack = PlayerComboSystem.GetRedFrenzy(player.playerStats);
                player.attackNetwork = SetAttack(player.attackInput, attack);
                EnterState(player, "RedFrenzy");
                return true;
            }
        return false;
    }
    protected void Attack(PlayerNetwork player, bool air = false)
    {
        if ((player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Light || player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Medium
        || player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Heavy))
        {
            player.pushbackDuration = 0;
            player.attackInput = player.inputBuffer.CurrentTrigger().inputEnum;
            player.isCrouch = false;
            player.isAir = air;
            if ((player.direction.y < 0) || player.inputBuffer.RecentDownInput())
                player.isCrouch = true;
            if (player.isAir)
                player.isCrouch = false;
            AttackSO attack = PlayerComboSystem.GetComboAttack(player.playerStats, player.attackInput, new Vector2(player.direction.x * player.flip, player.direction.y), player.isAir);
            if (attack != null)
            {
                player.attackNetwork = SetAttack(player.attackInput, attack);
                EnterState(player, "Attack");
            }
        }
    }
    protected void Arcana(PlayerNetwork player, bool air = false, bool skipCheck = false)
    {
        if (player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Special || skipCheck)
        {
            player.attackInput = player.inputBuffer.CurrentTrigger().inputEnum;
            player.isAir = air;
            player.isCrouch = false;
            if ((player.direction.y < 0) || player.inputBuffer.RecentDownInput())
                player.isCrouch = true;
            //bool frenzied = player.inputBuffer.RecentTrigger(InputEnum.Throw);
            bool frenzied = true;
            if (player.arcanaGauge < PlayerStatsSO.ARCANA_MULTIPLIER)
                frenzied = false;
            ArcanaSO attack = PlayerComboSystem.GetArcana(player.playerStats, new Vector2(player.direction.x * player.flip, player.direction.y), player.isAir, frenzied);
            if (attack != null)
            {
                player.canChainAttack = false;
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
            travelDistance = new DemonVector2((DemonFloat)attack.travelDistance.x, (DemonFloat)attack.travelDistance.y),
            name = attack.name,
            moveName = attack.moveName,
            attackSound = attack.attackSound,
            hurtEffect = attack.hurtEffect,
            knockbackForce = (DemonFloat)attack.knockbackForce.x,
            moveMaterial = attack.moveMaterial,
            knockbackDuration = attack.knockbackDuration,
            hitstop = attack.hitstop,
            impactSound = attack.impactSound,
            hitStun = attack.hitStun,
            blockStun = attack.blockStun,
            knockbackArc = attack.knockbackArc,
            jumpCancelable = attack.jumpCancelable,
            softKnockdown = attack.causesSoftKnockdown,
            hardKnockdown = attack.causesKnockdown,
            projectilePosition = new DemonVector2((DemonFloat)attack.hitEffectPosition.x, (DemonFloat)attack.hitEffectPosition.y),
            comboTimerStarter = input == InputEnum.Heavy ? ComboTimerStarterEnum.Red : ComboTimerStarterEnum.Yellow,
            attackType = attack.attackTypeEnum,
            superArmor = attack.superArmor,
            projectileSpeed = (DemonFloat)attack.projectileSpeed,
            projectileDestroyOnHit = attack.projectileDestroyOnHit,
            projectilePriority = attack.projectilePriority,
            teleport = attack.teleport,
            teleportPosition = new DemonVector2((DemonFloat)attack.teleportPosition.x, (DemonFloat)attack.teleportPosition.y),
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
            travelDistance = new DemonVector2((DemonFloat)attack.travelDistance.x, (DemonFloat)attack.travelDistance.y),
            name = attack.name,
            moveName = attack.moveName,
            attackSound = attack.attackSound,
            hurtEffect = attack.hurtEffect,
            moveMaterial = attack.moveMaterial,
            knockbackForce = (DemonFloat)attack.knockbackForce.x,
            knockbackDuration = attack.knockbackDuration,
            hitstop = attack.hitstop,
            impactSound = attack.impactSound,
            hitStun = attack.hitStun,
            blockStun = attack.blockStun,
            knockbackArc = attack.knockbackArc,
            jumpCancelable = attack.jumpCancelable,
            softKnockdown = attack.causesSoftKnockdown,
            hardKnockdown = attack.causesKnockdown,
            projectilePosition = new DemonVector2((DemonFloat)attack.hitEffectPosition.x, (DemonFloat)attack.hitEffectPosition.y),
            comboTimerStarter = input == InputEnum.Heavy ? ComboTimerStarterEnum.Red : ComboTimerStarterEnum.Yellow,
            attackType = attack.attackTypeEnum,
            superArmor = attack.superArmor,
        };
        if (attack.cameraShaker != null)
        {
            attackNetwork.cameraShakerNetwork = new CameraShakerNetwork() { intensity = attack.cameraShaker.intensity, timer = attack.cameraShaker.timer };
        }
        return attackNetwork;
    }
    public void ResetPlayer(PlayerNetwork player)
    {
        player.inputBuffer.AddTrigger(new InputItemNetwork()
        {
            inputEnum = InputEnum.Neutral,
            frame = GameSimulation.FramesStatic,
            time = GameSimulation.FramesStatic,
            sequence = true,
            pressed = true
        });
        player.dashDirection = 0;
        player.healthRecoverable = 10000;
        player.health = 10000;
        player.shadowGauge = GameSimulation.maxShadowGauge;
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
        DemonFloat calculatedDamage = (DemonFloat)damage / (DemonFloat)defense;
        if (player.combo > 1)
        {
            DemonFloat damageScale = (DemonFloat)1;
            for (int i = 0; i < player.combo; i++)
            {
                damageScale *= (DemonFloat)0.96;
            }
            calculatedDamage *= damageScale;
        }
        int calculatedIntDamage = (int)calculatedDamage;
        SetResultAttack(player, calculatedIntDamage, player.attackHurtNetwork);
        return calculatedIntDamage;
    }
    protected int CalculateRecoverableDamage(PlayerNetwork player, int damage, float defense)
    {
        DemonFloat calculatedDamage = ((DemonFloat)damage / (DemonFloat)defense) - 120;
        if (player.combo > 1)
        {
            DemonFloat damageScale = (DemonFloat)1;
            for (int i = 0; i < player.combo; i++)
            {
                damageScale *= (DemonFloat)0.97;
            }
            calculatedDamage *= damageScale;
        }
        int calculatedIntDamage = (int)calculatedDamage;
        return calculatedIntDamage;
    }

    protected void UpdateFramedata(PlayerNetwork player)
    {
        if (SceneSettings.IsTrainingMode)
            player.framedataEnum = player.player.PlayerAnimator.GetFramedata(player.animation, player.animationFrames);
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
        CameraShake.Instance.ZoomDefault(0.05f);
        player.combo++;
        player.health -= CalculateDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.healthRecoverable -= CalculateRecoverableDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.player.PlayerUI.Damaged();
        player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
        player.player.OtherPlayerUI.IncreaseCombo(player.combo);
        if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
            player.position = new DemonVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
        else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
            player.position = new DemonVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
        player.SetParticle(player.attackHurtNetwork.hurtEffect, new DemonVector2(player.position.x, player.position.y));
        GameSimulation.Hitstop = 10;
        HitstopFully(player);
        HitstopFully(player.otherPlayer);
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
                player.shadowGauge = GameSimulation.maxShadowGauge;
            }
        }
    }

    public void EnterState(PlayerNetwork player, string name, bool skipEnter = false)
    {
        if (!player.gotHit || name.Contains("Hurt") || name.Contains("Airborne") || name.Contains("Grabbed") || name.Contains("Block") || name.Contains("Knockback"))
        {
            player.player.PlayerAnimator.NormalMaterial();
            player.framedataEnum = Demonics.FramedataTypesEnum.None;
            player.enter = skipEnter;
            player.state = name;
            StateSimulation.SetState(player);
            player.CurrentState.UpdateLogic(player);
        }
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
    public bool AIHurt(PlayerNetwork player)
    {
        if (SceneSettings.IsTrainingMode && player.isAi)
        {
            if (TrainingSettings.OnHit)
            {
                if ((DemonFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
                {
                    player.isAir = true;
                }
                AttackSO attack = PlayerComboSystem.GetComboAttack(player.playerStats, InputEnum.Light, Vector2.zero, player.isAir);
                player.attackNetwork = SetAttack(InputEnum.Light, attack);
                EnterState(player, "Attack");
                return true;
            }
        }
        return false;
    }
    public bool AIBlocking(PlayerNetwork player, AttackTypeEnum attackType)
    {
        if (SceneSettings.IsTrainingMode && player.isAi)
        {
            bool canBlock = false;
            if (TrainingSettings.Block == BlockType.BlockAlways)
                canBlock = true;
            if (TrainingSettings.Block == BlockType.BlockCount && TrainingSettings.BlockCountCurrent > 0)
            {
                TrainingSettings.BlockCountCurrent--;
                canBlock = true;
            }
            if (canBlock)
            {
                if (attackType == AttackTypeEnum.Low)
                    player.direction.y = -1;
                else
                    player.direction.y = 0;
                return true;
            }
        }
        TrainingSettings.BlockCountCurrent = TrainingSettings.BlockCount;
        return false;
    }
    protected void ResetCombo(PlayerNetwork player)
    {
        player.otherPlayer.shadow.usedInCombo = false;
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
        if (player.invincible && player.health <= 0)
            return false;
        if (player.otherPlayer.shadow.projectile.active)
        {
            if (DemonicsCollider.Colliding(player.otherPlayer.shadow.projectile.hitbox, player.hurtbox) && !player.otherPlayer.shadow.projectile.hitbox.enter)
            {
                player.attackHurtNetwork = player.otherPlayer.shadow.projectile.attackNetwork;
                GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
                player.hitstop = true;
                player.otherPlayer.shadow.projectile.hitstop = true;
                player.otherPlayer.shadow.projectile.hitbox.enter = true;
                player.otherPlayer.shadow.projectile.hitstop = true;
                player.hurtPosition = new DemonVector2(player.otherPlayer.shadow.projectile.hitbox.position.x + ((player.otherPlayer.shadow.projectile.hitbox.size.x / 2) * -player.flip), player.otherPlayer.shadow.projectile.hitbox.position.y);
                if (player.otherPlayer.shadow.projectile.destroyOnHit)
                {
                    player.otherPlayer.shadow.projectile.hitbox.enter = false;
                    player.otherPlayer.shadow.projectile.hitstop = false;
                    player.otherPlayer.shadow.projectile.animationFrames = 0;
                    player.otherPlayer.shadow.projectile.active = false;
                    player.otherPlayer.shadow.projectile.hitbox.active = false;
                }
                player.gotHit = true;
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
                    player.hurtPosition = new DemonVector2(player.position.x, player.position.y + (player.pushbox.size.y / 2));
                    if (player.otherPlayer.projectiles[i].destroyOnHit)
                    {
                        player.otherPlayer.projectiles[i].hitbox.enter = false;
                        player.otherPlayer.projectiles[i].hitstop = false;
                        player.otherPlayer.projectiles[i].animationFrames = 0;
                        player.otherPlayer.projectiles[i].active = false;
                        player.otherPlayer.projectiles[i].hitbox.active = false;
                    }
                    player.gotHit = true;
                    return true;
                }
            }
        }
        if (DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox) && !player.otherPlayer.hitbox.enter)
        {
            if (player.otherPlayer.attackNetwork.attackType == AttackTypeEnum.Throw && player.stunFrames == 0 && (DemonFloat)player.position.y > DemonicsPhysics.GROUND_POINT)
                return false;
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
            GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
            player.hitstop = true;
            player.otherPlayer.hitbox.enter = true;
            player.otherPlayer.hitstop = true;
            if (player.otherPlayer.isAir)
            {
                player.hurtPosition = new DemonVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y - (0.1f * -player.flip));
            }
            else
            {
                player.hurtPosition = new DemonVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y);
            }
            player.gotHit = true;
            return true;
        }
        return false;
    }

    protected void SuperArmorHurt(PlayerNetwork player)
    {
        player.attackNetwork.superArmor -= 1;
        player.sound = "SuperArmor";
        if (player.attackHurtNetwork.cameraShakerNetwork.timer > 0)
        {
            CameraShake.Instance.Shake(player.attackHurtNetwork.cameraShakerNetwork);
        }
        player.health -= CalculateDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.healthRecoverable -= CalculateRecoverableDamage(player, player.attackHurtNetwork.damage, player.playerStats.Defense);
        player.otherPlayer.canChainAttack = true;
        player.canChainAttack = false;
        if (GameSimulation.Hitstop <= 0)
        {
            GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
        }
        player.SetParticle(player.attackHurtNetwork.hurtEffect, player.hurtPosition);
        player.player.PlayerAnimator.SpriteSuperArmorEffect();
        player.player.PlayerUI.Damaged();
        player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
    }

    protected void Shadow(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed && player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Assist)
        {
            if (!player.shadow.isOnScreen && player.shadowGauge >= GameSimulation.maxShadowGauge / 2)
            {
                player.sound = "Shadow";
                player.shadow.usedInCombo = true;
                player.shadow.projectile.attackNetwork = SetAttack(player.attackInput, player.shadow.attack);
                player.shadow.projectile.flip = player.flip == 1 ? false : true;
                player.shadow.projectile.fire = true;
                player.shadow.animationFrames = 0;
                player.shadow.isOnScreen = true;
                player.shadow.flip = player.flip;
                player.shadow.position = new DemonVector2(player.position.x + (player.shadow.spawnPoint.x * player.flip), player.position.y + player.shadow.spawnPoint.y);
                player.shadowGauge -= GameSimulation.maxShadowGauge / 2;
            }
        }
    }
    protected void ToShadowbreakState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed && player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Assist)
        {
            if (player.shadowGauge == GameSimulation.maxShadowGauge)
            {
                ResetCombo(player);
                player.player.PlayerUI.UpdateHealthDamaged(player.healthRecoverable);
                player.velocity = DemonVector2.Zero;
                player.shadowGauge -= 2000;
                EnterState(player, "Shadowbreak");
            }
        }
    }
};