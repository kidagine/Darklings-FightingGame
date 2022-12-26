using UnityEngine;


public class AttackState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        AttackSO attack = PlayerComboSystem.GetComboAttack(player.playerStats, player.attackInput, player.isCrouch, player.isAir);
        if (!player.enter)
        {
            SetTopPriority(player);
            player.canChainAttack = false;
            player.inputBuffer.inputItems[0].frame = 0;
            player.enter = true;
            player.animation = attack.name;
            player.sound = attack.attackSound;
            player.animationFrames = 0;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
        }
        if (!player.isAir)
        {
            player.velocity = new Vector2(attack.travelDistance.x * player.flip, attack.travelDistance.y);
        }
        else
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y - player.gravity);
        }
        if (GameSimulation.Hitstop <= 0)
        {
            player.animationFrames++;
            player.attackFrames--;
            if (player.canChainAttack)
            {
                if (player.inputBuffer.inputItems[0].frame + 20 >= DemonicsWorld.Frame)
                {
                    player.attackInput = player.inputBuffer.inputItems[0].inputEnum;
                    player.isCrouch = false;
                    player.isAir = false;
                    if (player.direction.y < 0)
                    {
                        player.isCrouch = true;
                    }
                    player.enter = false;
                    player.state = "Attack";
                }
            }
        }
        ToIdleState(player);
        ToIdleFallState(player);
    }
    private void ToIdleFallState(PlayerNetwork player)
    {
        if (player.isAir && (DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
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
            if (player.isAir)
            {
                player.isCrouch = false;
                player.isAir = false;
                player.attackInput = InputEnum.Direction;
                player.state = "Fall";
            }
            else
            {
                if (player.direction.y < 0)
                {
                    player.isCrouch = false;
                    player.isAir = false;
                    player.attackInput = InputEnum.Direction;
                    player.state = "Crouch";
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
}