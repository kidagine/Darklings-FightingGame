using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
	private PlayerStats _playerStats;
	private PlayerMovement _playerMovement;


	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerStats = GetComponent<PlayerStats>();
	}

	public AttackSO GetComboAttack()
	{
		if (_playerMovement.IsCrouching && _playerMovement.IsGrounded)
		{
			return _playerStats.PlayerStatsSO.m2L;
		}
		else
		{
			if (!_playerMovement.IsGrounded)
			{
				return _playerStats.PlayerStatsSO.jumpL;
			}
			else if (_playerMovement.IsMoving)
			{
				if (_playerMovement.MovementInput.x * transform.localScale.x > 0.0f)
				{
					return _playerStats.PlayerStatsSO.m6L;
				}
				else
				{
					return _playerStats.PlayerStatsSO.m4L;
				}
			}
			else
			{
				return _playerStats.PlayerStatsSO.m5L;
			}
		}
	}

	public ArcanaSO GetArcana()
	{
		return _playerStats.PlayerStatsSO.arcana;
	}
}
