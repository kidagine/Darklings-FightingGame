using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
	private PlayerStats _playerStats;

	void Awake()
	{
		_playerStats = GetComponent<PlayerStats>();
	}

	public AttackSO GetComboAttack(InputEnum inputEnum, bool isCrouching, bool isAir)
	{
		if (inputEnum == InputEnum.Throw)
		{
			return _playerStats.PlayerStatsSO.mThrow;
		}
		if (isCrouching)
		{
			return GetCrouchingAttackType(inputEnum);
		}
		else
		{
			if (isAir)
			{
				return GetJumpAttackType(inputEnum);
			}
			else
			{
				return GetStandingAttackType(inputEnum);
			}
		}
	}

	private AttackSO GetJumpAttackType(InputEnum inputEnum)
	{
		if (inputEnum == InputEnum.Light)
		{
			return _playerStats.PlayerStatsSO.jL;
		}
		else if (inputEnum == InputEnum.Medium)
		{
			return _playerStats.PlayerStatsSO.jM;
		}
		else
		{
			return _playerStats.PlayerStatsSO.jM;
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

	public AttackSO GetThrow()
	{
		return _playerStats.PlayerStatsSO.mThrow;
	}

	public ArcanaSO GetArcana(bool isCrouching = false, bool isAir = false)
	{
		if (isAir)
		{
			return _playerStats.PlayerStatsSO.jArcana;
		}
		if (isCrouching)
		{
			return _playerStats.PlayerStatsSO.m2Arcana;
		}
		return _playerStats.PlayerStatsSO.m5Arcana;
	}
}
