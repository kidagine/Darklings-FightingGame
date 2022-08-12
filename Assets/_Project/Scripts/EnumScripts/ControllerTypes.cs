using System;
using UnityEngine;

[Serializable]
public enum ControllerTypeEnum { Keyboard, Xbox, Controller, Switch, Cpu };

public class ContollerType : MonoBehaviour
{
	[SerializeField] private ControllerTypeEnum _controllerTypeEnum = default;

	public ControllerTypeEnum ControllerTypeEnum { get { return _controllerTypeEnum; } private set { } }
}
