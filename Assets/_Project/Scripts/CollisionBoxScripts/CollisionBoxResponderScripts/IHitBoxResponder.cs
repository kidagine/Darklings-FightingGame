using UnityEngine;

public interface IHitboxResponder
{
    bool HitboxCollided(Vector2 hurtPosition, Hurtbox hurtbox = null);
}
