using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
	private PlayerStats _playerStats;
	private PlayerMovement _playerMovement;
	private PlayerController _playerController;

	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerStats = GetComponent<PlayerStats>();
		_playerController = GetComponent<PlayerController>();
	}

	public AttackSO GetComboAttack(InputEnum inputEnum)
	{
		if (_playerController.InputDirection.y < 0 && _playerMovement.IsGrounded)
		{
			return GetCrouchingAttackType(inputEnum);
		}
		else
		{
			if (!_playerMovement.IsGrounded)
			{
				return _playerStats.PlayerStatsSO.jumpL;
			}
			else
			{
				return GetStandingAttackType(inputEnum);
			}
		}
	}

	private AttackSO GetCrouchingAttackType(InputEnum inputEnum)
	{
		if (inputEnum == InputEnum.Light)
		{
			return _playerStats.PlayerStatsSO.m2L;
		}
		else if (inputEnum == InputEnum.Medium)
		{
			return _playerStats.PlayerStatsSO.m2M;
		}
		else
		{
			return _playerStats.PlayerStatsSO.m2H;
		}
	}

	private AttackSO GetStandingAttackType(InputEnum inputEnum)
	{
		if (inputEnum == InputEnum.Light)
		{
			return _playerStats.PlayerStatsSO.m5L;
		}
		else if (inputEnum == InputEnum.Medium)
		{
			return _playerStats.PlayerStatsSO.m5M;
		}
		else
		{
			return _playerStats.PlayerStatsSO.m5H;
		}
	}

	public ArcanaSO GetArcana()
	{
		return _playerStats.PlayerStatsSO.arcana;
	}
}
