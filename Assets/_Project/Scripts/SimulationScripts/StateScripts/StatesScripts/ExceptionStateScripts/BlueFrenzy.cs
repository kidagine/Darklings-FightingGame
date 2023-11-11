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
            UpdateFramedata(player);
            return;
        }
        if (!player.hitstop)
        {
            player.animationFrames++;
            player.attackFrames--;
            if (player.pushbackDuration > 0 && player.knockback <= player.pushbackDuration)
            {
                DemonFloat ratio = (DemonFloat)player.knockback / (DemonFloat)player.pushbackDuration;
                DemonFloat nextX = DemonFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
                DemonVector2 nextPosition = new DemonVector2(nextX, player.position.y);
                player.position = nextPosition;
                player.knockback++;
                if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
                    player.position = new DemonVector2(DemonicsPhysics.WALL_RIGHT_POINT, player.position.y);
                else if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
                    player.position = new DemonVector2(DemonicsPhysics.WALL_LEFT_POINT, player.position.y);
            }
        }
        UpdateFramedata(player);
        player.velocity = DemonVector2.Zero;

        bool isParrying = player.player.PlayerAnimator.GetParrying(player.animation, player.animationFrames);
        if (isParrying)
            player.player.PlayerAnimator.ParryMaterial();
        else
            player.player.PlayerAnimator.NormalMaterial();

        if (IsColliding(player))
        {
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
            {
                EnterState(player, "Grabbed");
                return;
            }
            if (isParrying)
                Parry(player);
            else
                ToHurtState(player);
        }
        ToIdleState(player);
        ToParryState(player, isParrying);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
            EnterState(player, "Idle");
    }
    private void ToParryState(PlayerNetwork player, bool isParrying)
    {
        if (player.inputBuffer.CurrentTrigger().pressed && player.inputBuffer.CurrentTrigger().inputEnum == InputEnum.Parry)
            if (isParrying)
                EnterState(player, "BlueFrenzy");
    }

    private void Parry(PlayerNetwork player)
    {
        player.animationFrames = 6;
        int parryDistance = 37;
        player.sound = "Parry";
        DemonVector2 hurtEffectPosition = new DemonVector2(player.position.x + (20 * player.flip), player.position.y + 25);
        player.SetParticle("Parry", hurtEffectPosition);
        player.otherPlayer.canChainAttack = true;
        GameSimulation.Hitstop = 13;
        CameraShake.Instance.Shake(new CameraShakerNetwork() { intensity = 10, timer = 0.12f });
        if (DemonicsPhysics.IsInCorner(player.otherPlayer))
        {
            player.knockback = 0;
            player.pushbackStart = player.position;
            player.pushbackEnd = new DemonVector2(player.position.x + (parryDistance * -player.flip), DemonicsPhysics.GROUND_POINT);
            player.pushbackDuration = 10;
        }
        else
        {
            player.otherPlayer.knockback = 0;
            player.otherPlayer.pushbackStart = player.otherPlayer.position;
            player.otherPlayer.pushbackEnd = new DemonVector2(player.otherPlayer.position.x + (parryDistance * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
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
            player.otherPlayer.pushbackEnd = new DemonVector2(player.otherPlayer.position.x + (player.attackHurtNetwork.knockbackForce * -player.otherPlayer.flip), DemonicsPhysics.GROUND_POINT);
            player.otherPlayer.pushbackDuration = player.attackHurtNetwork.knockbackDuration;
        }
        if (player.attackHurtNetwork.hardKnockdown)
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