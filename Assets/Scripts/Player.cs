using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IHurtboxResponder
{
	[SerializeField] private Slider _healthSlider = default;
	[SerializeField] private Transform _otherPlayer = default;
	[SerializeField] private PlayerStatsSO _playerStats = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	private PlayerMovement _playerMovement = default;

	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
	}

	void Start()
	{
		_healthSlider.maxValue = _playerStats.maxHealth;
		_healthSlider.value = _playerStats.currentHealth;
	}

	void Update()
	{
		if (_otherPlayer.position.x > transform.position.x)
		{
			transform.localScale = new Vector2(1.0f, transform.localScale.y);
		}
		else
		{
			transform.localScale = new Vector2(-1.0f, transform.localScale.y);
		}
	}

	public void AttackAction()
	{
		if (_playerMovement.IsGrounded)
		{
			_playerAnimator.Attack();
			_playerMovement.SetLockMovement(true);
		}
	}

	public void TakeDamage(int damage, Vector2 knockbackDirection = default, float knockbackForce = 0)
	{
		_playerStats.currentHealth--;
		_healthSlider.value = _playerStats.currentHealth;	
		_playerAnimator.Hurt();
	}
}
