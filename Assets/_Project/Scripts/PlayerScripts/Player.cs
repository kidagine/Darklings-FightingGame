using Demonics.Sounds;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IHurtboxResponder, IHitboxResponder
{
	[SerializeField] private PlayerStateManager _playerStateManager = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private Assist _assist = default;
	[SerializeField] private Pushbox _groundPushbox = default;
	[SerializeField] private Pushbox _airPushbox = default;
	[SerializeField] private GameObject _hurtbox = default;
	[SerializeField] private GameObject _blockEffectPrefab = default;
	[SerializeField] private GameObject _shadowbreakPrefab = default;
	[SerializeField] protected Transform _effectsParent = default;
	[SerializeField] private Transform _grabPoint = default;
	[SerializeField] private Transform _keepFlip = default;
	[SerializeField] private InputBuffer _inputBuffer = default;
	[SerializeField] private GameObject[] _playerIcons = default;
	protected PlayerUI _playerUI;
	private PlayerMovement _playerMovement;
	protected PlayerComboSystem _playerComboSystem;
	private PlayerStats _playerStats;
	private BrainController _controller;
	private Audio _audio;
	private bool _throwBreakInvulnerable;
	public PlayerStateManager PlayerStateManager { get { return _playerStateManager; } private set { } }
	public PlayerStateManager OtherPlayerStateManager { get; private set; }
	public Player OtherPlayer { get; private set; }
	public PlayerMovement OtherPlayerMovement { get; private set; }
	public PlayerUI OtherPlayerUI { get; private set; }
	public PlayerStatsSO PlayerStats { get { return _playerStats.PlayerStatsSO; } private set { } }
	public PlayerUI PlayerUI { get { return _playerUI; } private set { } }
	public AttackSO CurrentAttack { get; set; }
	public float Health { get; set; }
	public int Lives { get; set; } = 2;
	public bool IsBlocking { get; private set; }
	public bool IsKnockedDown { get; private set; }
	public bool IsAttacking { get; set; }
	public bool IsPlayerOne { get; set; }
	public float AssistGauge { get; set; } = 1.0F;
	public float Arcana { get; set; }
	public float ArcaneSlowdown { get; set; } = 7.5f;
	public bool IsStunned { get; private set; }
	public bool BlockingLow { get; set; }
	public bool BlockingHigh { get; set; }
	public bool BlockingMiddair { get; set; }
	public bool IsDead { get; set; }
	public bool CanShadowbreak { get; set; } = true;
	public bool CanCancelAttack { get; set; }

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

	public void SetAssist(AssistStatsSO assistStats)
	{
		_assist.SetAssist(assistStats);
		_playerUI.SetAssistName(assistStats.name[0].ToString());
	}

	void Start()
	{
		InitializeStats();
	}

	public void SetPlayerUI(PlayerUI playerUI)
	{
		_playerUI = playerUI;
	}

	public void SetOtherPlayer(Player otherPlayer)
	{
		OtherPlayer = otherPlayer;
		OtherPlayerMovement = otherPlayer.GetComponent<PlayerMovement>();
		OtherPlayerUI = otherPlayer.PlayerUI;
		OtherPlayerStateManager = otherPlayer.PlayerStateManager;
	}

	public void SetToGrabPoint(Player player)
	{
		player.transform.SetParent(_grabPoint);
		player.transform.localPosition = Vector2.zero;
		player.transform.localScale = new Vector2(-1.0f, 1.0f);
	}

	public void ResetPlayer()
	{
		_playerStateManager.ResetToInitialState();
		transform.rotation = Quaternion.identity;
		IsStunned = false;
		IsDead = false;
		IsAttacking = false;
		_controller.ActiveController.enabled = true;
		_controller.ActivateInput();
		_effectsParent.gameObject.SetActive(true);
		SetHurtbox(true);
		AssistGauge = 1.0f;
		_playerMovement.FullyLockMovement = false;
		transform.SetParent(null);
		_playerMovement.IsInCorner = false;
		if (!GameManager.Instance.InfiniteArcana)
		{
			Arcana = 0.0f;
		}
		IsKnockedDown = false;
		StopAllCoroutines();
		_playerMovement.StopAllCoroutines();
		OtherPlayerUI.ResetCombo();
		_playerMovement.ResetPlayerMovement();
		_playerUI.SetArcana(Arcana);
		_playerUI.SetAssist(AssistGauge);
		_playerUI.ResetHealthDamaged();
		InitializeStats();
		_playerUI.ShowPlayerIcon();
	}

	public void ResetLives()
	{
		Lives = 2;
		_playerUI.ResetLives();
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
	}

	private void AssistCharge()
	{
		if (AssistGauge < 1.0f && !_assist.IsOnScreen && CanShadowbreak && GameManager.Instance.HasGameStarted)
		{
			AssistGauge += Time.deltaTime / (11.0f - _assist.AssistStats.assistRecharge);
			if (GameManager.Instance.InfiniteAssist)
			{
				AssistGauge = 1.0f;
			}
			_playerUI.SetAssist(AssistGauge);
		}
	}

	private void ArcanaCharge()
	{
		if (Arcana < _playerStats.PlayerStatsSO.maxArcana && GameManager.Instance.HasGameStarted)
		{
			Arcana += Time.deltaTime / (ArcaneSlowdown - _playerStats.PlayerStatsSO.arcanaRecharge);
			if (GameManager.Instance.InfiniteArcana)
			{
				Arcana = _playerStats.PlayerStatsSO.maxArcana;
			}
			_playerUI.SetArcana(Arcana);
		}
	}

	public void Flip()
	{
		if (OtherPlayer.transform.position.x > transform.position.x && transform.position.x < 9.2f && transform.localScale.x != 1.0f)
		{
			transform.localScale = new Vector2(1.0f, transform.localScale.y);
			_keepFlip.localScale = new Vector2(1.0f, transform.localScale.y);
		}
		else if (OtherPlayer.transform.position.x < transform.position.x && transform.position.x > -9.2f && transform.localScale.x != -1.0f)
		{
			transform.localScale = new Vector2(-1.0f, transform.localScale.y);
			_keepFlip.localScale = new Vector2(-1.0f, transform.localScale.y);
		}
	}

	public bool AssistAction()
	{
		if (AssistGauge >= 1.0f && GameManager.Instance.HasGameStarted)
		{
			_assist.Attack();
			DecreaseArcana();
			return true;
		}
		return false;
	}

	public void DecreaseArcana()
	{
		AssistGauge--;
		_playerUI.SetAssist(AssistGauge);
	}

	public void HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null)
	{
		CurrentAttack.hurtEffectPosition = hit.point;
		bool gotHit = hurtbox.TakeDamage(CurrentAttack);
		if (!CurrentAttack.isAirAttack && CurrentAttack.attackTypeEnum != AttackTypeEnum.Break && !CurrentAttack.isProjectile && !CurrentAttack.isArcana)
		{
			AttackState.CanSkipAttack = true;
		}
		if (OtherPlayerMovement.IsInCorner && !CurrentAttack.isProjectile)
		{
			_playerMovement.Knockback(new Vector2(-transform.localScale.x, 0.0f), CurrentAttack.knockback, CurrentAttack.knockbackDuration);
		}
	}

	public virtual void CreateEffect(bool isProjectile = false)
	{
		if (CurrentAttack.hitEffect != null)
		{
			GameObject hitEffect;
			hitEffect = Instantiate(CurrentAttack.hitEffect, _effectsParent);
			hitEffect.transform.localPosition = CurrentAttack.hitEffectPosition;
			hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, CurrentAttack.hitEffectRotation);
			if (isProjectile)
			{
				hitEffect.transform.SetParent(null);
				hitEffect.GetComponent<MoveInDirection>().Direction = new Vector2(transform.localScale.x, 0.0f);
				hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetHitboxResponder(transform);
			}
		}
	}

	public bool TakeDamage(AttackSO attack)
	{
		if (attack.attackTypeEnum == AttackTypeEnum.Throw)
		{
			return _playerStateManager.TryToGrabbedState();
		}
		else if (CanBlock(attack))
		{
			return _playerStateManager.TryToBlockState(attack);
		}
		else
		{
			return _playerStateManager.TryToHurtState(attack);
		}
	}

	private bool CanBlock(AttackSO attack)
	{
		if (attack.attackTypeEnum == AttackTypeEnum.Break)
		{
			return false;
		}
		if (transform.localScale.x == 1.0f && _playerMovement.MovementInput.x < 0.0f
				   || transform.localScale.x == -1.0f && _playerMovement.MovementInput.x > 0.0f)
		{
			return true;
		}
		return false;
	}

	public void Taunt()
	{
		_playerAnimator.Taunt();
		_controller.ActiveController.enabled = false;
	}

	public void LoseLife()
	{
		Lives--;
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
		SetPushboxTrigger(state);
		_airPushbox.gameObject.SetActive(state);
	}

	public void SetHurtbox(bool state)
	{
		_hurtbox.gameObject.SetActive(state);
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
