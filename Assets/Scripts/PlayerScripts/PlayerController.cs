using UnityEngine;

public class PlayerController : BaseController
{
    private bool _hasJumped;

    void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        _controllerInputName = _player.IsPlayerOne is false ? SceneSettings.ControllerOne : SceneSettings.ControllerTwo;
    }

    void Update()
	{
        if (!string.IsNullOrEmpty(_controllerInputName) && _isControllerEnabled)
        {
            Movement();
            Jump();
            Crouch();
            Attack();
            if (_trainingController)
            {
                ResetRound();
            }
        }
	}

    private void Movement()
    {
        _playerMovement.MovementInput = new Vector2(Input.GetAxisRaw(_controllerInputName + "Horizontal"), Input.GetAxisRaw(_controllerInputName + "Vertical"));
	}

    private void Jump()
    {
        if (Input.GetAxisRaw(_controllerInputName + "Vertical") > 0.0f && !_hasJumped)
        {
            _hasJumped = true;
            _playerMovement.JumpAction();
        }
        else if (Input.GetAxisRaw(_controllerInputName + "Vertical") <= 0.0f && _hasJumped)
        {
            _hasJumped = false;
            _playerMovement.JumpStopAction();
        }
	}

    private void Crouch()
    {
		if (Input.GetAxisRaw(_controllerInputName + "Vertical") < 0.0f)
		{
			_playerMovement.CrouchAction();
		}
		else if (Input.GetAxisRaw(_controllerInputName + "Vertical") == 0.0f)
		{
			_playerMovement.StandUpAction();
		}
	}

    private void Attack()
    {
        if (Input.GetButtonDown(_controllerInputName + "Light"))
        {
            _player.AttackAction();
        }
    }

	private void ResetRound()
	{
        if (Input.GetButtonDown(_controllerInputName + "Reset"))
        {
            TutorialManager.Instance.ResetRound(_playerMovement.MovementInput);
        }
    }
}
