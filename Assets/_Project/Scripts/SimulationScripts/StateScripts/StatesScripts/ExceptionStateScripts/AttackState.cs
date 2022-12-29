using UnityEngine;


public class AttackState : State
{
    public static Vector2 start;
    private static Vector2 end;
    private static int knockbackFrame;
    private static bool knock;
    private static bool b;
    private static bool opponentInCorner;
    public override void UpdateLogic(PlayerNetwork player)
    {
        AttackSO attack = PlayerComboSystem.GetComboAttack(player.playerStats, player.attackInput, player.isCrouch, player.isAir);
        if (!player.enter)
        {
            b = false;
            SetTopPriority(player);
            player.animationFrames = 0;
            player.canChainAttack = false;
            player.inputBuffer.inputItems[0].frame = 0;
            player.enter = true;
            player.animation = attack.name;
            player.sound = attack.attackSound;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            opponentInCorner = false;
            if (DemonicsPhysics.IsInCorner(player.otherPlayer))
            {
                opponentInCorner = true;
            }
        }
        if (!player.isAir)
        {
            player.velocity = new Vector2(attack.travelDistance.x * player.flip, attack.travelDistance.y);
        }
        else
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y - (float)DemonicsPhysics.GRAVITY);
        }

        if (GameSimulation.Hitstop <= 0)
        {
            player.animationFrames++;
            player.attackFrames--;
            if (player.canChainAttack)
            {
                if (!b)
                {
                    b = true;
                    knockbackFrame = 0;
                    start = player.position;
                    end = new Vector2(player.position.x + (attack.knockbackForce.x * -player.flip), (float)DemonicsPhysics.GROUND_POINT - 0.5f);
                }
                knock = true;
                if ((!(player.attackInput == InputEnum.Medium && player.isCrouch || player.attackInput == InputEnum.Heavy)) || player.inputBuffer.inputItems[0].inputEnum == InputEnum.Special)
                {
                    if (player.inputBuffer.inputItems[0].frame + 20 >= DemonicsWorld.Frame)
                    {
                        player.attackInput = player.inputBuffer.inputItems[0].inputEnum;
                        player.isCrouch = false;
                        if (player.direction.y < 0)
                        {
                            player.isCrouch = true;
                        }
                        player.enter = false;
                        if (player.attackInput == InputEnum.Special)
                        {
                            player.state = "Arcana";
                        }
                        else
                        {
                            player.state = "Attack";
                        }
                    }
                }
            }
            if (knock)
            {
                if (opponentInCorner && !player.isAir)
                {
                    if (attack.knockbackDuration > 0)
                    {
                        if (knockbackFrame <= attack.knockbackDuration)
                        {
                            float ratio = (float)knockbackFrame / (float)attack.knockbackDuration;
                            float distance = end.x - start.x;
                            float nextX = Mathf.Lerp(start.x, end.x, ratio);
                            float baseY = Mathf.Lerp(start.y, end.y, (nextX - start.x) / distance);
                            float arc = attack.knockbackArc * (nextX - start.x) * (nextX - end.x) / ((-0.25f) * distance * distance);
                            Vector2 nextPosition = new Vector2(nextX, baseY + arc);
                            nextPosition = new Vector2(nextX, player.position.y);
                            player.position = nextPosition;
                            knockbackFrame++;
                        }
                    }
                }
            }
        }
        ToJumpForwardState(player);
        ToJumpState(player);
        ToIdleState(player);
        ToIdleFallState(player);
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (player.attackInput == InputEnum.Heavy && player.isCrouch)
        {
            if (player.direction.y > 0)
            {
                player.isCrouch = false;
                player.isAir = false;
                GameSimulation.Hitstop = 0;
                player.state = "Jump";
            }
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.attackInput == InputEnum.Heavy && player.isCrouch)
        {
            if (player.direction.y > 0 && player.direction.x != 0)
            {
                player.isCrouch = false;
                player.isAir = false;
                GameSimulation.Hitstop = 0;
                player.state = "JumpForward";
            }
        }
    }
    private void ToIdleFallState(PlayerNetwork player)
    {
        if (player.isAir && (DemonicsFloat)player.position.y <= DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
        {
            knock = false;
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
            knock = false;
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