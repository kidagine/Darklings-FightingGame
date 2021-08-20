using UnityEngine;

public interface IHurtboxResponder
{
	void TakeDamage(int damage, Vector2 knockbackDirection = default, float knockbackForce = default);
}
