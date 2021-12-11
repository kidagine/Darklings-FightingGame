using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected bool _trainingController;
    protected bool _isControllerEnabled = true;
    protected BrainController _brainController;
    protected Player _player;
    protected PlayerMovement _playerMovement;
    protected InputBuffer _inputBuffer;


    void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        _brainController = GetComponent<BrainController>();
        _inputBuffer = GetComponent<InputBuffer>();
    }

    public virtual void ActivateInput()
    {
        _isControllerEnabled = true;
    }

    public virtual void DeactivateInput()
    {
        _isControllerEnabled = false;
    }
}
