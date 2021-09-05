using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerMovement _playerMovement;
    private string _controllerInputName;
    private bool _hasJumped;
    private bool _isControllerEnabled = true;

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
        }
	}

	public void Movement()
    {
        _playerMovement.MovementInput = new Vector2(Input.GetAxisRaw(_controllerInputName + "Horizontal"), 0.0f);
	}

    public void Jump()
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

    public void Crouch()
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

    public void Attack()
    {
        if (Input.GetButtonDown(_controllerInputName + "Light"))
        {
            _player.AttackAction();
        }
    }

	public void ActivateInput()
	{
        _isControllerEnabled = true;
    }

	public void DeactivateInput()
	{
        _isControllerEnabled = false;
    }
}
