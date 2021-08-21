using UnityEngine;

public class Player : MonoBehaviour, IHurtboxResponder
{
	[SerializeField] private Transform _otherPlayer = default;
	[SerializeField] private PlayerStatsSO _playerStats = default;
	[SerializeField] private PlayerUI _playerUI = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private GameObject _pushbox = default;
	private PlayerMovement _playerMovement;
	private float _health;
	private int _lives = 2;
	private bool _isDead;


	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
	}

	void Start()
	{
		InitializeStats();
	}

	public void ResetPlayer()
	{
		_isDead = false;
		_playerAnimator.Rebind();
		InitializeStats();
	}

	private void InitializeStats()
	{
		_health = _playerStats.currentHealth;
		_playerUI.SetMaxHealth(_playerStats.maxHealth);
		_playerUI.SetHealth(_health);
	}

	void Update()
	{
		if (!_isDead)
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
		_health--;
		_playerUI.SetHealth(_health);
		_playerAnimator.Hurt();
		if (_health <= 0)
		{
			_playerAnimator.Death();
			SetPushbox(false);
			if (!_isDead)
			{
				_lives--;
				_playerUI.SetLives(_lives);
				if (_lives <= 0)
				{
					GameManager.Instance.MatchOver();
				}
				else
				{
					GameManager.Instance.RoundOver();
				}
			}
			_isDead = true;
		}
	}

	private void SetPushbox(bool state)
	{
		_pushbox.SetActive(state);
	}
}
