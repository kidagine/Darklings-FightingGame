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
        if (GameSimulation.Hitstop <= 0)
        {
            player.animationFrames++;
            player.attackFrames--;
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
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            player.enter = false;
            player.state = "Idle";
        }
    }

    private void Parry(PlayerNetwork player)
    {
        player.sound = "Parry";
        DemonicsVector2 hurtEffectPosition = new DemonicsVector2(player.position.x + (20 * player.flip), player.otherPlayer.hitbox.position.y);
        player.SetEffect("Parry", hurtEffectPosition);
        player.otherPlayer.canChainAttack = true;
        GameSimulation.Hitstop = 10;
        player.otherPlayer.knockback = 0;
        player.otherPlayer.pushbackStart = player.otherPlayer.position;
        player.otherPlayer.pushbackEnd = new DemonicsVector2(player.otherPlayer.position.x + (24 * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
        player.otherPlayer.pushbackDuration = 10;
        player.health = player.healthRecoverable;
    }
    private void ToHurtState(PlayerNetwork player)
    {
        player.enter = false;
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
                player.state = "BlockLow";
            }
            else
            {
                player.state = "Block";
            }
        }
        else
        {
            if (player.attackHurtNetwork.hardKnockdown)
            {
                player.state = "Airborne";
            }
            else
            {
                if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
                {
                    player.state = "Hurt";
                }
                else
                {
                    player.state = "HurtAir";
                }
            }
        }
    }
}