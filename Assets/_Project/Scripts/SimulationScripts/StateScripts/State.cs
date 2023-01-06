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
    public virtual void ToArcanaState(PlayerNetwork player) { }
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
    protected void Attack(PlayerNetwork player, bool air = false)
    {
        player.attackInput = player.inputBuffer.inputItems[0].inputEnum;
        player.start = false;
        player.isAir = air;
        player.isCrouch = false;
        if (player.direction.y < 0)
        {
            player.isCrouch = true;
        }
        AttackSO atk = PlayerComboSystem.GetComboAttack(player.playerStats, player.attackInput, player.isCrouch, player.isAir);
        player.attackNetwork = new AttackNetwork()
        {
            damage = atk.damage,
            travelDistance = (DemonicsFloat)atk.travelDistance.x,
            name = atk.name,
            attackSound = atk.attackSound,
            hurtEffect = atk.hurtEffect,
            knockbackForce = (DemonicsFloat)atk.knockbackForce.x,
            knockbackDuration = atk.knockbackDuration,
            hitstop = atk.hitstop,
            impactSound = atk.impactSound,
            hitStun = atk.hitStun,
            comboTimerStarter = player.attackInput == InputEnum.Heavy ? ComboTimerStarterEnum.Red : ComboTimerStarterEnum.Yellow
        };
        player.enter = false;
        player.state = "Attack";
    }
};