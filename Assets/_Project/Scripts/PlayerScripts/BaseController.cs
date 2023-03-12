using UnityEngine;
using UnityEngine.InputSystem;

public class BaseController : MonoBehaviour
{
    protected BrainController _brainController;
    protected Player _player;
    protected PlayerMovement _playerMovement;
    protected InputBuffer _inputBuffer;
    protected PlayerInput _playerInput;
    public Vector2Int InputDirection { get; set; }
    public bool IsControllerEnabled { get; set; } = true;

    void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        _brainController = GetComponent<BrainController>();
        _inputBuffer = GetComponent<InputBuffer>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public virtual bool StandUp() { return false; }
    public virtual bool Crouch() { return false; }
    public virtual bool Jump() { return false; }

    public virtual bool Dash(int direction) { return false; }

    public virtual void SetEnable(bool state)
    {
        _playerInput.enabled = state;
    }

    public virtual void ActivateInput()
    {
        IsControllerEnabled = true;
    }

    public virtual void DeactivateInput()
    {
        IsControllerEnabled = false;
    }
}
