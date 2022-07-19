using UnityEngine;

[RequireComponent(typeof(InputBuffer))]
public class PlayerController : BaseController
{
	private bool _hasJumped;
	private bool reset;
	private bool k;
	private bool j;
	private float _dashInputCooldown;
	private bool reset2;
	private bool k2;
	private bool j2;
	private bool _pressedAction = false;
	private bool _holdingParryTrigger = false;
	private float _dashInputCooldown2;


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

	public override bool DashForward()
	{
		float input = Input.GetAxisRaw(_brainController.ControllerInputName + "Horizontal");
		if (input == 1)
		{
			if (_dashInputCooldown > 0 && k)
			{
				if (!j)
				{
					j = true;
					return true;
				}
			}
			else
			{
				_dashInputCooldown = 0.15f;
				reset = true;
			}
		}
		else if (Input.GetAxisRaw(_brainController.ControllerInputName + "Horizontal") == 0.0f && reset)
		{
			k = true;
			if (j)
			{
				reset = false;
				k = false;
				j = false;
			}
		}

		if (_dashInputCooldown > 0)
		{
			_dashInputCooldown -= 1 * Time.deltaTime;
		}
		else
		{
			reset = false;
			k = false;
			j = false;
		}
		return false;
	}

	public override bool DashBackward()
	{
		float input = Input.GetAxisRaw(_brainController.ControllerInputName + "Horizontal");
		if (input == -1)
		{
			if (_dashInputCooldown2 > 0 && k2)
			{
				if (!j2)
				{
					j2 = true;
					return true;
				}
			}
			else
			{
				_dashInputCooldown2 = 0.15f;
				reset2 = true;
			}
		}
		else if (Input.GetAxisRaw(_brainController.ControllerInputName + "Horizontal") == 0.0f && reset2)
		{
			k2 = true;
			if (j2)
			{
				reset2 = false;
				k2 = false;
				j2 = false;
			}
		}

		if (_dashInputCooldown2 > 0)
		{
			_dashInputCooldown2 -= 1 * Time.deltaTime;
		}
		else
		{
			reset2 = false;
			k2 = false;
			j2 = false;
		}
		return false;
	}
}
