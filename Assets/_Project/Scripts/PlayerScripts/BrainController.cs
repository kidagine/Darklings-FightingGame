using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CpuController))]
public class BrainController : MonoBehaviour
{
	private PlayerController _playerController;
	private CpuController _cpuController;
	private bool cpuActive;

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
		cpuActive = false;
		_playerController.enabled = true;
		_cpuController.enabled = false;
		DeactivateCpu();
	}

	public void SetControllerToCpu()
	{
		ActiveController = _cpuController;
		cpuActive = true;
		_playerController.enabled = false;
		_cpuController.enabled = true;
		ActivateCpu();
	}

	public void ActivateCpu()
	{
		if (cpuActive)
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
		ActiveController.ActivateInput();
	}

	public void DeactivateInput()
	{
		ActiveController.DeactivateInput();
	}
}
