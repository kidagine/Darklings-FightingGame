using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IHurtboxResponder, IHitboxResponder
{
	[SerializeField] private PlayerStatsSO _playerStats = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private Pushbox _groundPushbox = default;
	[SerializeField] private Pushbox _airPushbox = default;
	[SerializeField] private GameObject _hurtbox = default;
	[SerializeField] private Transform _effectsParent = default;
	private Transform _otherPlayer;
	private PlayerUI _playerUI;
	private PlayerMovement _playerMovement;
	private PlayerComboSystem _playerComboSystem;
	private BaseController _playerController;
	private Audio _audio;
	private AttackSO _currentAttack;
	private Coroutine _stunCoroutine;
	private int _lives = 2;
	private bool _isDead;

	public float Health { get; private set; }
	public bool IsBlocking { get; private set; }
	public bool IsAttacking { get; set; }
	public bool IsPlayerOne { get; set; }

	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerComboSystem = GetComponent<PlayerComboSystem>();
		_playerController = GetComponent<BaseController>();
		_audio = GetComponent<Audio>();
	}

	void Start()
	{
		InitializeStats();
	}

	public void SetPlayerUI(PlayerUI playerUI)
	{
		_playerUI = playerUI;
	}

	public void SetOtherPlayer(Transform otherPlayer)
	{
		_otherPlayer = otherPlayer;
	}

	public void ResetPlayer()
	{
		_isDead = false;
		IsAttacking = false;
		_playerController.enabled = true;
		_playerMovement.IsGrounded = true;
		_effectsParent.gameObject.SetActive(true);
		_playerMovement.SetLockMovement(false);
		_playerAnimator.Rebind();
		SetGroundPushBox(true);
		SetHurtbox(true);
		InitializeStats();
	}

	public void ResetLives()
	{
		_lives = 2;
		_playerUI.ResetLives();
	}

	private void InitializeStats()
	{
		_playerUI.InitializeUI(_playerStats, IsPlayerOne);
		Health = _playerStats.currentHealth;
		_playerUI.SetHealth(Health);
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
		if (!IsAttacking && !IsBlocking)
		{
			_audio.Sound("Hit").Play();
			IsAttacking = true;
			_playerAnimator.Attack();
			_currentAttack = _playerComboSystem.GetComboAttack();

			if (!string.IsNullOrEmpty(_currentAttack.attackSound))
			{
				_audio.Sound(_currentAttack.attackSound).Play();
			}
			if (!_currentAttack.isAirAttack)
			{
				_playerMovement.TravelDistance(_currentAttack.travelDistance * transform.localScale.x);
			}
		}
	}

	public void CreateEffect()
	{
		GameObject hitEffect = Instantiate(_currentAttack.hitEffect, _effectsParent);
		hitEffect.transform.localPosition = _currentAttack.hitEffectPosition;
		hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, _currentAttack.hitEffectRotation);
	}

	public bool TakeDamage(AttackSO attackSO)
	{
		DestroyEffects();
		_playerAnimator.IsHurt(true);
		Instantiate(attackSO.hurtEffect, attackSO.hurtEffectPosition, Quaternion.identity);
		if (CheckIsBlocking() && !attackSO.canGuardBreak)
		{
			_audio.Sound("Block").Play();
			IsBlocking = true;
			_playerAnimator.IsBlocking(true);
			_playerMovement.SetLockMovement(true);
			StartCoroutine(ResetBlockingCoroutine());
			return false;
		}
		else
		{
			_audio.Sound(attackSO.impactSound).Play();
			Health--;
			//_otherPlayerUI.IncreaseCombo();
			Stun(attackSO.hitStun);
			_playerUI.SetHealth(Health);
			_playerMovement.Knockback(new Vector2(-transform.localScale.x, 0.0f), attackSO.knockback);
			IsAttacking = false;
			if (Health <= 0)
			{
				Die();
			}
			return true;
		}
	}

	IEnumerator ResetBlockingCoroutine()
	{
		yield return new WaitForSeconds(0.25f);
		IsBlocking = false;
		_playerMovement.SetLockMovement(false);
		_playerAnimator.IsHurt(false);
		_playerAnimator.IsBlocking(false);
	}

	private bool CheckIsBlocking()
	{
		if (_playerMovement.IsGrounded && !IsAttacking)
		{
			if (transform.localScale.x == 1.0f && _playerMovement.MovementInput.x < 0.0f || transform.localScale.x == -1.0f && _playerMovement.MovementInput.x > 0.0f)
			{
				return true;
			}
		}
		return false;
	}

	private void Die()
	{
		DestroyEffects();
		_playerAnimator.Death();
		_playerController.enabled = false;
		SetGroundPushBox(false);
		SetHurtbox(false);
		if (!_isDead)
		{
			if (GameManager.Instance.HasGameStarted && !GameManager.Instance.IsTrainingMode)
			{
				_lives--;
			}
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

	public void LoseLife()
	{
		_playerUI.SetLives();
	}

	public void SetPushboxTrigger(bool state)
	{
		_groundPushbox.SetIsTrigger(state);
	}

	public void SetGroundPushBox(bool state)
	{
		_groundPushbox.gameObject.SetActive(state);
	}

	public void SetAirPushBox(bool state)
	{
		_airPushbox.gameObject.SetActive(state);
	}

	public void SetHurtbox(bool state)
	{
		_hurtbox.gameObject.SetActive(state);
	}

	public void Stun(float hitStun)
	{
		if (_stunCoroutine != null)
		{
			StopCoroutine(_stunCoroutine);
		}
		_stunCoroutine = StartCoroutine(StunCoroutine(hitStun));
	}

	private void DestroyEffects()
	{
		foreach (Transform effect in _effectsParent)
		{
			Destroy(effect.gameObject);
		}
	}

	IEnumerator StunCoroutine(float hitStun)
	{
		_playerMovement.SetLockMovement(true);
		_playerController.DeactivateInput();
		yield return new WaitForSeconds(hitStun);
		_playerController.ActivateInput();
		_playerMovement.SetLockMovement(false);
		_playerAnimator.IsHurt(false);
		//_otherPlayerUI.ResetCombo();
	}

	public void HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null)
	{
		_currentAttack.hurtEffectPosition = hit.point;
		bool gotHit = hurtbox.TakeDamage(_currentAttack);
		if (!gotHit)
		{
			_playerMovement.SetLockMovement(true);
			_playerMovement.Knockback(new Vector2(-transform.localScale.x, 0.0f), _currentAttack.selfKnockback);
		}
		else
		{
			_playerMovement.SetLockMovement(true);
			_playerMovement.Knockback(new Vector2(-transform.localScale.x, 0.0f), _currentAttack.selfKnockback / 2);
		}
	}
}
