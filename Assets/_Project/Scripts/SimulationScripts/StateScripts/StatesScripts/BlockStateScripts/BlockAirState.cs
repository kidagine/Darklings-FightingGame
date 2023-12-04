using UnityEngine;

public class BlockAirState : BlockParentState
{
    private readonly int _extraLandBlockStun = 4;

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
        if ((DemonFloat)player.position.y <= DemonicsPhysics.GROUND_POINT)
        {
            player.player.StopShakeCoroutine();
            CheckTrainingComboEnd(player);
            if (player.stunFrames <= 0)
            {
                EnterState(player, "Idle");
            }
            else
            {
                player.stunFrames = player.attackHurtNetwork.blockStun + _extraLandBlockStun;
                player.velocity = DemonVector2.Zero;
                player.animationFrames = 0;
                EnterState(player, "Block");
            }
        }
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            EnterState(player, "Fall");
        }
    }
    protected override void OnEnter(PlayerNetwork player)
    {
        base.OnEnter(player);
    }
}