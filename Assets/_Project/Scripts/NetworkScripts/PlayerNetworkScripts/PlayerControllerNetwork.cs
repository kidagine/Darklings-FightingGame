using UnityEngine;

public class PlayerControllerNetwork : PlayerController
{
	protected override void Movement()
	{
		if (IsClient && IsOwner)
		{
			base.Movement();
		}
	}

	protected override void Jump()
	{
		if (IsClient && IsOwner)
		{
			base.Jump();
		}
	}

	protected override void Crouch()
	{
		if (IsClient && IsOwner)
		{
			base.Crouch();
		}
	}

	protected override void Attack()
	{
		if (IsClient && IsOwner)
		{
			base.Attack();
		}
	}

	protected override void Arcane()
	{
		if (IsClient && IsOwner)
		{
			base.Arcane();
		}
	}

	protected override void Assist()
	{
		if (IsClient && IsOwner)
		{
			base.Assist();
		}
	}

	protected override void Dash()
	{
		if (IsClient && IsOwner)
		{
			base.Dash();
		}
	}
}
