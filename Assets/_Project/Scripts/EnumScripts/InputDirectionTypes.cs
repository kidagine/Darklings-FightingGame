using System;
using UnityEngine;

[Flags]
[Serializable]
public enum InputDirectionEnum { None, Up, Down, Left, Right };


public class InputDirectionTypes : MonoBehaviour
{
	[SerializeField] private InputDirectionEnum _inputDirectionEnum = default;

	public InputDirectionEnum InputDirectionEnum { get { return _inputDirectionEnum; } private set { } }
}
