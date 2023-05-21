using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityGGPO;
public class Player : MonoBehaviour, IHitstop
{
    [SerializeField] private PlayerAnimator _playerAnimator = default;
    [SerializeField] private Assist _assist = default;
    [SerializeField] private Transform _hurtbox = default;
    [SerializeField] protected Transform _effectsParent = default;
    [SerializeField] private Transform _cameraPoint = default;
    [SerializeField] private Transform _keepFlip = default;
    [SerializeField] private GameObject _throwTechPrefab = default;
    [SerializeField] private GameObject[] _playerIcons = default;
    protected PlayerUI _playerUI;
    private PlayerMovement _playerMovement;
    [HideInInspector] public PlayerStatsSO playerStats;
    protected PlayerComboSystem _playerComboSystem;
    private BrainController _controller;
    private InputBuffer _inputBuffer;
    private Coroutine _shakeContactCoroutine;
    [HideInInspector] public UnityEvent hitstopEvent;
    [HideInInspector] public UnityEvent hitConnectsEvent;
    [HideInInspector] public UnityEvent parryConnectsEvent;
    public PlayerSimulation PlayerSimulation { get; private set; }
    public PlayerAnimator PlayerAnimator { get { return _playerAnimator; } private set { } }
    public Player OtherPlayer { get; private set; }
    public PlayerMovement OtherPlayerMovement { get; private set; }
    public PlayerUI OtherPlayerUI { get; private set; }
    public PlayerStatsSO PlayerStats { get { return playerStats; } set { } }
    public PlayerUI PlayerUI { get { return _playerUI; } private set { } }
    public AttackSO CurrentAttack { get; set; }
    public ResultAttack ResultAttack { get; set; }
    public Transform CameraPoint { get { return _cameraPoint; } private set { } }
    public int Health { get; set; }
    public int HealthRecoverable { get; set; }
    public int Lives { get; set; } = 2;
    public bool IsPlayerOne { get; set; }
    public DemonicsFloat AssistGauge { get; set; } = (DemonicsFloat)1;
    public DemonicsFloat ArcanaGauge { get; set; }
    public Assist Assist { get { return _assist; } private set { } }
    public bool CanSkipAttack { get; set; }
    public bool LockChain { get; set; }
    public bool HasJuggleForce { get; set; }

    void Awake()
    {
        _inputBuffer = GetComponent<InputBuffer>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerComboSystem = GetComponent<PlayerComboSystem>();
        PlayerSimulation = GetComponent<PlayerSimulation>();
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

    public void ThrowTechPrefab(DemonicsVector2 pos)
    {
        Instantiate(_throwTechPrefab, new Vector3((float)pos.x, (float)pos.y, 0), Quaternion.identity);
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
    }

    public void ResetPlayer(Vector2 resetPosition)
    {
        RecallAssist();
        _playerMovement.StopKnockback();
        int index = IsPlayerOne ? 0 : 1;
        GameSimulation._players[index].position = new DemonicsVector2((DemonicsFloat)resetPosition.x, (DemonicsFloat)resetPosition.y);
        _playerMovement.Physics.SetPositionWithRender(new DemonicsVector2((DemonicsFloat)GameSimulation._players[index].position.x, (DemonicsFloat)GameSimulation._players[index].position.y));
        transform.rotation = Quaternion.identity;
        _effectsParent.gameObject.SetActive(true);
        SetHurtbox(true);
        AssistGauge = (DemonicsFloat)1;
        transform.SetParent(null);
        if (!GameplayManager.Instance.InfiniteArcana)
        {
            ArcanaGauge = (DemonicsFloat)0;
        }
        StopAllCoroutines();
        StopComboTimer();
        _playerMovement.StopAllCoroutines();
        PlayerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
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

    public AttackSO shadowbreakKnockback()
    {
        GameplayManager.Instance.AddHitstop(this);
        return new AttackSO() { hitStun = 30, hitstop = 5, knockbackForce = new Vector2(0.1f, 1), knockbackDuration = 5, hurtEffectPosition = new Vector2(0, (float)_playerMovement.Physics.Position.y + 1) };
    }

    public void MaxHealthStats()
    {
        _playerUI.ResetHealthDamaged();
        Health = playerStats.maxHealth;
        HealthRecoverable = playerStats.maxHealth;
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

    public void StartShakeContact()
    {
        _shakeContactCoroutine = StartCoroutine(ShakeContactCoroutine());
    }

    public void StopShakeCoroutine()
    {
        if (_shakeContactCoroutine != null)
        {
            _playerAnimator.transform.localPosition = Vector2.zero;
            StopCoroutine(_shakeContactCoroutine);
            _shakeContactCoroutine = null;
        }
    }

    IEnumerator ShakeContactCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.075f);
            PlayerAnimator.transform.localPosition = new Vector2(PlayerAnimator.transform.localPosition.x - 0.03f, PlayerAnimator.transform.localPosition.y);
            yield return new WaitForSeconds(0.075f);
            PlayerAnimator.transform.localPosition = Vector2.zero;
        }
    }

    private void Flip(int xDirection)
    {
        transform.localScale = new Vector2(xDirection, transform.localScale.y);
        _keepFlip.localScale = transform.localScale;
    }

    public bool CheckRecoverableHealth()
    {
        float remainingRecoverableHealth = HealthRecoverable - Health;
        if (remainingRecoverableHealth > 0)
        {
            return true;
        }
        return false;
    }
    public void StopComboTimer()
    {
        // _comboTimerWaitFrames = 0;
        // _playerUI.SetComboTimerActive(false);
        // _playerUI.ResetCombo();
        // _comboTimerPaused = false;
    }

    public void RecallAssist()
    {
        _assist.Recall();
    }

    public bool TakeDamage(AttackSO attack)
    {
        return true;
    }

    public void EnterHitstop()
    {
        _playerMovement.EnterHitstop();
        PlayerAnimator.Pause();
    }

    public void ExitHitstop()
    {
        StopShakeCoroutine();
        _playerMovement.ExitHitstop();
        PlayerAnimator.Resume();
        PlayerAnimator.SpriteNormalEffect();
        hitstopEvent?.Invoke();
        hitstopEvent.RemoveAllListeners();
    }

    public bool IsInHitstop()
    {
        return _playerMovement.IsInHitstop;
    }

    public void LoseLife()
    {
        Lives--;
        _playerUI.SetLives();
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
        if (GameplayManager.Instance.IsTrainingMode)
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
        if (!GameplayManager.Instance.IsTrainingMode)
        {
            _playerUI.ClosePauseHold();
        }
    }

    public bool IsAnimationFinished(string name, int frames)
    {
        return PlayerAnimator.IsAnimationFinished(name, frames);
    }
    public bool IsAnimationLoop(string name)
    {
        return PlayerAnimator.IsAnimationLoop(name);
    }
    public string ConnectionStatus { get; private set; }
    public int ConnectionProgress { get; private set; }
    public void Simulate(PlayerNetwork playerGs, PlayerConnectionInfo info)
    {
        _playerMovement.SetPosition(playerGs.position);
        _playerUI.SetHealth(playerGs.health);
        _playerUI.SetRecoverableHealth(playerGs.healthRecoverable);
        _playerUI.SetAssist(playerGs.shadowGauge);
        Flip(playerGs.flip);
        NetworkDebug(info);
    }

    private void NetworkDebug(PlayerConnectionInfo info)
    {
#if UNITY_EDITOR
        switch (info.state)
        {
            case PlayerConnectState.Connecting:
                ConnectionStatus = (info.type == GGPOPlayerType.GGPO_PLAYERTYPE_LOCAL) ? "<color=green>P" : "<color=blue>C";
                break;
            case PlayerConnectState.Synchronizing:
                ConnectionProgress = info.connect_progress;
                ConnectionStatus = (info.type == GGPOPlayerType.GGPO_PLAYERTYPE_LOCAL) ? "<color=green>P" : "<color=purple>S";
                break;
            case PlayerConnectState.Disconnected:
                ConnectionStatus = "<color=red>D";
                break;
            case PlayerConnectState.Disconnecting:
                ConnectionStatus = "<color=yellow>W";
                ConnectionProgress = (Utils.TimeGetTime() - info.disconnect_start) * 100 / info.disconnect_timeout;
                break;
        }
#endif
    }
}
