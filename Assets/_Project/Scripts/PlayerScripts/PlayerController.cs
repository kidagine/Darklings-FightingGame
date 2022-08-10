using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(InputBuffer))]
public class PlayerController : BaseController
{
	void Start()
	{
		_playerInput.actions.actionMaps[(int)ActionSchemeTypes.Training].Enable();
	}

	//GAMEPLAY
	public void Movement(CallbackContext callbackContext)
	{
		InputDirection = callbackContext.ReadValue<Vector2>();
		if (InputDirection.x == 1.0f && _playerMovement.MovementInput.x != InputDirection.x)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Right);
		}
		if (InputDirection.x == -1.0f && _playerMovement.MovementInput.x != InputDirection.x)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Left);
		}
		if (InputDirection.y == 1.0f && _playerMovement.MovementInput.y != InputDirection.y)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Up);
		}
		if (InputDirection.y == -1.0f && _playerMovement.MovementInput.y != InputDirection.y)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Down);
		}
		if (InputDirection == Vector2.zero && _playerMovement.MovementInput != InputDirection)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.None);
		}
		_playerMovement.MovementInput = InputDirection;
	}

	public override bool Jump()
	{
		if (InputDirection.y > 0.5f)
		{
			return true;
		}
		return false;
	}

	public override bool Crouch()
	{
		if (InputDirection.y < -0.5f)
		{
			return true;
		}
		return false;
	}

	public override bool StandUp()
	{
		if (InputDirection.y == 0.0f)
		{
			return true;
		}
		return false;
	}

	public void Jump(CallbackContext callbackContext)
	{
		//_inputBuffer.AddInputBufferItem(InputEnum.Light);
	}

	public void Crouch(CallbackContext callbackContext)
	{
		//_inputBuffer.AddInputBufferItem(InputEnum.Light);
	}

	public void StandUp(CallbackContext callbackContext)
	{
		//_inputBuffer.AddInputBufferItem(InputEnum.Light);
	}

	public void Light(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Light);
		}
	}

	public void Medium(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Medium);
		}
	}

	public void Heavy(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Heavy);
		}
	}

	public void Arcane(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Special);
		}
	}
	public void Assist(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Assist);
		}
	}

	public void Throw(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			_playerStateManager.TryToGrabState();
			_inputBuffer.AddInputBufferItem(InputEnum.Throw);
		}
	}

	public void Parry(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			_playerStateManager.TryToParryState();
			_inputBuffer.AddInputBufferItem(InputEnum.Parry);
		}
	}

	public void Dash(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			if (InputDirection.x == 1)
			{
				_inputBuffer.AddInputBufferItem(InputEnum.ForwardDash);
			}
			else if (InputDirection.x == -1)
			{
				_inputBuffer.AddInputBufferItem(InputEnum.BackDash);
			}
		}
	}

	public void Pause(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			_player.Pause(_brainController.IsPlayerOne);
		}
	}
	//TRAINING
	public void Reset(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			GameManager.Instance.ResetRound(InputDirection);
		}
	}

	public void Switch(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			GameManager.Instance.SwitchCharacters();
		}
	}
	//UI
	public void Confirm(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			CurrentPrompts?.OnConfirm?.Invoke();
		}
	}

	public void Back(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			CurrentPrompts?.OnBack?.Invoke();
		}
	}

	public void Stage(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			CurrentPrompts?.OnStage?.Invoke();
		}
	}

	public void Coop(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			CurrentPrompts?.OnCoop?.Invoke();
		}
	}

	public void Controls(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			CurrentPrompts?.OnControls?.Invoke();
		}
	}

	public void PageLeft(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			CurrentPrompts?.OnLeftPage?.Invoke();
		}
	}

	public void PageRight(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			CurrentPrompts?.OnRightPage?.Invoke();
		}
	}
}
