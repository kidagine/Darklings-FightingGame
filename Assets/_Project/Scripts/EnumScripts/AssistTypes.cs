using System;
using UnityEngine;

[Serializable]
public enum AssistTypeEnum { A, B };

public class AssistType : MonoBehaviour
{
	[SerializeField] private AssistTypeEnum _assistTypeEnum = default;

	public AssistTypeEnum AssistTypeEnum { get { return _assistTypeEnum; } private set { } }
}