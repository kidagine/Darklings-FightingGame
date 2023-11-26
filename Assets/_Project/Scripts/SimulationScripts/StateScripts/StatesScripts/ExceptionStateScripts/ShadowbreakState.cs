using SharedGame;
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
                hitstop = 5,
                blockStun = 15,
                cameraShakerNetwork = new CameraShakerNetwork() { intensity = 35, timer = 0.15f }
            };
            SetTopPriority(player);
            CheckFlip(player);
            player.enter = true;
            player.animationFrames = 0;
            player.sound = "Shadowbreak";
            player.canChainAttack = false;
            player.usedShadowbreak = true;
            player.position = new DemonVector2(player.position.x, player.position.y + 15);
            if (player.player.IsPlayerOne)
                GameplayManager.Instance.SetCameraTargets(0.8f, 0.2f);
            else
                GameplayManager.Instance.SetCameraTargets(0.2f, 0.8f);
            player.InitializeProjectile("Shadowbreak", player.attackNetwork, (DemonFloat)0, 0, false);
            player.SetProjectile("Shadowbreak", new DemonVector2(player.position.x, player.position.y + player.pushbox.size.y), false);
            CameraShake.Instance.Shake(player.attackNetwork.cameraShakerNetwork);
            return;
        }
        player.velocity = DemonVector2.Zero;
        player.animation = "Shadowbreak";
        player.animationFrames++;
        ToHurtState(player);
        ToFallState(player);
    }
    private void ToFallState(PlayerNetwork player)
    {
        if (player.animationFrames >= 60)
        {
            GameplayManager.Instance.SetCameraTargets(0.5f, 0.5f);
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
                GameplayManager.Instance.SetCameraTargets(0.5f, 0.5f);
                EnterState(player, "Knockback");
                return;
            }
            if (player.attackHurtNetwork.attackType == AttackTypeEnum.Throw)
                return;
            if (player.attackHurtNetwork.hardKnockdown || player.attackHurtNetwork.softKnockdown && player.position.y > DemonicsPhysics.GROUND_POINT)
            {
                GameplayManager.Instance.SetCameraTargets(0.5f, 0.5f);
                EnterState(player, "Airborne");
            }
            else
            {
                GameplayManager.Instance.SetCameraTargets(0.5f, 0.5f);
                EnterState(player, "HurtAir");
            }
        }
    }
}