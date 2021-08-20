using UnityEngine;

public interface IHitboxResponder
{
	void HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null);
}
