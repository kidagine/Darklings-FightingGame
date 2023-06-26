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
                hitstop = 10,
                blockStun = 10,
                cameraShakerNetwork = new CameraShakerNetwork() { intensity = 35, timer = 0.15f }
            };
            SetTopPriority(player);
            CheckFlip(player);
            player.enter = true;
            player.animationFrames = 0;
            player.sound = "Shadowbreak";
            player.canChainAttack = false;
            player.usedShadowbreak = true;
            player.position = new DemonicsVector2(player.position.x, player.position.y + 15);
            player.InitializeProjectile("Shadowbreak", player.attackNetwork, (DemonicsFloat)0, 0, false);
            player.SetProjectile("Shadowbreak", new DemonicsVector2(player.position.x, player.position.y + player.pushbox.size.y), false);
            CameraShake.Instance.Shake(player.attackNetwork.cameraShakerNetwork);
            return;
        }
        player.velocity = DemonicsVector2.Zero;
        player.animation = "Shadowbreak";
        player.animationFrames++;
        ToHurtState(player);
        ToFallState(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.animationFrames >= 60)
        {
            CheckTrainingGauges(player);
            EnterState(player, "Fall");
        }
    }
    private void ToHurtState(PlayerNetwork player)
    {
        if (IsColliding(player))
        {
            if (player.attackHurtNetwork.moveName == "Shadowbreak")
            {
                EnterState(player, "Knockback");
                return;
            }
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
                return;
            if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.softKnockdown && player.position.y > DemonicsPhysics.GROUND_POINT)
            {
                EnterState(player, "Airborne");
            }
            else
            {
                EnterState(player, "HurtAir");
            }
        }
    }
}