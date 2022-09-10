using Demonics.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IHurtboxResponder, IHitboxResponder, IHitstop
{
	[SerializeField] private PlayerStateManager _playerStateManager = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private Assist _assist = default;
	[SerializeField] private Pushbox _groundPushbox = default;
	[SerializeField] private Transform _hurtbox = default;
	[SerializeField] protected Transform _effectsParent = default;
	[SerializeField] private Transform _grabPoint = default;
	[SerializeField] private Transform _cameraPoint = default;
	[SerializeField] private Transform _keepFlip = default;
	[SerializeField] private GameObject[] _playerIcons = default;
	protected PlayerUI _playerUI;
	private PlayerMovement _playerMovement;
	[HideInInspector] public PlayerStatsSO playerStats;
	protected PlayerComboSystem _playerComboSystem;
	private BrainController _controller;
	private Coroutine _comboTimerCoroutine;
	private bool _comboTimerPaused;
	private readonly float _damageDecay = 0.97f;
	[HideInInspector] public UnityEvent knockbackEvent;

	public PlayerStateManager PlayerStateManager { get { return _playerStateManager; } private set { } }
	public PlayerStateManager OtherPlayerStateManager { get; private set; }
	public Player OtherPlayer { get; private set; }
	public PlayerMovement OtherPlayerMovement { get; private set; }
	public PlayerUI OtherPlayerUI { get; private set; }
	public PlayerStatsSO PlayerStats { get { return playerStats; } set { } }
	public PlayerUI PlayerUI { get { return _playerUI; } private set { } }
	public AttackSO CurrentAttack { get; set; }
	public AttackSO ResultAttack { get; set; }
	public Transform CameraPoint { get { return _cameraPoint; } private set { } }
	public bool CanAirArcana { get; set; }
	public float Health { get; set; }
	public int Lives { get; set; } = 2;
	public bool IsAttacking { get; set; }
	public bool IsPlayerOne { get; set; }
	public float AssistGauge { get; set; } = 1.0F;
	public float Arcana { get; set; }
	public float ArcaneSlowdown { get; set; } = 5.5f;
	public bool CanShadowbreak { get; set; } = true;
	public bool CanCancelAttack { get; set; }
	public bool BlockingLow { get; set; }
	public bool BlockingHigh { get; set; }
	public bool BlockingMiddair { get; set; }
	public bool Parrying { get; set; }


	void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerComboSystem = GetComponent<PlayerComboSystem>();
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

	public bool HasGrabbed()
	{
		if (_grabPoint.childCount == 0)
		{
			return false;
		}
		return true;
	}

	public void ResetPlayer()
	{
		RecallAssist();
		_playerStateManager.ResetToInitialState();
		transform.rotation = Quaternion.identity;
		_effectsParent.gameObject.SetActive(true);
		SetHurtbox(true);
		AssistGauge = 1.0f;
		transform.SetParent(null);
		if (!GameManager.Instance.InfiniteArcana)
		{
			Arcana = 0.0f;
		}
		StopAllCoroutines();
		StopComboTimer();
		_playerMovement.StopAllCoroutines();
		_playerMovement.ResetMovement();
		_playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
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
		Health = playerStats.maxHealth;
		_playerUI.MaxHealth(Health);
	}

	private void InitializeStats()
	{
		_playerUI.InitializeUI(playerStats, _controller, _playerIcons);
		Health = playerStats.maxHealth;
		_playerUI.SetHealth(Health);
	}

	void FixedUpdate()
	{
		ArcanaCharge();
		AssistCharge();
	}

	private void AssistCharge()
	{
		if (AssistGauge < 1.0f && !_assist.IsOnScreen && CanShadowbreak && GameManager.Instance.HasGameStarted)
		{
			AssistGauge += Time.deltaTime / (9.0f - _assist.AssistStats.assistRecharge);
			if (GameManager.Instance.InfiniteAssist)
			{
				AssistGauge = 1.0f;
			}
			_playerUI.SetAssist(AssistGauge);
		}
	}

	private void ArcanaCharge()
	{
		if (Arcana < playerStats.Arcana && GameManager.Instance.HasGameStarted)
		{
			Arcana += Time.deltaTime / (ArcaneSlowdown - playerStats.arcanaRecharge);
			if (GameManager.Instance.InfiniteArcana)
			{
				Arcana = playerStats.Arcana;
			}
			_playerUI.SetArcana(Arcana);
		}
	}

	public void ArcanaGain(float arcana)
	{
		if (Arcana < playerStats.Arcana && GameManager.Instance.HasGameStarted)
		{
			Arcana += arcana;
			_playerUI.SetArcana(Arcana);
		}
	}

	public void CheckFlip()
	{
		if (OtherPlayer.transform.position.x > transform.position.x && !_playerMovement.IsInCorner && _keepFlip.localScale.x != 1.0f)
		{
			if (!OtherPlayerMovement.IsInCorner)
			{
				Flip(1);
			}
		}
		else if (OtherPlayer.transform.position.x < transform.position.x && !_playerMovement.IsInCorner && _keepFlip.localScale.x != -1.0f)
		{
			if (!OtherPlayerMovement.IsInCorner)
			{
				Flip(-1);
			}
		}
	}

	public void Flip(int xDirection)
	{
		transform.localScale = new Vector2(xDirection, transform.localScale.y);
		_keepFlip.localScale = transform.localScale;
	}

	public bool AssistAction()
	{
		if (AssistGauge >= 0.5f && GameManager.Instance.HasGameStarted)
		{
			_assist.Attack();
			DecreaseArcana(0.5f);
			//CurrentAttack = _assist.AssistStats.attackSO;
			return true;
		}
		return false;
	}

	public void SetResultAttack(int calculatedDamage)
	{
		if (CurrentAttack != null)
		{
			ResultAttack = Instantiate(CurrentAttack);
			ResultAttack.damage = calculatedDamage;
		}
	}

	public float DemonLimitMultiplier()
	{
		if (Health < 3000)
		{
			return 1.2f;
		}
		return 1.0f;
	}

	public void DecreaseArcana(float value)
	{
		AssistGauge -= value;
		_playerUI.SetAssist(AssistGauge);
	}

	public void StartComboTimer(ComboTimerStarterEnum comboTimerStarter)
	{
		_playerUI.SetComboTimerActive(true);
		_comboTimerCoroutine = StartCoroutine(StartComboTimerCoroutine(comboTimerStarter));
	}

	public void StopComboTimer()
	{
		if (_comboTimerCoroutine != null)
		{
			StopCoroutine(_comboTimerCoroutine);
		}
		_playerUI.SetComboTimerActive(false);
		_playerUI.ResetCombo();
		_comboTimerPaused = false;
	}

	public void FreezeComboTimer()
	{
		if (_comboTimerCoroutine != null)
		{
			_playerUI.SetComboTimerLock();
			_comboTimerPaused = true;
		}
	}

	IEnumerator StartComboTimerCoroutine(ComboTimerStarterEnum comboTimerStarter)
	{
		float elapsedTime = 0.0f;
		float waitTime = ComboTimerStarterTypes.GetComboTimerStarterValue(comboTimerStarter);
		Color color = ComboTimerStarterTypes.GetComboTimerStarterColor(comboTimerStarter);
		while (elapsedTime < waitTime)
		{
			if (!_comboTimerPaused)
			{
				float value = Mathf.Lerp(1.0f, 0.0f, elapsedTime / waitTime);
				elapsedTime += Time.deltaTime;
				_playerUI.SetComboTimer(value, color);
				yield return null;
			}
			else
			{
				yield return null;
			}
		}
		OtherPlayer._playerStateManager.TryToIdleState();
		_playerUI.SetComboTimerActive(false);
	}

	public void RecallAssist()
	{
		_assist.Recall();
	}

	public float CalculateDamage(AttackSO hurtAttack)
	{
		int comboCount = OtherPlayerUI.CurrentComboCount;
		float calculatedDamage = (hurtAttack.damage / playerStats.Defense) * OtherPlayer.DemonLimitMultiplier();
		if (comboCount > 1)
		{
			float damageScale = 1.0f;
			for (int i = 0; i < comboCount; i++)
			{
				damageScale *= _damageDecay;
			}
			calculatedDamage *= damageScale;
		}
		OtherPlayer.SetResultAttack(Mathf.RoundToInt(calculatedDamage));
		return calculatedDamage;
	}


	public void HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null)
	{
		if (!CurrentAttack.isProjectile)
		{
			if (!CurrentAttack.isArcana || CurrentAttack.attackTypeEnum != AttackTypeEnum.Throw)
			{
				GameManager.Instance.AddHitstop(this);
			}
		}
		CurrentAttack.hurtEffectPosition = hit.point;
		hurtbox.TakeDamage(CurrentAttack);
		if (!CurrentAttack.isProjectile)
		{
			if (!CurrentAttack.isArcana)
			{
				AttackState.CanSkipAttack = true;
			}
			if (OtherPlayerMovement.IsInCorner)
			{
				if (!CurrentAttack.isArcana && CurrentAttack.attackTypeEnum != AttackTypeEnum.Throw)
				{
					_playerMovement.Knockback(new Vector2(OtherPlayer.transform.localScale.x, 0.0f), new Vector2(CurrentAttack.knockback, 0.0f), CurrentAttack.knockbackDuration);
				}
			}
		}
	}

	public virtual void CreateEffect(bool isProjectile = false)
	{
		if (CurrentAttack.hitEffect != null)
		{
			GameObject hitEffect;
			hitEffect = ObjectPoolingManager.Instance.Spawn(CurrentAttack.hitEffect, parent: _effectsParent);
			hitEffect.transform.localPosition = CurrentAttack.hitEffectPosition;
			hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, CurrentAttack.hitEffectRotation);
			hitEffect.transform.localScale = new Vector2(-1, 1);
			if (isProjectile)
			{
				hitEffect.transform.SetParent(null);
				hitEffect.transform.localScale = new Vector2(transform.localScale.x, 1);
				hitEffect.GetComponent<Projectile>().SetSourceTransform(transform);
				hitEffect.GetComponent<Projectile>().Direction = new Vector2(transform.localScale.x, 0.0f);
				hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetHitboxResponder(transform);
			}
		}
	}

	public bool TakeDamage(AttackSO attack)
	{
		GameManager.Instance.AddHitstop(this);
		if (attack.attackTypeEnum == AttackTypeEnum.Throw)
		{
			return _playerStateManager.TryToGrabbedState();
		}
		if (CanBlock(attack))
		{
			if (!_playerStateManager.TryToBlockState(attack))
			{
				return _playerStateManager.TryToHurtState(attack);
			}
		}
		return _playerStateManager.TryToHurtState(attack);
	}

	public void EnterHitstop()
	{
		_playerMovement.EnterHitstop();
		_playerAnimator.Pause();
	}

	public void ExitHitstop()
	{
		_playerMovement.ExitHitstop();
		_playerAnimator.Resume();
		knockbackEvent?.Invoke();
		knockbackEvent.RemoveAllListeners();
	}

	private bool CanBlock(AttackSO attack)
	{
		if (attack.attackTypeEnum == AttackTypeEnum.Break)
		{
			if (BlockingLeftOrRight())
			{
				_playerUI.DisplayNotification(NotificationTypeEnum.GuardBreak);
			}
			return false;
		}
		if (_controller.ControllerInputName == ControllerTypeEnum.Cpu.ToString() && TrainingSettings.BlockAlways && TrainingSettings.CpuOff)
		{
			return true;
		}
		if (BlockingLeftOrRight())
		{
			if (attack.attackTypeEnum == AttackTypeEnum.Overhead && !_controller.ActiveController.Crouch())
			{
				return true;
			}
			if (attack.attackTypeEnum == AttackTypeEnum.Mid)
			{
				return true;
			}
			if (attack.attackTypeEnum == AttackTypeEnum.Low && _controller.ActiveController.Crouch())
			{
				return true;
			}
		}
		return false;
	}

	private bool BlockingLeftOrRight()
	{
		if (transform.localScale.x == 1.0f && _playerMovement.MovementInput.x < 0.0f
				   || transform.localScale.x == -1.0f && _playerMovement.MovementInput.x > 0.0f)
		{
			return true;
		}
		return false;
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

	public void SetHurtbox(bool state)
	{
		for (int i = 0; i < _hurtbox.childCount; i++)
		{
			_hurtbox.GetChild(i).gameObject.SetActive(state);
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
