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
    private readonly DemonicsFloat _damageDecay = (DemonicsFloat)0.97f;
    private readonly DemonicsFloat _whiteHealthDivider = (DemonicsFloat)1.4f;
    [HideInInspector] public UnityEvent hitstopEvent;
    [HideInInspector] public UnityEvent hitConnectsEvent;
    [HideInInspector] public UnityEvent parryConnectsEvent;

    public PlayerStateManager PlayerStateManager { get { return _playerStateManager; } private set { } }
    public PlayerStateManager OtherPlayerStateManager { get; private set; }
    public Player OtherPlayer { get; private set; }
    public PlayerMovement OtherPlayerMovement { get; private set; }
    public PlayerUI OtherPlayerUI { get; private set; }
    public PlayerStatsSO PlayerStats { get { return playerStats; } set { } }
    public PlayerUI PlayerUI { get { return _playerUI; } private set { } }
    public AttackSO CurrentAttack { get; set; }
    public ResultAttack ResultAttack { get; set; }
    public Pushbox Groundbox { get { return _groundPushbox; } private set { } }
    public Transform CameraPoint { get { return _cameraPoint; } private set { } }
    public bool CanAirArcana { get; set; }
    public int Health { get; set; }
    public int HealthRecoverable { get; set; }
    public int Lives { get; set; } = 2;
    public bool IsPlayerOne { get; set; }
    public DemonicsFloat AssistGauge { get; set; } = (DemonicsFloat)1;
    public DemonicsFloat ArcanaGauge { get; set; }
    public int ArcaneSlowdown { get; set; } = 6;
    public bool CanShadowbreak { get; set; } = true;
    public bool BlockingLow { get; set; }
    public bool BlockingHigh { get; set; }
    public bool BlockingMiddair { get; set; }
    public bool Parrying { get; set; }
    public bool CanSkipAttack { get; set; }
    public bool Invincible { get; set; }
    public bool Invisible { get; set; }
    public bool LockChain { get; set; }

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerComboSystem = GetComponent<PlayerComboSystem>();
        ResultAttack = new ResultAttack();
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

    public void ResetPlayer(Vector2 resetPosition)
    {
        RecallAssist();
        _playerMovement.Physics.ResetSkipWall();
        _playerMovement.Physics.Position = new DemonicsVector2((DemonicsFloat)resetPosition.x, (DemonicsFloat)resetPosition.y);
        _playerMovement.Physics.Velocity = DemonicsVector2.Zero;
        _playerStateManager.ResetToInitialState();
        SetInvinsible(false);
        transform.rotation = Quaternion.identity;
        _effectsParent.gameObject.SetActive(true);
        SetHurtbox(true);
        AssistGauge = (DemonicsFloat)1;
        transform.SetParent(null);
        if (!GameManager.Instance.InfiniteArcana)
        {
            ArcanaGauge = (DemonicsFloat)0;
        }
        _playerMovement.Physics.EnableGravity(true);
        StopAllCoroutines();
        StopComboTimer();
        _playerMovement.StopAllCoroutines();
        _playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
        _playerUI.SetArcana((float)ArcanaGauge);
        _playerUI.SetAssist((float)AssistGauge);
        _playerUI.ResetHealthDamaged();
        InitializeStats();
        _playerUI.ShowPlayerIcon();
        hitstopEvent.RemoveAllListeners();
        LockChain = false;
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
        _playerUI.CheckDemonLimit(Health);
    }

    private void InitializeStats()
    {
        _playerUI.InitializeUI(playerStats, _controller, _playerIcons);
        Health = playerStats.maxHealth;
        HealthRecoverable = playerStats.maxHealth;
        _playerUI.SetHealth(Health);
    }

    void FixedUpdate()
    {
        ArcanaCharge();
        AssistCharge();
    }

    private void AssistCharge()
    {
        if (AssistGauge < (DemonicsFloat)1.0f && !_assist.IsOnScreen && CanShadowbreak && GameManager.Instance.HasGameStarted)
        {
            AssistGauge += (DemonicsFloat)(Time.deltaTime / (10.0f - _assist.AssistStats.assistRecharge));
            if (GameManager.Instance.InfiniteAssist)
            {
                AssistGauge = (DemonicsFloat)1.0f;
            }
            _playerUI.SetAssist((float)AssistGauge);
        }
    }

    private void ArcanaCharge()
    {
        if (ArcanaGauge < (DemonicsFloat)playerStats.Arcana && GameManager.Instance.HasGameStarted)
        {
            ArcanaGauge += (DemonicsFloat)(Time.deltaTime / (ArcaneSlowdown - playerStats.arcanaRecharge));
            if (GameManager.Instance.InfiniteArcana)
            {
                ArcanaGauge = (DemonicsFloat)playerStats.Arcana;
            }
            _playerUI.SetArcana((float)ArcanaGauge);
        }
    }

    public void ArcanaGain(DemonicsFloat arcana)
    {
        if (ArcanaGauge < (DemonicsFloat)playerStats.Arcana && GameManager.Instance.HasGameStarted)
        {
            ArcanaGauge += arcana;
            _playerUI.SetArcana((float)ArcanaGauge);
        }
    }

    public void HealthGain()
    {
        Health = HealthRecoverable;
        _playerUI.UpdateHealth();
        _playerUI.CheckDemonLimit(Health);
    }

    public void SetHealth(int value, bool noRecoverable = false)
    {
        Health -= value;
        if (noRecoverable)
        {
            HealthRecoverable -= value;
        }
        else
        {
            HealthRecoverable -= (int)((DemonicsFloat)value / _whiteHealthDivider);
        }
        _playerUI.SetHealth(Health);
        _playerUI.SetRecoverableHealth(HealthRecoverable);
    }

    public void CheckFlip()
    {
        if (OtherPlayer.transform.position.x > transform.position.x && !_playerMovement.IsInCorner)
        {
            Flip(1);
        }
        else if (OtherPlayer.transform.position.x < transform.position.x && !_playerMovement.IsInCorner)
        {
            Flip(-1);
        }
    }

    public void Flip(int xDirection)
    {
        transform.localScale = new Vector2(xDirection, transform.localScale.y);
        _keepFlip.localScale = transform.localScale;
    }

    public bool HasRecoverableHealth()
    {
        float remainingRecoverableHealth = HealthRecoverable - Health;
        if (remainingRecoverableHealth > 0)
        {
            HealthRecoverable = Health;
            _playerUI.SetRecoverableHealth(HealthRecoverable);
            return true;
        }
        return false;
    }

    public bool AssistAction()
    {
        if (AssistGauge >= (DemonicsFloat)0.5f && GameManager.Instance.HasGameStarted && !_assist.IsOnScreen)
        {
            _assist.Attack();
            DecreaseArcana((DemonicsFloat)0.5f);
            return true;
        }
        return false;
    }

    public float DemonLimitMultiplier()
    {
        if (Health < 3000)
        {
            return 1.2f;
        }
        return 1.0f;
    }

    public void DecreaseArcana(DemonicsFloat value)
    {
        AssistGauge -= value;
        _playerUI.SetAssist((float)AssistGauge);
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
        if (_comboTimerCoroutine != null && !_comboTimerPaused)
        {
            _playerUI.SetComboTimerLock(true);
            _comboTimerPaused = true;
        }
    }

    public void UnfreezeComboTimer()
    {
        if (_comboTimerCoroutine != null && _comboTimerPaused)
        {
            _playerUI.SetComboTimerLock(false);
            _comboTimerPaused = false;
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

    public int CalculateDamage(AttackSO hurtAttack)
    {
        int comboCount = OtherPlayerUI.CurrentComboCount;
        DemonicsFloat calculatedDamage = (DemonicsFloat)((hurtAttack.damage / playerStats.Defense) * OtherPlayer.DemonLimitMultiplier());
        if (comboCount > 1)
        {
            DemonicsFloat damageScale = (DemonicsFloat)1;
            for (int i = 0; i < comboCount; i++)
            {
                damageScale *= _damageDecay;
            }
            calculatedDamage *= damageScale;
        }
        int calculatedIntDamage = (int)calculatedDamage;
        OtherPlayer.SetResultAttack(calculatedIntDamage, hurtAttack);
        return calculatedIntDamage;
    }

    public void SetResultAttack(int calculatedDamage, AttackSO attack)
    {
        ResultAttack.startUpFrames = attack.startUpFrames;
        ResultAttack.activeFrames = attack.activeFrames;
        ResultAttack.recoveryFrames = attack.recoveryFrames;
        ResultAttack.attackTypeEnum = attack.attackTypeEnum;
        ResultAttack.damage = calculatedDamage;
        ResultAttack.comboDamage += calculatedDamage;
    }

    public bool HitboxCollided(Vector2 hurtPosition, Hurtbox hurtbox = null)
    {
        if (!CurrentAttack.isProjectile)
        {
            if (!CurrentAttack.isArcana || CurrentAttack.attackTypeEnum != AttackTypeEnum.Throw)
            {
                GameManager.Instance.AddHitstop(this);
            }
        }
        CurrentAttack.hurtEffectPosition = hurtPosition;
        if (!CurrentAttack.isProjectile)
        {
            if (!CurrentAttack.isArcana)
            {
                CanSkipAttack = true;
            }
            if (OtherPlayerMovement.IsInCorner)
            {
                if (!CurrentAttack.isArcana && CurrentAttack.attackTypeEnum != AttackTypeEnum.Throw)
                {
                    _playerMovement.Knockback(new Vector2(OtherPlayer.transform.localScale.x, 0), new Vector2(CurrentAttack.knockbackForce.x, 0), CurrentAttack.knockbackDuration);
                }
            }
        }
        return hurtbox.TakeDamage(CurrentAttack);
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
                hitEffect.GetComponent<Projectile>().SetSourceTransform(transform, transform.position);
                hitEffect.GetComponent<Projectile>().Direction = new Vector2(transform.localScale.x, 0.0f);
                hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetHitboxResponder(transform);
            }
        }
    }

    public void SetInvinsible(bool state)
    {
        Invisible = state;
        _playerAnimator.SetInvinsible(state);
        SetHurtbox(!state);
        SetPushboxTrigger(state);
    }

    public bool TakeDamage(AttackSO attack)
    {
        GameManager.Instance.AddHitstop(this);
        if (attack.attackTypeEnum == AttackTypeEnum.Throw)
        {
            return _playerStateManager.TryToGrabbedState();
        }
        if (Invincible)
        {
            return false;
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

    public void SetSpriteOrderPriority()
    {
        _playerAnimator.SetSpriteOrder(1);
        OtherPlayer._playerAnimator.SetSpriteOrder(0);
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
        _playerAnimator.SpriteNormalEffect();
        hitstopEvent?.Invoke();
        hitstopEvent.RemoveAllListeners();
    }

    public bool IsInHitstop()
    {
        return _playerMovement.IsInHitstop;
    }

    public void HurtOnSuperArmor(AttackSO attack)
    {
        SetHealth(CalculateDamage(attack));
        _playerUI.Damaged();
        _playerAnimator.SpriteSuperArmorEffect();
        GameManager.Instance.HitStop(attack.hitstop);
    }

    public bool CanTakeSuperArmorHit(AttackSO attack)
    {
        if (CurrentAttack.hasSuperArmor && !_playerAnimator.InRecovery() && !CanSkipAttack)
        {
            return true;
        }
        return false;
    }

    private bool CanBlock(AttackSO attack)
    {
        if (_playerStateManager.CurrentState is BlockParentState)
        {
            return true;
        }
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
