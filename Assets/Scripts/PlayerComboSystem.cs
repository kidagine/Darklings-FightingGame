using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
	[SerializeField] private AttackSO _2L = default;
	[SerializeField] private AttackSO _5L = default;
	[SerializeField] private AttackSO _6L = default;
	[SerializeField] private AttackSO _jumpL = default;
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
			if (!_playerMovement.IsGrounded)
			{
				return _jumpL;
			}
			else if (_playerMovement.IsMoving)
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
