using UnityEngine;


public class ArcanaState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        AttackSO attack = PlayerComboSystem.GetArcana(player.playerStats, player.isCrouch, player.isAir);
        if (!player.enter)
        {
            player.enter = true;
            player.canChainAttack = false;
            GameplayManager.Instance.PlayerOne.CurrentAttack = attack;
            player.animation = attack.name;
            player.sound = attack.attackSound;
            player.animationFrames = 0;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            player.velocity = new Vector2(attack.travelDistance.x * player.flip, attack.travelDistance.y);
        }
        ToIdleState(player);
        if (GameSimulation.Hitstop <= 0)
        {
            if (attack.travelDistance.y > 0)
            {
                player.velocity = new Vector2(player.velocity.x, player.velocity.y - player.gravity);
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