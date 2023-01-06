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
};