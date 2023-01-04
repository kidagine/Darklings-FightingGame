using UnityEngine;


public class ArcanaState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.arcana -= PlayerStatsSO.ARCANA_MULTIPLIER;
            player.attack = PlayerComboSystem.GetComboAttack(player.playerStats, InputEnum.Special, player.isCrouch, player.isAir);
            player.enter = true;
            player.canChainAttack = false;
            //  GameplayManager.Instance.PlayerOne.CurrentAttack = attack;
            player.animation = player.attack.name;
            player.sound = player.attack.attackSound;
            player.animationFrames = 0;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            player.velocity = new DemonicsVector2(player.attack.travelDistance.x * (DemonicsFloat)player.flip, (DemonicsFloat)player.attack.travelDistance.y);
        }
        ToIdleState(player);
        if (GameSimulation.Hitstop <= 0)
        {
            if (player.attack.travelDistance.y > 0)
            {
                player.velocity = new DemonicsVector2(player.velocity.x, player.velocity.y - (float)DemonicsPhysics.GRAVITY);
                ToIdleFallState(player);
            }
            player.animationFrames++;
            player.attackFrames--;
        }
    }
    private void ToIdleFallState(PlayerNetwork player)
    {
        if ((DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
        {
            player.isCrouch = false;
            player.isAir = false;
            player.attackInput = InputEnum.Direction;
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            player.enter = false;
            if (player.isAir)
            {
                player.isCrouch = false;
                player.isAir = false;
                player.attackInput = InputEnum.Direction;
                player.state = "Fall";
            }
            else
            {
                player.isCrouch = false;
                player.isAir = false;
                player.attackInput = InputEnum.Direction;
                player.state = "Idle";
            }
        }
    }
}