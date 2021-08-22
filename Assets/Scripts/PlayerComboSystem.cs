using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
	[SerializeField] private AttackSO _2L = default;
	[SerializeField] private AttackSO _5L = default;
	[SerializeField] private AttackSO _6L = default;
	private PlayerMovement _playerMovement;


	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
	}

	public AttackSO GetComboAttack()
	{
		if (_playerMovement.IsCrouching)
		{
			return _2L;
		}
		else
		{
			if (_playerMovement.IsMoving)
			{
				return _6L;
			}
			else
			{
				return _5L;
			}
		}
	}
}
