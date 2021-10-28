using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected bool _trainingController;
    protected Player _player;
    protected PlayerMovement _playerMovement;
    protected bool _isControllerEnabled = true;

	public bool IsPlayerOne { get; set; }
	public string ControllerInputName { get; set; }


	void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
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
