using UnityEngine;

public class BlueFrenzyState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.sound = "ParryStart";
            SetTopPriority(player);
            CheckFlip(player);
            player.enter = true;
            player.animationFrames = 0;
            player.animation = "Parry";
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
        }
        player.velocity = DemonicsVector2.Zero;
        if (!player.hitstop)
        {
            player.animationFrames++;
            player.attackFrames--;
            if (player.pushbackDuration > 0 && player.knockback <= player.pushbackDuration)
            {
                DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.pushbackDuration;
                DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
                DemonicsVector2 nextPosition = new DemonicsVector2(nextX, player.position.y);
                player.position = nextPosition;
                player.knockback++;
                if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
                {
                    player.position = new DemonicsVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
                }
                else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
                {
                    player.position = new DemonicsVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
                }
            }
        }
        bool isParrying = player.player.PlayerAnimator.GetParrying(player.animation, player.animationFrames);
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            if (isParrying)
            {
                Parry(player);
            }
            else
            {
                ToHurtState(player);
            }
        }
        if (isParrying)
        {
            player.health = player.healthRecoverable;
        }
        ToIdleState(player);
        ToParryState(player, isParrying);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            EnterState(player, "Idle");
        }
    }
    private void ToParryState(PlayerNetwork player, bool isParrying)
    {
        if (player.inputBuffer.CurrentInput().pressed && player.inputBuffer.CurrentInput().inputEnum == InputEnum.Parry)
        {
            if (isParrying)
            {
                EnterState(player, "BlueFrenzy");
            }
        }
    }

    private void Parry(PlayerNetwork player)
    {
        player.sound = "Parry";
        DemonicsVector2 hurtEffectPosition = new DemonicsVector2(player.position.x + (20 * player.flip), player.otherPlayer.hitbox.position.y);
        player.SetEffect("Parry", hurtEffectPosition);
        player.otherPlayer.canChainAttack = true;
        GameSimulation.Hitstop = 10;
        if (DemonicsPhysics.IsInCorner(player.otherPlayer))
        {
            player.knockback = 0;
            player.pushbackStart = player.position;
            player.pushbackEnd = new DemonicsVector2(player.position.x + (24 * -player.flip), DemonicsPhysics.GROUND_POINT);
            player.pushbackDuration = 10;
        }
        else
        {
            player.otherPlayer.knockback = 0;
            player.otherPlayer.pushbackStart = player.otherPlayer.position;
            player.otherPlayer.pushbackEnd = new DemonicsVector2(player.otherPlayer.position.x + (24 * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
            player.otherPlayer.pushbackDuration = 10;
        }
        player.health = player.healthRecoverable;
    }
    private void ToHurtState(PlayerNetwork player)
    {
        player.attackHurtNetwork = player.otherPlayer.attackNetwork;
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
            if (player.attackHurtNetwork.hardKnockdown)
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