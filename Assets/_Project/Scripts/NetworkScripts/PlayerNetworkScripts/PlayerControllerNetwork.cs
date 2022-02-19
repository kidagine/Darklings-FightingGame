using UnityEngine;

public class PlayerControllerNetwork : PlayerController
{
	protected override void Movement()
	{
		if (IsClient)
		{
			Debug.Log(gameObject.name);
			base.Movement();
		}
	}
}
