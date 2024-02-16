
using UnityEngine;

public class GroundParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.canDoubleJump = true;
        player.hasJumped = false;
        player.canJump = true;
        if (ToBlueFrenzyState(player))
            return;
        if (RedFrenzy(player))
            return;
        ToArcanaState(player);
        ToGrabState(player);
        Shadow(player);
        if (ToAttackState(player))
            return;
        ToHurtState(player);
    }

    private bool ToBlueFrenzyState(PlayerNetwork player)
    {
        if (player.inputBuffer.GetBlueFrenzy())
        {
            EnterState(player, "BlueFrenzy");
            return true;
        }
        return false;
    }
    public bool ToAttackState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed)
        {
            Attack(player);
            return true;
        }
        return false;
    }
    public void ToArcanaState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed)
            Arcana(player);
    }

    private void ToGrabState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed && player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Throw)
        {
            AttackSO attack = PlayerComboSystem.GetThrow(player.playerStats);
            player.attackNetwork = SetAttack(player.attackInput, attack);
            EnterState(player, "Grab");
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                EnterState(player, "Grabbed");
                return;
            }
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                EnterState(player, "Grabbed");
                return;
            }
            if (DemonicsPhysics.IsInCorner(player))
            {
                player.otherPlayer.knockback = 0;
                player.otherPlayer.pushbackStart = player.otherPlayer.position;
                player.otherPlayer.pushbackEnd = new DemonVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
                player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
            }
            if (IsBlocking(player))
            {
                if (player.direction.y < 0)
                    EnterState(player, "BlockLow");
                else
                    EnterState(player, "Block");
            }
            else
            {
                if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.moveName == "Shadowbreak")
                    EnterState(player, "Airborne");
                else
                {
                    if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                        EnterState(player, "Hurt");
                    else
                        EnterState(player, "HurtAir");
                }
            }
        }
    }
}