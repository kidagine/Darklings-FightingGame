using UnityEngine;

[RequireComponent(typeof(InputBuffer))]
public class PlayerController : BaseController
{
	private int _lastDashDirection;
	private bool _dashPressed;
	private float _dashLastInputTime;
	private float _dashTime = 0.3f;

	private bool _pressedAction = false;
	private bool _holdingParryTrigger = false;


	void Update()
	{
		if (GameManager.Instance.HasGameStarted)
		{
			if (!string.IsNullOrEmpty(_brainController.ControllerInputName) && !SceneSettings.ReplayMode)
			{
				if (IsControllerEnabled)
				{
					Movement();
					Jump();
					Crouch();
					Parry();
					Throw();
					Light();
					Medium();
					Heavy();
					Arcane();
					Assist();
					Dash(1);
					Dash(-1);
					_pressedAction = false;
				}
				Pause();
				ResetRound();
				SwitchCharacter();
			}
		}
		else
		{
			InputDirection = Vector2.zero;
		}
	}

	protected virtual void Movement()
	{
		InputDirection = new(Input.GetAxisRaw(_brainController.ControllerInputName + "Horizontal"), Input.GetAxisRaw(_brainController.ControllerInputName + "Vertical"));
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
		if (InputDirection.y > 0.0f)
		{
			return true;
		}
		return false;
	}

	public override bool Crouch()
	{
		if (InputDirection.y < 0.0f)
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

	protected virtual void Light()
	{
		if (!_pressedAction)
		{
			if (Input.GetButtonDown(_brainController.ControllerInputName + "Light"))
			{
				_inputBuffer.AddInputBufferItem(InputEnum.Light);
				_pressedAction = true;
			}
		}
	}

	protected virtual void Medium()
	{
		if (!_pressedAction)
		{
			if (Input.GetButtonDown(_brainController.ControllerInputName + "Medium"))
			{
				_inputBuffer.AddInputBufferItem(InputEnum.Medium);
				_pressedAction = true;
			}
		}
	}

	protected virtual void Heavy()
	{
		if (!_pressedAction)
		{
			if (Input.GetButtonDown(_brainController.ControllerInputName + "Heavy"))
			{
				_inputBuffer.AddInputBufferItem(InputEnum.Heavy);
				_pressedAction = true;
			}
		}
	}

	protected virtual void Arcane()
	{
		if (!_pressedAction)
		{
			if (Input.GetButtonDown(_brainController.ControllerInputName + "Arcane"))
			{
				_inputBuffer.AddInputBufferItem(InputEnum.Special);
				_pressedAction = true;
			}
		}
	}

	protected virtual void Assist()
	{
		if (Input.GetButtonDown(_brainController.ControllerInputName + "Assist"))
		{
			_inputBuffer.AddInputBufferItem(InputEnum.Assist);
		}
	}

	protected virtual void Throw()
	{
		if (!_pressedAction)
		{
			if ((Input.GetButtonDown(_brainController.ControllerInputName + "Light") &&
				Input.GetButtonDown(_brainController.ControllerInputName + "Medium")) ||
				Input.GetButtonDown(_brainController.ControllerInputName + "Throw"))
			{
				_playerStateManager.TryToGrabState();
				_inputBuffer.AddInputBufferItem(InputEnum.Throw);
				_pressedAction = true;
			}
		}
	}

	protected virtual void Parry()
	{
		if (!_pressedAction)
		{
			if ((Input.GetButtonDown(_brainController.ControllerInputName + "Medium") &&
				Input.GetButtonDown(_brainController.ControllerInputName + "Heavy")) ||
				Input.GetButtonDown(_brainController.ControllerInputName + "Parry") ||
				Input.GetAxis(_brainController.ControllerInputName + "Parry") >= 0.9f &&
				!_holdingParryTrigger)
			{
				_playerStateManager.TryToParryState();
				_inputBuffer.AddInputBufferItem(InputEnum.Parry);
				_pressedAction = true;
				_holdingParryTrigger = true;
			}
			if (Input.GetAxis(_brainController.ControllerInputName + "Parry") == 0.0f)
			{
				_holdingParryTrigger = false;
			}
		}
	}

	private void ResetRound()
	{
		if (Input.GetButtonDown(_brainController.ControllerInputName + "Reset"))
		{
			GameManager.Instance.ResetRound(_playerMovement.MovementInput);
		}
	}

	private void SwitchCharacter()
	{
		if (Input.GetButtonDown(_brainController.ControllerInputName + "Switch"))
		{
			GameManager.Instance.SwitchCharacters();
		}
	}

	private void Pause()
	{
		if (GameManager.Instance.HasGameStarted)
		{
			if (Input.GetButtonDown(_brainController.ControllerInputName + "Pause"))
			{
				_player.Pause(_brainController.IsPlayerOne);
			}
			if (Input.GetButtonUp(_brainController.ControllerInputName + "Pause"))
			{
				_player.UnPause();
			}
		}
	}

	public override bool Dash(int direction)
	{
		float input = Input.GetAxisRaw(_brainController.ControllerInputName + "Horizontal");
		if (input == direction && !_dashPressed)
		{
			_dashPressed = true;
			float timeSinceLastPress = Time.time - _dashLastInputTime;
			if (timeSinceLastPress <= _dashTime && direction == _lastDashDirection)
			{
				if (direction == 1)
				{
					_inputBuffer.AddInputBufferItem(InputEnum.ForwardDash);
				}
				else
				{
					_inputBuffer.AddInputBufferItem(InputEnum.BackDash);
				}
				return true;
			}
			_lastDashDirection = direction;
			_dashLastInputTime = Time.time;
		}
		else if (input == 0)
		{
			_dashPressed = false;
		}

		return false;
	}
}
