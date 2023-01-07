using UnityEngine;

public class HurtState : HurtParentState
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        player.animation = "Hurt";
        if (player.animationFrames < 4)
        {
            player.animationFrames++;
        }
        ToHurtState(player);
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.stunFrames <= 0 || player.comboTimer <= 0)
        {
            player.combo = 0;
            player.player.OtherPlayerUI.ResetCombo();
            player.player.StopShakeCoroutine();
            player.player.PlayerUI.SetComboTimerActive(false);
            player.player.PlayerUI.UpdateHealthDamaged();
            player.velocity = DemonicsVector2.Zero;
            player.enter = false;
            player.state = "Idle";
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (!player.otherPlayer.canChainAttack && DemonicsCollider.Colliding(player.otherPlayer.hitbox, player.hurtbox))
        {
            player.player.StopShakeCoroutine();
            player.player.PlayerUI.UpdateHealthDamaged();
            player.attackHurtNetwork = player.otherPlayer.attackNetwork;
            player.enter = false;
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
    protected override void OnEnter(PlayerNetwork player)
    {
        base.OnEnter(player);
        CheckFlip(player);
    }
    protected override void Knockback(PlayerNetwork player)
    {
        if (!DemonicsPhysics.IsInCorner(player))
        {
            DemonicsFloat ratio = (DemonicsFloat)player.knockback / (DemonicsFloat)player.attackHurtNetwork.knockbackDuration;
            DemonicsFloat distance = player.pushbackEnd.x - player.pushbackStart.x;
            DemonicsFloat nextX = DemonicsFloat.Lerp(player.pushbackStart.x, player.pushbackEnd.x, ratio);
            DemonicsFloat baseY = DemonicsFloat.Lerp(player.pushbackStart.y, player.pushbackEnd.y, (nextX - player.pushbackStart.x) / distance);
            DemonicsFloat arc = player.attackHurtNetwork.knockbackArc * (nextX - player.pushbackStart.x) * (nextX - player.pushbackEnd.x) / ((-0.25f) * distance * distance);
            DemonicsVector2 nextPosition = DemonicsVector2.Zero;
            if (player.attackHurtNetwork.knockbackArc == 0 || player.attackHurtNetwork.softKnockdown)
            {
                nextPosition = new DemonicsVector2(nextX, player.position.y);
            }
            else
            {
                nextPosition = new DemonicsVector2(nextX, baseY + arc);
            }
            player.position = nextPosition;
            player.knockback++;
        }
    }
}
