using UnityEngine;

public class PlayerController : BaseController
{
    private float timeOfFirstButton = 0.0f;
    private bool _hasJumped;
    private bool firstButtonPressed;
    private bool reset;
    private bool k;
    private bool j;

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
        float input = Input.GetAxisRaw(ControllerInputName + "Horizontal");
        if (input == 1.0f || input == -1.0f)
        {
			if (k)
			{
                if (!j)
                {
                    Debug.Log("DoubleClicked");
                    _playerMovement.Dash(input);
                }
                j = true;
            }
            else
            {
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

        //if (Input.GetAxisRaw(ControllerInputName + "Horizontal") == 1.0f && firstButtonPressed)
        //{
        //    if (Time.time - timeOfFirstButton < 0.5f)
        //    {
        //        Debug.Log("DoubleClicked");
        //    }
        //    else
        //    {
        //        Debug.Log("Too late");
        //    }

        //    reset = true;
        //}
        //else if (Input.GetAxisRaw(ControllerInputName + "Horizontal") == 0.0f)
        //{
        //    Debug.Log("1");
        //    k = true;
        //}
        //if (k)
        //{
        //    if (Input.GetAxisRaw(ControllerInputName + "Horizontal") == 1.0 && !firstButtonPressed)
        //    {
        //        firstButtonPressed = true;
        //        timeOfFirstButton = Time.time;
        //    }

        //    if (reset)
        //    {
        //        firstButtonPressed = false;
        //        reset = false;
        //    }
        //}

    }
}
