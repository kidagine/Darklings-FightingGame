using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] protected bool _trainingController = default;
    protected Player _player;
    protected PlayerMovement _playerMovement;
    protected string _controllerInputName;
    protected bool _isControllerEnabled = true;

    void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        _controllerInputName = _player.IsPlayerOne is false ? SceneSettings.ControllerOne : SceneSettings.ControllerTwo;
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
