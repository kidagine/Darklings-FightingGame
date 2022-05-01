using UnityEngine;

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


	void Awake()
	{
		_playerController = GetComponent<PlayerController>();
		_cpuController = GetComponent<CpuController>();
	}

	public void SetControllerToPlayer()
	{
		ActiveController = _playerController;
		_cpuActive = false;
		_playerController.enabled = true;
		_cpuController.enabled = false;
		DeactivateCpu();
	}

	public void SetControllerToCpu()
	{
		ActiveController = _cpuController;
		_cpuActive = true;
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
