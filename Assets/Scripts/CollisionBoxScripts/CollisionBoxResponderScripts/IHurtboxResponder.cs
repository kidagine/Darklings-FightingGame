using UnityEngine;

public interface IHurtboxResponder
{
	void TakeDamage(int damage, float hitStun = 0, Vector2 knockbackDirection = default, float knockbackForce = default);
}
