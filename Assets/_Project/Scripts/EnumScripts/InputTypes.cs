using System;
using UnityEngine;

[Flags]
[Serializable]
public enum InputEnum { Up = 1, Down = 2, Left = 4, Right = 8, Light = 16, Special = 32 };


public class InputTypes : MonoBehaviour
{
	[SerializeField] private InputEnum _inputEnum = default;

	public InputEnum InputEnum { get { return _inputEnum; } private set { } }
}
