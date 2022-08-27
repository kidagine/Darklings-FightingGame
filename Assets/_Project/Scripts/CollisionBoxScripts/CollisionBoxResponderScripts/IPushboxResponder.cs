using UnityEngine;

public interface IPushboxResponder
{
    void OnGrounded();
    void OnAir();
    void GroundedPoint(Vector2 point);
}
