using UnityEngine;


public class AttackState : State
{
    private AttackSO _attack;
    public static Vector2 start;
    private static Vector2 end;
    private static int knockbackFrame;
    private static bool knock;
    private static bool b;
    private static bool opponentInCorner;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            _attack = PlayerComboSystem.GetComboAttack(player.playerStats, player.attackInput, player.isCrouch, player.isAir);
            b = false;
            SetTopPriority(player);
            player.animationFrames = 0;
            player.canChainAttack = false;
            player.inputBuffer.inputItems[0].frame = 0;
            player.enter = true;
            player.animation = _attack.name;
            player.sound = _attack.attackSound;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
            opponentInCorner = false;
            if (DemonicsPhysics.IsInCorner(player.otherPlayer))
            {
                opponentInCorner = true;
            }
        }
        if (!player.isAir)
        {
            player.velocity = new Vector2(_attack.travelDistance.x * player.flip, _attack.travelDistance.y);
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
                    end = new Vector2(player.position.x + (_attack.knockbackForce.x * -player.flip), (float)DemonicsPhysics.GROUND_POINT - 0.5f);
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
                    if (_attack.knockbackDuration > 0)
                    {
                        if (knockbackFrame <= _attack.knockbackDuration)
                        {
                            float ratio = (float)knockbackFrame / (float)_attack.knockbackDuration;
                            float distance = end.x - start.x;
                            float nextX = Mathf.Lerp(start.x, end.x, ratio);
                            float baseY = Mathf.Lerp(start.y, end.y, (nextX - start.x) / distance);
                            float arc = _attack.knockbackArc * (nextX - start.x) * (nextX - end.x) / ((-0.25f) * distance * distance);
                            Vector2 nextPosition = new Vector2(nextX, baseY + arc);
                            nextPosition = new Vector2(nextX, player.position.y);
                            player.position = nextPosition;
                            knockbackFrame++;
                        }
                    }
                }
            }
        }
        ToJumpState(player);
        ToJumpForwardState(player);
        ToIdleState(player);
        ToIdleFallState(player);
    }
    private void ToJumpState(PlayerNetwork player)
    {
        if (_attack.jumpCancelable)
        {
            if (player.direction.y > 0)
            {
                player.enter = false;
                player.isCrouch = false;
                player.isAir = false;
                GameSimulation.Hitstop = 0;
                player.state = "Jump";
            }
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (_attack.jumpCancelable)
        {
            if (player.direction.y > 0 && player.direction.x != 0)
            {
                player.dashDirection = (int)player.direction.x;
                player.enter = false;
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
    public override bool ToHurtState(PlayerNetwork player, AttackSO attack)
    {
        player.enter = false;
        if (_attack.hasSuperArmor && !player.player.PlayerAnimator.InRecovery())
        {
            GameSimulation.Hitstop = attack.hitstop;
            player.player.PlayerAnimator.SpriteSuperArmorEffect();
            player.player.SetHealth(player.player.CalculateDamage(attack));
            player.player.StartShakeContact();
            player.player.PlayerUI.Damaged();
            player.player.OtherPlayerUI.IncreaseCombo();
            return false;
        }
        if (attack.causesKnockdown)
        {
            player.state = "Airborne";
        }
        else
        {
            if (attack.knockbackArc == 0 || attack.causesSoftKnockdown)
            {
                player.state = "Hurt";
            }
            else
            {
                player.state = "HurtAir";
            }
        }
        return true;
    }
}