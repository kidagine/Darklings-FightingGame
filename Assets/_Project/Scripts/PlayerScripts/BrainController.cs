using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CpuController))]
public class BrainController : MonoBehaviour
{
    private PlayerController _playerController;
    private CpuController _cpuController;

    public bool IsPlayerOne { get; set; }
    public string ControllerInputName { get; set; }
	public BaseController ActiveController { get; private set; }


	void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _cpuController = GetComponent<CpuController>();
    }

    public void SetControllerToPlayer()
    {
        ActiveController = _playerController;
        _playerController.enabled = true;
        _cpuController.enabled = false;
    }

    public void SetControllerToCpu()
    {
        ActiveController = _cpuController;
        _playerController.enabled = false;
        _cpuController.enabled = true;
    }

    public void ActivateInput()
    {
        ActiveController.ActivateInput();
    }

    public void DeactivateInput()
    {
        ActiveController.DeactivateInput();
    }
}
