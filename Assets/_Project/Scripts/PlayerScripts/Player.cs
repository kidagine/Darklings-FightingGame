using Demonics.Sounds;
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
	private float _arcana;
	private int _lives = 2;
	private bool _isDead;

	public PlayerStatsSO PlayerStats { get { return _playerStats; } private set { } }
	public float Health { get; private set; }
	public bool IsBlocking { get; private set; }
	public bool HitMiddair { get; set; }
	public bool IsAttacking { get; set; }
	public bool IsPlayerOne { get; set; }
	public float ArcaneSlowdown { get; set; } = 4.5f;
	public bool BlockingLow { get; set; }
	public bool BlockingHigh { get; set; }
	public bool BlockingMiddair { get; set; }

	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerComboSystem = GetComponent<PlayerComboSystem>();
		_audio = GetComponent<Audio>();
	}

	public void SetController()
	{
		_playerController = GetComponent<BaseController>();
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
		_arcana = 0.0f;
		_playerUI.SetArcana(_arcana);
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
		Health = _playerStats.maxHealth;
		_playerUI.SetHealth(Health);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			Taunt();
		}
		if (_arcana < _playerStats.maxArcana && GameManager.Instance.HasGameStarted)
		{
			_arcana += Time.deltaTime / (ArcaneSlowdown - _playerStats.arcanaRecharge);
			_playerUI.SetArcana(_arcana);
		}

		if (!_isDead)
		{
			if (_otherPlayer.position.x > transform.position.x && transform.position.x < 9.2f && !IsAttacking && transform.localScale.x != 1.0f)
			{
				_playerAnimator.IsRunning(false);
				transform.localScale = new Vector2(1.0f, transform.localScale.y);
			}
			else if (_otherPlayer.position.x < transform.position.x && transform.position.x > -9.2f && !IsAttacking && transform.localScale.x != -1.0f)
			{
				_playerAnimator.IsRunning(false);
				transform.localScale = new Vector2(-1.0f, transform.localScale.y);
			}
			CheckIsBlocking();
		}
	}

	public void ArcaneAction()
	{

		//REPLACE
		if (_arcana >= 1.0f)
		{
			if (!IsAttacking && !IsBlocking && !_playerMovement.IsDashing)
			{
				if (_playerComboSystem.GetArcana().airOk || _playerMovement.IsGrounded)
				{
					_playerMovement.ResetToWalkSpeed();
					_arcana -= 1.0f;
					_playerUI.DecreaseArcana();
					_playerUI.SetArcana(_arcana);
					_audio.Sound("Hit").Play();
					IsAttacking = true;
					_playerAnimator.Arcana();
					_currentAttack = _playerComboSystem.GetArcana();

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
		}
		//REPLACE
	}

	public void AttackAction()
	{
		if (!IsAttacking && !IsBlocking && !_playerMovement.IsDashing)
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
		if (_currentAttack.hitEffect != null)
		{
			GameObject hitEffect = Instantiate(_currentAttack.hitEffect, _effectsParent);
			hitEffect.transform.localPosition = _currentAttack.hitEffectPosition;
			hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, _currentAttack.hitEffectRotation);
		}
	}

	public bool TakeDamage(AttackSO attackSO)
	{
		DestroyEffects();
		if (!_playerMovement.IsGrounded)
		{
			HitMiddair = true;
		}
		_playerAnimator.IsHurt(true);
		Instantiate(attackSO.hurtEffect, attackSO.hurtEffectPosition, Quaternion.identity);
		if (!BlockingLow && !BlockingHigh && !BlockingMiddair || BlockingLow && attackSO.attackTypeEnum == AttackTypeEnum.Overhead || BlockingHigh && attackSO.attackTypeEnum == AttackTypeEnum.Low || attackSO.attackTypeEnum == AttackTypeEnum.Throw)
		{
			_audio.Sound(attackSO.impactSound).Play();
			Health--;
			_playerMovement.StopDash();
			//_otherPlayerUI.IncreaseCombo();
			Stun(attackSO.hitStun);
			_playerUI.SetHealth(Health);
			if (HitMiddair)
			{
				_playerMovement.Knockback(new Vector2(-transform.localScale.x, 0.0f), 7.0f);
			}
			else
			{
				_playerMovement.Knockback(new Vector2(-transform.localScale.x, attackSO.knockbackDirection.y), attackSO.knockback);
			}
			IsAttacking = false;
			if (Health <= 0)
			{
				Die();
			}
			return true;
		}
		else
		{
			_audio.Sound("Block").Play();
			IsBlocking = true;
			if (!BlockingMiddair)
			{
				if (BlockingLow)
				{
					_playerAnimator.IsBlockingLow(true);
				}
				else
				{
					_playerAnimator.IsBlocking(true);
				}
			}
			else
			{
				_playerAnimator.IsBlockingAir(true);
			}
			_playerMovement.SetLockMovement(true);
			StartCoroutine(ResetBlockingCoroutine());
			return false;
		}
	}

	IEnumerator ResetBlockingCoroutine()
	{
		yield return new WaitForSeconds(0.25f);
		IsBlocking = false;
		_playerMovement.SetLockMovement(false);
		_playerAnimator.IsHurt(false);
		_playerAnimator.IsBlocking(false);
		_playerAnimator.IsBlockingLow(false);
		_playerAnimator.IsBlockingAir(false);
	}

	private void CheckIsBlocking()
	{
		if (!IsAttacking && !_playerMovement.IsDashing)
		{
			if (transform.localScale.x == 1.0f && _playerMovement.MovementInput.x < 0.0f || transform.localScale.x == -1.0f && _playerMovement.MovementInput.x > 0.0f)
			{
				if (_playerMovement.IsGrounded)
				{
					if (_playerMovement.MovementInput.y < 0.0f)
					{
						BlockingLow = true;
						BlockingHigh = false;
						BlockingMiddair = false;
					}
					else
					{
						BlockingLow = false;
						BlockingHigh = true;
						BlockingMiddair = false;
					}
				}
				else
				{
					BlockingLow = false;
					BlockingHigh = false;
					BlockingMiddair = true;
				}
	
			}
			else
			{
				BlockingLow = false;
				BlockingHigh = false;
				BlockingMiddair = false;
			}
		}
		else
		{
			BlockingLow = false;
			BlockingHigh = false;
			BlockingMiddair = false;
		}
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

	public void Taunt()
	{
		_playerAnimator.Taunt();
		_playerMovement.SetLockMovement(true);
		_playerController.enabled = false;
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
		if (!HitMiddair)
		{
			_playerController.ActivateInput();
			_playerMovement.SetLockMovement(false);
			_playerAnimator.IsHurt(false);
		}
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

	public void Pause(bool isPlayerOne)
	{
		if (GameManager.Instance.IsTrainingMode)
		{
			_playerUI.OpenTrainingPause(isPlayerOne);
		}
		else
		{
			_playerUI.OpenPauseHold(isPlayerOne);
		}
	}

	public void UnPause()
	{
		if (!GameManager.Instance.IsTrainingMode)
		{
			_playerUI.ClosePauseHold();
		}
	}
}
