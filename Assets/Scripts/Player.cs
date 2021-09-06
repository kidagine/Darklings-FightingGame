using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IHurtboxResponder, IHitboxResponder
{
	[SerializeField] private Transform _otherPlayer = default;
	[SerializeField] private PlayerStatsSO _playerStats = default;
	[SerializeField] private PlayerUI _playerUI = default;
	[SerializeField] private PlayerUI _otherPlayerUI = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private GameObject _pushbox = default;
	[SerializeField] private GameObject _hitEffect1 = default;
	[SerializeField] private bool _isPlayerOne = default;
	private PlayerMovement _playerMovement;
	private PlayerComboSystem _playerComboSystem;
	private PlayerController _playerController;
	private Audio _audio;
	private AttackSO _currentAttack;
	private Coroutine _stunCoroutine;
	private float _health;
	private int _lives = 2;
	private bool _isDead;

	public bool IsAttacking { get; set; }
	public bool IsPlayerOne { get { return _isPlayerOne; } private set { } }

	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerComboSystem = GetComponent<PlayerComboSystem>();
		_playerController = GetComponent<PlayerController>();
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
		if (!IsAttacking)
		{
			IsAttacking = true;
			_playerAnimator.Attack();
			_currentAttack = _playerComboSystem.GetComboAttack();
			Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, _currentAttack.hitEfffectRotation);
			Instantiate(_currentAttack.hitEffect, new Vector2(transform.position.x + _currentAttack.hitEffectPosition.x, transform.position.y + _currentAttack.hitEffectPosition.y), rotation, transform);
			if (!string.IsNullOrEmpty(_currentAttack.attackSound))
			{
				_audio.Sound(_currentAttack.attackSound).Play();
			}
			_playerMovement.TravelDistance(_currentAttack.travelDistance * transform.localScale.x);
		}
	}

	public void TakeDamage(int damage, float hitStun = 0, Vector2 knockbackDirection = default, float knockbackForce = 0)
	{
		_health--;
		Stun(hitStun);
		_playerUI.SetHealth(_health);
		_playerAnimator.Rebind();
		_playerAnimator.Hurt(true);
		_playerMovement.Knockback(knockbackDirection, knockbackForce);
		IsAttacking = false;
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

	public void Stun(float hitStun)
	{
		if (_stunCoroutine != null)
		{
			StopCoroutine(_stunCoroutine);
		}
		_stunCoroutine = StartCoroutine(StunCoroutine(hitStun));
	}

	IEnumerator StunCoroutine(float hitStun)
	{
		_playerMovement.SetLockMovement(true);
		_playerController.DeactivateInput();
		yield return new WaitForSeconds(hitStun);
		_playerController.ActivateInput();
		_playerMovement.SetLockMovement(false);
		_playerAnimator.Hurt(false);
		_otherPlayerUI.ResetCombo();
	}

	public void HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null)
	{
		_audio.Sound("Hit").Play();
		hurtbox.TakeDamage(1, _currentAttack.hitStun, new Vector2(transform.localScale.x, 0.0f), _currentAttack.knockback);
		_playerUI.IncreaseCombo();
	}
}
