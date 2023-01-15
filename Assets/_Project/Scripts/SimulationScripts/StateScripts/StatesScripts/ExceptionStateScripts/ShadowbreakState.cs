using UnityEngine;

public class ShadowbreakState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            player.attackNetwork = new AttackNetwork()
            {
                name = "Shadowbreak",
                moveName = "Shadowbreak",
                impactSound = "",
                attackSound = "",
                hurtEffect = "",
                hitstop = 10
            };
            SetTopPriority(player);
            CheckFlip(player);
            player.enter = true;
            player.animationFrames = 0;
            player.sound = "Shadowbreak";
            player.canChainAttack = false;
            player.position = new DemonicsVector2(player.position.x, player.position.y + 15);
            player.InitializeProjectile("Shadowbreak", player.attackNetwork, (DemonicsFloat)0, 0, false);
            player.SetProjectile("Shadowbreak", new DemonicsVector2(player.position.x, player.position.y + player.pushbox.size.y), false);
        }
        player.velocity = DemonicsVector2.Zero;
        player.hurtbox.active = false;
        player.animation = "Shadowbreak";
        player.animationFrames++;
        ToFallState(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.animationFrames >= 60)
        {
            player.enter = false;
            player.state = "Fall";
        }
    }
}