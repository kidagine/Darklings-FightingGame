using SharedGame;
using System.Collections.Generic;
using UnityEngine;
using UnityGGPO;

public class ProjectilesSimulation
{
    public static void HandleProjectileCollision(PlayerNetwork player, int index)
    {
        if (player.shadow.projectile.active)
        {
            ShadowToShadow(player, index);
        }
        for (int i = 0; i < player.projectiles.Length; i++)
        {
            if (player.projectiles[i].active)
            {
                ProjectileToProjectile(player, index, i);
            }
        }
    }

    private static void ShadowToShadow(PlayerNetwork player, int index)
    {
        if (DemonicsCollider.Colliding(player.shadow.projectile.hitbox, player.otherPlayer.shadow.projectile.hitbox))
        {
            if (player.shadow.projectile.priority < player.otherPlayer.shadow.projectile.priority)
            {
                DisableShadow(player);
            }
            else if (player.shadow.projectile.priority == player.otherPlayer.shadow.projectile.priority)
            {
                DisableShadow(player);
                DisableShadow(player.otherPlayer);
            }
        }
        for (int i = 0; i < player.otherPlayer.projectiles.Length; i++)
        {
            if (DemonicsCollider.Colliding(player.shadow.projectile.hitbox, player.otherPlayer.projectiles[i].hitbox))
            {
                if (player.shadow.projectile.priority < player.otherPlayer.projectiles[i].priority)
                {
                    DisableShadow(player);
                }
                else if (player.shadow.projectile.priority == player.otherPlayer.shadow.projectile.priority)
                {
                    DisableShadow(player);
                    DisableProjectile(player.otherPlayer, i);
                }
            }
        }

        if (!player.shadow.projectile.hitstop)
        {
            player.shadow.projectile.animationFrames++;
        }
        if (player.shadow.projectile.position.y <= DemonicsPhysics.GROUND_POINT)
        {
            DisableShadow(player);
        }
        if (player.shadow.projectile.animationFrames >= player.shadow.projectile.animationMaxFrames)
        {
            DisableShadow(player, false);
        }
        if (player.shadow.projectile.active)
        {
            DemonVector2 t = (new DemonVector2((DemonFloat)player.shadow.spawnRotation.x * (DemonFloat)player.shadow.flip, (DemonFloat)player.shadow.spawnRotation.y) * (DemonFloat)player.shadow.projectile.speed);
            player.shadow.projectile.position = new DemonVector2(player.shadow.projectile.position.x + t.x, player.shadow.projectile.position.y + t.y);
            AnimationBox[] hitboxes = ObjectPoolingManager.Instance.GetAssistPoolAnimation(index, player.shadow.projectile.name).GetHitboxes("Idle", player.shadow.projectile.animationFrames);
            if (hitboxes.Length > 0)
            {
                player.shadow.projectile.hitbox.size = new DemonVector2((DemonFloat)hitboxes[0].size.x, (DemonFloat)hitboxes[0].size.y);
                player.shadow.projectile.hitbox.offset = new DemonVector2((DemonFloat)hitboxes[0].offset.x, (DemonFloat)hitboxes[0].offset.y);
                player.shadow.projectile.hitbox.position = new DemonVector2(player.shadow.projectile.position.x + (player.shadow.projectile.hitbox.offset.x * player.flip), player.shadow.projectile.position.y + player.shadow.projectile.hitbox.offset.y);
                player.shadow.projectile.hitbox.active = true;
            }
        }
    }

    private static void ProjectileToProjectile(PlayerNetwork player, int index, int i)
    {
        for (int j = 0; j < player.otherPlayer.projectiles.Length; j++)
        {
            if (DemonicsCollider.Colliding(player.projectiles[i].hitbox, player.otherPlayer.projectiles[j].hitbox))
            {
                if (player.projectiles[i].priority < player.otherPlayer.projectiles[j].priority)
                {
                    if (player.projectiles[i].priority > -1)
                    {
                        DisableProjectile(player, i);
                    }
                }
                else if (player.projectiles[i].priority == player.otherPlayer.projectiles[j].priority)
                {
                    if (player.projectiles[i].priority > -1)
                    {
                        DisableProjectile(player, i);
                        DisableProjectile(player.otherPlayer, i);
                    }
                }
            }
        }

        if (!player.projectiles[i].hitstop)
        {
            player.projectiles[i].animationFrames++;
        }
        if (player.projectiles[i].animationFrames >= player.projectiles[i].animationMaxFrames)
        {
            DisableProjectile(player, i, false);
        }
        if (player.projectiles[i].active)
        {
            player.projectiles[i].position = new DemonVector2(player.projectiles[i].position.x + (player.projectiles[i].speed * player.flip), player.projectiles[i].position.y);
            AnimationBox[] hitboxes = ObjectPoolingManager.Instance.GetObjectPoolAnimation(index, player.projectiles[i].name).GetHitboxes("Idle", player.projectiles[i].animationFrames);
            if (hitboxes.Length > 0)
            {
                player.projectiles[i].hitbox.size = new DemonVector2((DemonFloat)hitboxes[0].size.x, (DemonFloat)hitboxes[0].size.y);
                player.projectiles[i].hitbox.offset = new DemonVector2((DemonFloat)hitboxes[0].offset.x, (DemonFloat)hitboxes[0].offset.y);
                player.projectiles[i].hitbox.position = new DemonVector2(player.projectiles[i].position.x + (player.projectiles[i].hitbox.offset.x * player.flip), player.projectiles[i].position.y + player.projectiles[i].hitbox.offset.y);
                player.projectiles[i].hitbox.active = true;
            }
        }
    }
    private static void DisableShadow(PlayerNetwork player, bool explode = true)
    {
        if (player.shadow.projectile.active)
        {
            player.shadow.projectile.hitbox.enter = false;
            player.shadow.projectile.animationFrames = 0;
            player.shadow.projectile.active = false;
            player.shadow.projectile.hitbox.active = false;
            if (explode)
            {
                player.SetEffect("Impact", player.shadow.projectile.position);
            }
        }
    }
    private static void DisableProjectile(PlayerNetwork player, int i, bool explode = true)
    {
        if (player.projectiles[i].active)
        {
            player.projectiles[i].hitbox.enter = false;
            player.projectiles[i].animationFrames = 0;
            player.projectiles[i].active = false;
            player.projectiles[i].hitbox.active = false;
            if (explode)
            {
                player.SetEffect("Impact", player.projectiles[i].position);
            }
        }
    }
}
