using UnityEngine;

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
    private float _dashInputCooldown2;
    void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
	{
        if (!string.IsNullOrEmpty(ControllerInputName) && _isControllerEnabled)
        {
            Movement();
            Jump();
            Crouch();
            Attack();
            Arcane();
            Pause();
            Dash();
            if (_trainingController)
            {
                ResetRound();
            }
        }
	}

    private void Movement()
    {
        _playerMovement.MovementInput = new Vector2(Input.GetAxisRaw(ControllerInputName + "Horizontal"), Input.GetAxisRaw(ControllerInputName + "Vertical"));
	}

    private void Jump()
    {
        if (Input.GetAxisRaw(ControllerInputName + "Vertical") > 0.0f && !_hasJumped)
        {
            _hasJumped = true;
            _playerMovement.JumpAction();
        }
        else if (Input.GetAxisRaw(ControllerInputName + "Vertical") <= 0.0f && _hasJumped)
        {
            _hasJumped = false;
            _playerMovement.JumpStopAction();
        }
	}

    private void Crouch()
    {
		if (Input.GetAxisRaw(ControllerInputName + "Vertical") < 0.0f)
		{
			_playerMovement.CrouchAction();
		}
		else if (Input.GetAxisRaw(ControllerInputName + "Vertical") == 0.0f)
		{
			_playerMovement.StandUpAction();
		}
	}

    private void Attack()
    {
        if (Input.GetButtonDown(ControllerInputName + "Light"))
        {
            _player.AttackAction();
        }
    }

    private void Arcane()
    {
        if (Input.GetButtonDown(ControllerInputName + "Arcane"))
        {
            _player.ArcaneAction();
        }
    }

    private void ResetRound()
	{
        if (Input.GetButtonDown(ControllerInputName + "Reset"))
        {
            GameManager.Instance.ResetRound(_playerMovement.MovementInput);
        }
    }

    private void Pause()
    {
        if (Input.GetButtonDown(ControllerInputName + "Pause"))
        {
            _player.Pause();
        }
        if (Input.GetButtonUp(ControllerInputName + "Pause"))
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
        float input = Input.GetAxisRaw(ControllerInputName + "Horizontal");
        if (input == directionX)
        {
            if (_dashInputCooldown > 0 && k)
            {
                if (!j)
                {
                    _playerMovement.Dash(input);
                }
                j = true;
            }
            else
            {
                _dashInputCooldown = 0.15f;
                reset = true;
            }
        }
        else if (Input.GetAxisRaw(ControllerInputName + "Horizontal") == 0.0f && reset)
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
        float input = Input.GetAxisRaw(ControllerInputName + "Horizontal");
        if (input == directionX)
        {
            if (_dashInputCooldown2 > 0 && k2)
            {
                if (!j2)
                {
                    _playerMovement.Dash(input);
                }
                j2 = true;
            }
            else
            {
                _dashInputCooldown2 = 0.15f;
                reset2 = true;
            }
        }
        else if (Input.GetAxisRaw(ControllerInputName + "Horizontal") == 0.0f && reset2)
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
