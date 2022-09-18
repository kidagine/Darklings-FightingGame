using UnityEngine;

public interface IHitboxResponder
{
    bool HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null);
}
