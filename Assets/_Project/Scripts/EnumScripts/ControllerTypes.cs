using System;
using UnityEngine;

[Serializable]
public enum ControllerTypeEnum { Keyboard, Xbox, Playstation, Switch, Controller, Cpu };

public class ControllerType : MonoBehaviour
{
	[SerializeField] private ControllerTypeEnum _controllerTypeEnum = default;

	public ControllerTypeEnum ControllerTypeEnum { get { return _controllerTypeEnum; } private set { } }

	public static ControllerTypeEnum ToControllerType(string controller)
	{
		if (controller.Contains("Keyboard"))
		{
			return ControllerTypeEnum.Keyboard;
		}
		if (controller.Contains("Xbox"))
		{
			return ControllerTypeEnum.Xbox;
		}
		if (controller.Contains("Controller"))
		{
			return ControllerTypeEnum.Playstation;
		}
		return ControllerTypeEnum.Xbox;
	}
}
