using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CpuController))]
public class BrainController : MonoBehaviour
{
    private PlayerController _playerController;
    private CpuController _cpuController;
    private bool _cpuActive;

    public bool IsPlayerOne { get; set; }
    public string ControllerInputName { get; set; }
    public BaseController ActiveController { get; private set; }
    public InputDevice InputDevice { get; set; }


    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _cpuController = GetComponent<CpuController>();
    }

    public void SetControllerToPlayer(int index)
    {
        ActiveController = _playerController;
        _cpuActive = false;
        GameSimulation._players[index].isAi = false;
        _playerController.enabled = true;
        _cpuController.enabled = false;
        DeactivateCpu();
    }

    public void SetControllerToCpu(int index)
    {
        GetComponent<PlayerInput>().enabled = false;
        ActiveController = _cpuController;
        _cpuActive = true;
        GameSimulation._players[index].isAi = true;
        _playerController.enabled = false;
        _cpuController.enabled = true;
        ActivateCpu();
    }

    public void ActivateCpu()
    {
        if (_cpuActive)
        {
            _cpuController.ActivateInput();
        }
    }

    public void DeactivateCpu()
    {
        _cpuController.DeactivateInput();
    }

    public void ActivateInput()
    {
        ActivateCpu();
        ActiveController.ActivateInput();
    }

    public void DeactivateInput()
    {
        DeactivateCpu();
        ActiveController.DeactivateInput();
    }
}
