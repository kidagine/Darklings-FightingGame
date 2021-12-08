using System;
using UnityEngine;


[Serializable]
public enum InputEnum { Up, Down, Left, Right, Light, Special };


public class InputTypes : MonoBehaviour
{
	[SerializeField] private InputEnum _inputEnum = default;

	public InputEnum InputEnum { get { return _inputEnum; } private set { } }
}
