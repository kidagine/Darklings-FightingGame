using UnityEngine;

public class IdleState : GroundParentState
{
	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Idle();
		_player.SetAirPushBox(false);
		_player.SetPushboxTrigger(false);
		_playerMovement.HasAirDashed = false;
		_playerMovement.HasDoubleJumped = false;
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		ToWalkState();
		ToCrouchState();
		ToJumpState();
		ToDashState();
		_player.Flip();
	}

	private void ToWalkState()
	{
		if (_baseController.InputDirection.x != 0.0f)
		{
			_stateMachine.ChangeState(_walkState);
		}
	}

	private void ToCrouchState()
	{
		if (_baseController.Crouch())
		{
			_stateMachine.ChangeState(_crouchState);
		}
	}

	private void ToJumpState()
	{
		if (_baseController.Jump() && !_playerMovement.HasJumped)
		{
			_playerMovement.HasJumped = true;
			_stateMachine.ChangeState(_jumpState);
		}
		else if (_playerMovement.HasJumped)
		{
			_playerMovement.HasJumped = false;
		}
	}

	private void ToDashState()
	{
		if (_baseController.DashForward())
		{
			_dashState.DashDirection = 1;
			_stateMachine.ChangeState(_dashState);
		}
		else if (_baseController.DashBackward())
		{
			_dashState.DashDirection = -1;
			_stateMachine.ChangeState(_dashState);
		}
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
	}
}
