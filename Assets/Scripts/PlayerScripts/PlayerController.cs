using UnityEngine;

public class PlayerController : BaseController
{
    private bool _hasJumped;

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
            if (_trainingController)
            {
                ResetRound();
            }
        }
	}

    private void Movement()
    {
        Debug.Log(Input.GetAxisRaw(ControllerInputName + "Vertical"));
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
}
