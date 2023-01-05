using UnityEngine;

public class HurtState : HurtParentState
{
    public static DemonicsVector2 start;
    private static DemonicsVector2 end;
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.animationFrames = 0;
            //player.player.OtherPlayer.StartComboTimer(ComboTimerStarterEnum.Yellow);
            CheckFlip(player);
            player.health -= player.attackHurtNetwork.damage;
            // player.player.SetHealth(200);
            player.player.StartShakeContact();
            player.player.PlayerUI.Damaged();
            player.player.OtherPlayerUI.IncreaseCombo();
            player.enter = true;
            GameSimulation.Hitstop = player.attackHurtNetwork.hitstop;
            player.otherPlayer.canChainAttack = true;
            player.sound = player.attackHurtNetwork.impactSound;
            DemonicsVector2 hurtEffectPosition = DemonicsVector2.Zero;
            if (player.otherPlayer.isAir)
            {
                hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y - (0.1f * -player.flip));
            }
            else
            {
                hurtEffectPosition = new DemonicsVector2(player.otherPlayer.hitbox.position.x + ((player.otherPlayer.hitbox.size.x / 2) * -player.flip) - (0.3f * -player.flip), player.otherPlayer.hitbox.position.y);
            }
            player.SetEffect(player.attackHurtNetwork.hurtEffect, hurtEffectPosition);
            if (player.attackHurtNetwork.cameraShakerNetwork.timer > 0)
            {
                CameraShake.Instance.Shake(player.attackHurtNetwork.cameraShakerNetwork);
            }
            player.stunFrames = 10;
            player.knockback = 0;
            player.attackHurtNetwork.knockbackStart = player.position;
            player.attackHurtNetwork.knockbackEnd = new DemonicsVector2(player.position.x + (player.attackHurtNetwork.knockbackForce * -player.flip), DemonicsPhysics.GROUND_POINT);
        }
        //player.velocity = DemonicsVector2.Zero;
        player.animation = "Hurt";
        if (player.animationFrames < 4)
        {
            player.animationFrames++;
        }
        if (GameSimulation.Hitstop <= 0)
        {
            //player.velocity = new DemonicsVector2((player.attackHurtNetwork.travdelDistance) * (DemonicsFloat)(-player.flip), (DemonicsFloat)0);
            if (!DemonicsPhysics.IsInCorner(player))
            {
                if (player.attackHurtNetwork.knockbackDuration > 0 && player.knockback <= player.attackHurtNetwork.knockbackDuration)
                {
                    DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.attackHurtNetwork.knockbackDuration;
                    DemonicsFloat distance = player.attackHurtNetwork.knockbackEnd.x - player.attackHurtNetwork.knockbackStart.x;
                    DemonicsFloat nextX = DemonicsFloat.Lerp(player.attackHurtNetwork.knockbackStart.x, player.attackHurtNetwork.knockbackEnd.x, ratio);
                    DemonicsVector2 nextPosition = new DemonicsVector2(nextX, player.position.y);
                    player.position = nextPosition;
                    player.knockback++;
                }
            }
            // player.player.StopShakeCoroutine();
            player.stunFrames--;
        }
        ToHurtState(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0)
        {
            player.velocity = DemonicsVector2.Zero;
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        //DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
            player.enter = false;
            player.state = "Hurt";
        }
    }
}
