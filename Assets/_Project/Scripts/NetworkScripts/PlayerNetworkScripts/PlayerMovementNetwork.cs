using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovementNetwork : PlayerMovement
{
	private readonly NetworkVariable<Vector2> MovementInputNetwork = new NetworkVariable<Vector2>();


	private void Update()
	{
		if (IsClient && IsOwner) 
		{
			MovementInputServerRpc();
		}
	}

	protected override void Movement()
	{
		if (!IsCrouching && !_player.IsAttacking && !_onTopOfPlayer && !IsDashing && !_isMovementLocked)
		{
			if (!_player.IsBlocking && !_player.IsKnockedDown)
			{
				_rigidbody.velocity = new Vector2(MovementInput.x * _movementSpeed, _rigidbody.velocity.y);
			}
			else
			{
				_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
			}
			_playerAnimator.SetMovementX(MovementInputNetwork.Value.x * transform.localScale.x);
			if (MovementInputNetwork.Value.x != 0.0f)
			{
				if (_rigidbody.velocity.x > 0.0f && transform.localScale.x == 1.0f)
				{
					_player.ArcaneSlowdown = 6.5f;
				}
				else if (_rigidbody.velocity.x < 0.0f && transform.localScale.x == -1.0f)
				{
					_player.ArcaneSlowdown = 6.5f;
				}
				else
				{
					ResetToWalkSpeed();
				}
				Debug.Log("bbb");
				IsMoving = true;
				_playerAnimator.SetMove(true);
			}
			else
			{
				ResetToWalkSpeed();
				IsMoving = false;
				_player.ArcaneSlowdown = 8.0f;
				_playerAnimator.SetMove(false);
			}
		}
		else
		{
			IsMoving = false;
		}
	}

	[ServerRpc]
	public void MovementInputServerRpc()
	{
		MovementInputNetwork.Value = MovementInput;
	}
}
