using UnityEngine;

public class BlockAirState : BlockParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "BlockAir";
        player.animationFrames++;
        ToFallState(player);
    }
    protected override void AfterHitstop(PlayerNetwork player)
    {
        base.AfterHitstop(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.knockbackArc > 0 && player.knockback <= 1)
            return;
        if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT)
        {
            player.player.StopShakeCoroutine();
            CheckTrainingComboEnd(player);
            if (player.stunFrames <= 0)
                EnterState(player, "Idle");
            else
            {
                player.stunFrames = player.attackHurtNetwork.blockStun;
                player.velocity = DemonicsVector2.Zero;
                player.animationFrames = 0;
                EnterState(player, "Block");
            }
        }
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
            EnterState(player, "Fall");
    }
    protected override void OnEnter(PlayerNetwork player)
    {
        base.OnEnter(player);
    }
}