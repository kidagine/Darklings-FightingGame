using System;
using UnityEngine;

[Flags]
[Serializable]
public enum InputEnum { Direction = 1, Light = 2, Special = 4, Assist = 8 };


public class InputTypes : MonoBehaviour
{
	[SerializeField] private InputEnum _inputEnum = default;

	public InputEnum InputEnum { get { return _inputEnum; } private set { } }
}
