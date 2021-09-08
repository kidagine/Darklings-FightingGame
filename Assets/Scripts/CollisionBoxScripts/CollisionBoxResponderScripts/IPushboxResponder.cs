using UnityEngine;

public interface IPushboxResponder
{
	void OnGrounded();
	void OnAir();
	void GroundedPoint(Transform other, float point);
}
