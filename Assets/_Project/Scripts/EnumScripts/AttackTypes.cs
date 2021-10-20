using System;
using UnityEngine;

[Serializable]
public enum AttackTypeEnum { Overhead, Middling, Low, Throw };

public class AttackType : MonoBehaviour
{
	[SerializeField] private AttackTypeEnum _attackTypeEnum = default;

	public AttackTypeEnum AttackTypeEnum { get { return _attackTypeEnum; } private set { } }
}