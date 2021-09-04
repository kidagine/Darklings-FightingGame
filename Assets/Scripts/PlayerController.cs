using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerMovement _playerMovement;
    private string _controllerInputName;
    private bool _hasJumped;

    public bool DisableThoughtsInput { private get; set; }
    public bool DisableCodexInput { private get; set; }


    void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        _controllerInputName = _player.IsPlayerOne is true ? SceneSettings.ControllerOne : SceneSettings.ControllerTwo;
    }

	void Update()
	{
        Movement();
        Jump();
        Crouch();
        Attack();
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

    //public void ActivateInput()
    //{
    //    _playerInput.ActivateInput();
    //}

    //public void DeactivateInput()
    //{
    //    _playerInput.DeactivateInput();
    //}
}
