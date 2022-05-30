using UnityEngine;

public class WalkState : GroundParentState
{
	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Walk();
		_playerMovement.MovementSpeed = _playerStats.PlayerStatsSO.walkSpeed;
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		ToIdleState();
		ToCrouchState();
		ToJumpForwardState();
		_player.Flip();
	}

	private void ToIdleState()
	{
		if (_baseController.InputDirection.x == 0.0f)
		{
			_stateMachine.ChangeState(_idleState);
		}
	}

	public void ToCrouchState()
	{
		if (_baseController.Crouch())
		{
			_stateMachine.ChangeState(_crouchState);
		}
	}

	private void ToJumpForwardState()
	{
		if (_baseController.Jump() && !_playerMovement.HasJumped)
		{
			_playerMovement.HasJumped = true;
			_stateMachine.ChangeState(_jumpForwardState);
		}
		else if (_playerMovement.HasJumped)
		{
			_playerMovement.HasJumped = false;
		}
	}

	public override bool ToBlockState(AttackSO attack)
	{
		_blockState.Initialize(attack);
		_stateMachine.ChangeState(_blockState);
		return true;
	}

	public override bool ToGrabbedState()
	{
		_stateMachine.ChangeState(_grabbedState);
		return true;
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(_baseController.InputDirection.x * _playerMovement.MovementSpeed, _rigidbody.velocity.y);
	}
}
