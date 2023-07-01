
using UnityEngine;

public class GroundParentState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.canDoubleJump = true;
        player.hasJumped = false;
        player.canJump = true;
        ToBlueFrenzyState(player);
        ToRedFrenzyState(player);
        ToGrabState(player);
        ToAttackState(player);
        ToArcanaState(player);
        Shadow(player);
        ToHurtState(player);
    }

    private void ToBlueFrenzyState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed && player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Parry)
        {
            EnterState(player, "BlueFrenzy");
        }
    }
    public void ToAttackState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed)
        {
            Attack(player);
        }
    }
    public void ToArcanaState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed)
        {
            Arcana(player);
        }
    }
    private void ToRedFrenzyState(PlayerNetwork player)
    {
        if (player.inputBuffer.CurrentTrigger().pressed)
        {
            RedFrenzy(player);
        }
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
            if (DemonicsPhysics.IsInCorner(player))
            {
                player.otherPlayer.knockback = 0;
                player.otherPlayer.pushbackStart = player.otherPlayer.position;
                player.otherPlayer.pushbackEnd = new DemonicsVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
                player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
            }
            if (IsBlocking(player))
            {
                if (player.direction.y < 0)
                {
                    EnterState(player, "BlockLow");
                }
                else
                {
                    EnterState(player, "Block");
                }
            }
            else
            {
                if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.moveName == "Shadowbreak")
                {
                    EnterState(player, "Airborne");
                }
                else
                {
                    if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                    {
                        EnterState(player, "Hurt");
                    }
                    else
                    {
                        EnterState(player, "HurtAir");
                    }
                }
            }
        }
    }
}