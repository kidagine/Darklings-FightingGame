using UnityEngine;

public class PreJumpState : GroundParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.enter = true;
            player.animation = "Crouch";
            player.animationFrames = 0;
            return;
        }
        player.position = new DemonVector2(player.position.x, DemonicsPhysics.GROUND_POINT);
        player.animationFrames++;
        player.velocity = DemonVector2.Zero;
        CheckFlip(player);
        ToJumpState(player);
        ToHurtState(player);
    }

    private void ToJumpState(PlayerNetwork player)
    {
        if (player.animationFrames > 3)
        {
            if (player.jumpDirection == 0)
                EnterState(player, "Jump");
            else
                EnterState(player, "JumpForward");
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (player.attackHurtNetwork.attackType != AttackTypeEnum.Throw)
        {
            if (IsColliding(player))
            {
                if (player.attackHurtNetwork.moveName == "Shadowbreak")
                {
                    EnterState(player, "Knockback");
                    return;
                }
                if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.softKnockdown && player.position.y > DemonicsPhysics.GROUND_POINT)
                    EnterState(player, "Airborne");
                else
                    EnterState(player, "HurtAir");
            }
        }
    }
}