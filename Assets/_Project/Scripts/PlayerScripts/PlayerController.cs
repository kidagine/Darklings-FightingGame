using UnityEngine;

public class PlayerController : BaseController
{
	[SerializeField] private InputBuffer _inputBuffer = default;
	private bool _hasJumped;
	private bool reset;
	private bool k;
	private bool j;
	private float _dashInputCooldown;
	private bool reset2;
	private bool k2;
	private bool j2;
	private float _dashInputCooldown2;


	void Update()
	{
		if (!string.IsNullOrEmpty(_brainController.ControllerInputName) && _isControllerEnabled)
		{
			Movement();
			Jump();
			Crouch();
			Attack();
			Arcane();
			Assist();
			Pause();
			Dash();
			ResetRound();
			SwitchCharacter();
		}
	}

	private void Movement()
	{
		Vector2 currentInput = new Vector2(Input.GetAxisRaw(_brainController.ControllerInputName + "Horizontal"), Input.GetAxisRaw(_brainController.ControllerInputName + "Vertical"));
		if (currentInput.x == 1.0f && _playerMovement.MovementInput.x != currentInput.x)
		{
			_inputBuffer.CheckInput(InputEnum.Right);
		}
		if (currentInput.x == -1.0f && _playerMovement.MovementInput.x != currentInput.x)
		{
			_inputBuffer.CheckInput(InputEnum.Left);
		}
		if (currentInput.y == 1.0f && _playerMovement.MovementInput.y != currentInput.y)
		{
			_inputBuffer.CheckInput(InputEnum.Up);
		}
		if (currentInput.y == -1.0f && _playerMovement.MovementInput.y != currentInput.y)
		{
			_inputBuffer.CheckInput(InputEnum.Down);
		}
		_playerMovement.MovementInput = currentInput;
	}

	private void Jump()
	{
		if (Input.GetAxisRaw(_brainController.ControllerInputName + "Vertical") > 0.0f && !_hasJumped)
		{
			_hasJumped = true;
			_playerMovement.JumpAction();
		}
		else if (Input.GetAxisRaw(_brainController.ControllerInputName + "Vertical") <= 0.0f && _hasJumped)
		{
			_hasJumped = false;
		}
	}

	private void Crouch()
	{
		if (Input.GetAxisRaw(_brainController.ControllerInputName + "Vertical") < 0.0f)
		{
			_playerMovement.CrouchAction();
		}
		else if (Input.GetAxisRaw(_brainController.ControllerInputName + "Vertical") == 0.0f)
		{
			_playerMovement.StandUpAction();
		}
	}

	private void Attack()
	{
		if (Input.GetButtonDown(_brainController.ControllerInputName + "Light"))
		{
			_player.AttackAction();
			_inputBuffer.CheckInput(InputEnum.Light);
		}
	}

	private void Arcane()
	{
		if (Input.GetButtonDown(_brainController.ControllerInputName + "Arcane"))
		{
			_player.ArcaneAction();
			_inputBuffer.CheckInput(InputEnum.Special);
		}
	}

	private void Assist()
	{
		if (Input.GetButtonDown(_brainController.ControllerInputName + "Assist"))
		{
			_player.AssistAction();
			_inputBuffer.CheckInput(InputEnum.Assist);
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
		if (Input.GetButtonDown(_brainController.ControllerInputName + "Pause"))
		{
			_player.Pause(_brainController.IsPlayerOne);
		}
		if (Input.GetButtonUp(_brainController.ControllerInputName + "Pause"))
		{
			_player.UnPause();
		}
	}

	private void Dash()
	{
		DoubleTapAxis(1);
		DoubleTapAxis2(-1);
	}

	private void DoubleTapAxis(int directionX)
	{
		float input = Input.GetAxisRaw(_brainController.ControllerInputName + "Horizontal");
		if (input == directionX)
		{
			if (_dashInputCooldown > 0 && k)
			{
				if (!j)
				{
					_playerMovement.DashAction(input);
				}
				j = true;
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
	}

	private void DoubleTapAxis2(int directionX)
	{
		float input = Input.GetAxisRaw(_brainController.ControllerInputName + "Horizontal");
		if (input == directionX)
		{
			if (_dashInputCooldown2 > 0 && k2)
			{
				if (!j2)
				{
					_playerMovement.DashAction(input);
				}
				j2 = true;
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
	}
}
