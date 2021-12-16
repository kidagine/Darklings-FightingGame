using Demonics.Sounds;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IHurtboxResponder, IHitboxResponder
{
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private Assist _assist = default;
	[SerializeField] private Pushbox _groundPushbox = default;
	[SerializeField] private Pushbox _airPushbox = default;
	[SerializeField] private GameObject _hurtbox = default;
	[SerializeField] private Transform _effectsParent = default;
	[SerializeField] private Transform _keepFlip = default;
	[SerializeField] private GameObject[] _playerIcons = default;
	private Transform _otherPlayer;
	private PlayerUI _playerUI;
	private PlayerUI _otherPlayerUI;
	private PlayerMovement _playerMovement;
	private PlayerComboSystem _playerComboSystem;
	private PlayerStats _playerStats;
	private BrainController _controller;
	private Audio _audio;
	private AttackSO _currentAttack;
	private Coroutine _stunCoroutine;
	private float _arcana;
	private float _assistGauge = 1.0f;
	private int _lives = 2;
	private bool _canAttack;

	public PlayerStatsSO PlayerStats { get { return _playerStats.PlayerStatsSO; } private set { } }
	public PlayerUI PlayerUI { get { return _playerUI; } private set { } }
	public float Health { get; private set; }
	public bool IsBlocking { get; private set; }
	public bool HitMiddair { get; set; }
	public bool IsAttacking { get; set; }
	public bool IsPlayerOne { get; set; }
	public float ArcaneSlowdown { get; set; } = 4.5f;
	public bool BlockingLow { get; set; }
	public bool BlockingHigh { get; set; }
	public bool BlockingMiddair { get; set; }
	public bool IsDead { get; set; }
	public bool CanFlip { get; set; } = true;

	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerComboSystem = GetComponent<PlayerComboSystem>();
		_playerStats = GetComponent<PlayerStats>();
		_audio = GetComponent<Audio>();
	}

	public void SetController()
	{
		_controller = GetComponent<BrainController>();
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
		_otherPlayerUI = otherPlayer.GetComponent<Player>().PlayerUI;
	}

	public void ResetPlayer()
	{
		IsDead = false;
		IsAttacking = false;
		_controller.ActiveController.enabled = true;
		_controller.ActivateInput();
		_effectsParent.gameObject.SetActive(true);
		_playerAnimator.Rebind();
		SetGroundPushBox(true);
		SetAirPushBox(false);
		SetPushboxTrigger(false);
		SetHurtbox(true);
		_assistGauge = 1.0f;
		if (!GameManager.Instance.InfiniteArcana)
		{
			_arcana = 0.0f;
		}
		_playerMovement.ResetPlayerMovement();
		_playerUI.SetArcana(_arcana);
		_playerUI.SetAssist(_assistGauge);
		InitializeStats();
		_playerUI.ShowPlayerIcon();
	}

	public void ResetLives()
	{
		_lives = 2;
		_playerUI.ResetLives();
	}

	public void MaxArcanaStats()
	{
		_arcana = _playerStats.PlayerStatsSO.maxArcana;
		_playerUI.SetArcana(_arcana);
	}

	public void MaxHealthStats()
	{
		Health = _playerStats.PlayerStatsSO.maxHealth;
		_playerUI.SetHealth(Health);
	}

	private void InitializeStats()
	{
		_playerUI.InitializeUI(_playerStats.PlayerStatsSO, _controller, _playerIcons);
		Health = _playerStats.PlayerStatsSO.maxHealth;
		_playerUI.SetHealth(Health);
	}

	void Update()
	{
		ArcanaCharge();
		AssistCharge();
		CheckFlip();
	}

	private void AssistCharge()
	{
		if (_assistGauge < 1.0f && GameManager.Instance.HasGameStarted)
		{
			_assistGauge += Time.deltaTime / (5.0f - _assist.AssistStats.assistRecharge);
			_playerUI.SetAssist(_assistGauge);
		}
	}

	private void ArcanaCharge()
	{
		if (_arcana < _playerStats.PlayerStatsSO.maxArcana && GameManager.Instance.HasGameStarted)
		{
			_arcana += Time.deltaTime / (ArcaneSlowdown - _playerStats.PlayerStatsSO.arcanaRecharge);
			_playerUI.SetArcana(_arcana);
		}
	}

	private void CheckFlip()
	{
		if (!IsDead && CanFlip)
		{
			if (_otherPlayer.position.x > transform.position.x && transform.position.x < 9.2f && !IsAttacking && transform.localScale.x != 1.0f)
			{
				_playerAnimator.IsRunning(false);
				transform.localScale = new Vector2(1.0f, transform.localScale.y);
				_keepFlip.localScale = new Vector2(1.0f, transform.localScale.y);
			}
			else if (_otherPlayer.position.x < transform.position.x && transform.position.x > -9.2f && !IsAttacking && transform.localScale.x != -1.0f)
			{
				_playerAnimator.IsRunning(false);
				transform.localScale = new Vector2(-1.0f, transform.localScale.y);
				_keepFlip.localScale = new Vector2(-1.0f, transform.localScale.y);
			}
			CheckIsBlocking();
		}
	}

	public bool ArcaneAction()
	{
		//REPLACE
		if (_arcana >= 1.0f)
		{
			if (!IsAttacking && !IsBlocking && !_playerMovement.IsDashing)
			{
				if (_playerComboSystem.GetArcana().airOk || _playerMovement.IsGrounded)
				{
					_playerMovement.ResetToWalkSpeed();
					if (!GameManager.Instance.InfiniteArcana)
					{
						_arcana--;
					}
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
					return true;
				}
			}
		}
		return false;
		//REPLACE
	}

	public bool AttackAction()
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
			return true;
		}
		return false;
	}

	public bool AssistAction()
	{
		if (_assistGauge >= 1.0f)
		{
			_assist.Attack();
			_assistGauge--;
			_playerUI.SetAssist(_assistGauge);
			return true;
		}
		return false;
	}

	public void HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null)
	{
		_canAttack = true;
		_currentAttack.hurtEffectPosition = hit.point;
		bool gotHit = hurtbox.TakeDamage(_currentAttack);
		if (_currentAttack.selfKnockback > 0.0f)
		{
			_playerMovement.SetLockMovement(true);
		}
		if (!gotHit)
		{
			_playerMovement.Knockback(new Vector2(-transform.localScale.x, 0.0f), _currentAttack.selfKnockback, _currentAttack.knockbackDuration);
		}
		else
		{
			_playerMovement.Knockback(new Vector2(-transform.localScale.x, 0.0f), _currentAttack.selfKnockback / 2, _currentAttack.knockbackDuration);
		}
	}

	public void CreateEffect(bool isProjectile = false)
	{
		if (_currentAttack.hitEffect != null)
		{
			GameObject hitEffect;
			hitEffect = Instantiate(_currentAttack.hitEffect, _effectsParent);
			hitEffect.transform.localPosition = _currentAttack.hitEffectPosition;
			hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, _currentAttack.hitEffectRotation);
			if (isProjectile)
			{
				hitEffect.transform.SetParent(null);
				hitEffect.GetComponent<MoveInDirection>().Direction = new Vector2(transform.localScale.x, 0.0f);
				hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetHitboxResponder(transform);
			}
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
		GameObject effect = Instantiate(attackSO.hurtEffect);
		effect.transform.localPosition = attackSO.hurtEffectPosition;
		if (!BlockingLow && !BlockingHigh && !BlockingMiddair || BlockingLow && attackSO.attackTypeEnum == AttackTypeEnum.Overhead || BlockingHigh && attackSO.attackTypeEnum == AttackTypeEnum.Low || attackSO.attackTypeEnum == AttackTypeEnum.Throw)
		{
			if (IsAttacking)
			{
				_otherPlayerUI.DisplayNotification("Punish");
			}
			_audio.Sound(attackSO.impactSound).Play();
			if (!GameManager.Instance.InfiniteHealth)
			{
				Health--;
			}
			GameManager.Instance.HitStop();
			_playerMovement.StopDash();
			_otherPlayerUI.IncreaseCombo();
			Stun(attackSO.hitStun);
			_playerUI.SetHealth(Health);
			if (HitMiddair)
			{
				_playerMovement.Knockback(new Vector2(_otherPlayer.transform.localScale.x, 0.0f), 7.0f, attackSO.knockbackDuration);
			}
			else
			{
				_playerMovement.Knockback(new Vector2(_otherPlayer.transform.localScale.x, attackSO.knockbackDirection.y), attackSO.knockback, attackSO.knockbackDuration);
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
		_controller.ActiveController.enabled = false;
		SetGroundPushBox(false);
		SetHurtbox(false);
		if (!IsDead)
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
		IsDead = true;
	}

	public void Taunt()
	{
		_playerAnimator.Taunt();
		_playerMovement.SetLockMovement(true);
		_controller.ActiveController.enabled = false;
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
		StopStun();
		_stunCoroutine = StartCoroutine(StunCoroutine(hitStun));
	}

	public void StopStun()
	{
		if (_stunCoroutine != null)
		{
			StopCoroutine(_stunCoroutine);
		}
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
		_controller.DeactivateInput();
		yield return new WaitForSeconds(hitStun);
		if (!HitMiddair)
		{
			_controller.ActivateInput();
			_playerMovement.SetLockMovement(false);
			_playerAnimator.IsHurt(false);
		}
		_otherPlayerUI.ResetCombo();
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

	public void HitboxCollidedGround(RaycastHit2D hit)
	{
		throw new System.NotImplementedException();
	}
}
