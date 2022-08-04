using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] protected PlayerStateManager _playerStateManager;
    protected bool _trainingController;
    protected BrainController _brainController;
    protected Player _player;
    protected PlayerMovement _playerMovement;
    protected InputBuffer _inputBuffer;
    public Vector2 InputDirection { get; set; }
    public bool IsControllerEnabled { get; set; } = true;

    void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        _brainController = GetComponent<BrainController>();
        _inputBuffer = GetComponent<InputBuffer>();
    }
    public virtual bool StandUp() { return false; }
    public virtual bool Crouch() { return false; }
    public virtual bool Jump() { return false; }

    public virtual bool Dash(int direction) { return false; }

    public virtual void ActivateInput()
    {
        IsControllerEnabled = true;
    }

    public virtual void DeactivateInput()
    {
        IsControllerEnabled = false;
    }
}
