using UnityEngine;

public class Player : MonoBehaviour, IHurtboxResponder
{
	[SerializeField] private Transform _otherPlayer = default;
	[SerializeField] private PlayerStatsSO _playerStats = default;
	[SerializeField] private PlayerUI _playerUI = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private GameObject _pushbox = default;
	[SerializeField] private bool _isPlayerOne = default;
	private PlayerMovement _playerMovement;
	private PlayerComboSystem _playerComboSystem;
	private Audio _audio;
	private float _health;
	private int _lives = 2;
	private bool _isDead;

	public bool IsAttacking { get; set; }
	public bool IsPlayerOne { get { return _isPlayerOne; } private set { } }

	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerComboSystem = GetComponent<PlayerComboSystem>();
		_audio = GetComponent<Audio>();
	}

	void Start()
	{
		InitializeStats();
	}

	public void ResetPlayer()
	{
		_isDead = false;
		IsAttacking = false;
		_playerAnimator.Rebind();
		SetPushbox(true);
		InitializeStats();
	}

	public void ResetLives()
	{
		_lives = 2;
		_playerUI.ResetLives();
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
		if (_playerMovement.IsGrounded && !IsAttacking)
		{
			IsAttacking = true;
			_playerAnimator.Attack();
			AttackSO attack = _playerComboSystem.GetComboAttack();
			if (!string.IsNullOrEmpty(attack.attackSound))
			{
				_audio.Sound(attack.attackSound).Play();
			}
			_playerMovement.TravelDistance(attack.travelDistance * transform.localScale.x);
			//_playerMovement.SetLockMovement(true);
		}
	}

	public void TakeDamage(int damage, Vector2 knockbackDirection = default, float knockbackForce = 0)
	{
		_health--;
		_playerUI.SetHealth(_health);
		_playerAnimator.Hurt();
		if (_health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		_playerAnimator.Death();
		SetPushbox(false);
		if (!_isDead)
		{
			GameManager.Instance.PlayerOneWon = _isPlayerOne is true ? true : false;
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

	private void SetPushbox(bool state)
	{
		_pushbox.SetActive(state);
	}
}
