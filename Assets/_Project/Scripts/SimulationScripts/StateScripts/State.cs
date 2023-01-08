using System;

[Serializable]
public class State
{
    public virtual void UpdateLogic(PlayerNetwork player) { }
    public virtual void Exit() { }
    public virtual bool ToRedFrenzyState(PlayerNetwork player) { return false; }
    public virtual bool ToBlueFrenzyState(PlayerNetwork player) { return false; }
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
        player.attackNetwork = new AttackNetwork()
        {
            damage = attack.damage,
            travelDistance = new DemonicsVector2((DemonicsFloat)attack.travelDistance.x, (DemonicsFloat)attack.travelDistance.y),
            name = attack.name,
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
            comboTimerStarter = player.attackInput == InputEnum.Heavy ? ComboTimerStarterEnum.Red : ComboTimerStarterEnum.Yellow,
            attackType = attack.attackTypeEnum
        };
        if (attack.cameraShaker != null)
        {
            player.attackNetwork.cameraShakerNetwork = new CameraShakerNetwork() { intensity = attack.cameraShaker.intensity, timer = attack.cameraShaker.timer };
        }
        player.enter = false;
        player.state = "Attack";
    }
    protected void Arcana(PlayerNetwork player, bool air = false)
    {
        if (player.arcana >= PlayerStatsSO.ARCANA_MULTIPLIER)
        {
            player.attackInput = player.inputBuffer.inputItems[0].inputEnum;
            player.arcanaPress = false;
            player.isAir = air;
            player.isCrouch = false;
            if (player.inputBuffer.inputItems[0].inputDirection.y < 0)
            {
                player.isCrouch = true;
            }
            AttackSO attack = PlayerComboSystem.GetComboAttack(player.playerStats, player.attackInput, player.isCrouch, player.isAir);
            player.attackNetwork = new AttackNetwork()
            {
                damage = attack.damage,
                travelDistance = new DemonicsVector2((DemonicsFloat)attack.travelDistance.x, (DemonicsFloat)attack.travelDistance.y),
                name = attack.name,
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
                comboTimerStarter = player.attackInput == InputEnum.Heavy ? ComboTimerStarterEnum.Red : ComboTimerStarterEnum.Yellow,
                attackType = attack.attackTypeEnum
            };
            if (attack.cameraShaker != null)
            {
                player.attackNetwork.cameraShakerNetwork = new CameraShakerNetwork() { intensity = attack.cameraShaker.intensity, timer = attack.cameraShaker.timer };
            }
            player.enter = false;
            player.state = "Arcana";
        }
    }
    public void ResetPlayer(PlayerNetwork player)
    {
        player.healthRecoverable = 10000;
        player.health = 10000;
        player.player.PlayerUI.CheckDemonLimit(player.health);
        player.enter = false;
    }
};