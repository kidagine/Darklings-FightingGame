using FixMath.NET;
using UnityEngine;

public class Pushbox : DemonicsCollider
{
	public void SetIsTrigger(bool state)
	{
		this.IgnoreCollision = state;
	}

	protected override void OnDrawGizmos()
	{
		GizmoColor = Color.blue;
		base.OnDrawGizmos();
	}
}
