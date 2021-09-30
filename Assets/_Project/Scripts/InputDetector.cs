using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDetector : MonoBehaviour
{
	public void DeviceRegained(PlayerInput playerInput)
	{
		Debug.Log("Regained: ");
		for (int i = 0; i < playerInput.devices.Count; i++)
		{
			Debug.Log("Regained: "+ playerInput.devices[i].name);
		}
	}

	public void DeviceLost(PlayerInput playerInput)
	{
		Debug.Log("Lost: ");
		for (int i = 0; i < playerInput.devices.Count; i++)
		{
			Debug.Log("Lost: " + playerInput.devices[i].name);
		}
	}
}
