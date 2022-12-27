public class State
{
    public float Gravity;
    public virtual void Enter(PlayerNetwork player)
    {
        player.animationFrames = 0;
    }
    public virtual void UpdateLogic(PlayerNetwork player) { }
    public virtual void Exit() { }
    public virtual bool ToAttackState() { return false; }
    public virtual bool ToDashState(PlayerNetwork player) { return false; }
    public virtual bool ToAttackState(PlayerNetwork player) { return false; }
    public virtual bool ToArcanaState(PlayerNetwork player) { return false; }
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