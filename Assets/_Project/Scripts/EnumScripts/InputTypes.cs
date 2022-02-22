using System;
using UnityEngine;

[Flags]
[Serializable]
public enum InputEnum { Direction = 1, Light = 2, Medium = 4, Heavy = 8, Special = 12, Assist = 16 };


public class InputTypes : MonoBehaviour
{
	[SerializeField] private InputEnum _inputEnum = default;

	public InputEnum InputEnum { get { return _inputEnum; } private set { } }
}
