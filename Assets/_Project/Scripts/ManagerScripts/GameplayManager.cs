using Cinemachine;
using Demonics.Manager;
using SharedGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private StageTypeEnum _stage = default;
    [SerializeField] private CharacterTypeEnum _characterOne = default;
    [SerializeField] private CharacterTypeEnum _characterTwo = default;
    [SerializeField] private AssistTypeEnum _assistOne = default;
    [SerializeField] private AssistTypeEnum _assistTwo = default;
    [Range(-1, 3)]
    [SerializeField] private int _controllerOne = default;
    [SerializeField] private ControllerTypeEnum _controllerOneType = default;
    [Range(-1, 3)]
    [SerializeField] private int _controllerTwo = default;
    [SerializeField] private ControllerTypeEnum _controllerTwoType = default;
    [SerializeField] private MusicTypeEnum _music = default;
    [Range(0, 10)]
    [SerializeField] private int _playerOneSkin = default;
    [Range(0, 10)]
    [SerializeField] private int _playerTwoSkin = default;
    [SerializeField] private bool _isTrainingMode = default;
    [SerializeField] private bool _1BitOn = default;
    [Range(1, 10)]
    [SerializeField] private int _gameSpeed = 1;
    [Range(10, 300)]
    [SerializeField] private int _countdownTime = 99;
    [SerializeField] private float _leftSpawn = 9;
    [SerializeField] private float _rightSpawn = 6;
    [SerializeField] private PlayerInput _uiInput = default;
    [SerializeField] private float[] _spawnPositionsX = default;
    [Header("Data")]
    [SerializeField] private ConnectionWidget _connectionWidget = default;
    [SerializeField] private CinemachineTargetGroup _targetGroup = default;
    [SerializeField] private DisconnectMenu _disconnectMenu = default;
    [SerializeField] private IntroUI _introUI = default;
    [SerializeField] private FadeHandler _fadeHandler = default;
    [SerializeField] protected PlayerUI _playerOneUI = default;
    [SerializeField] protected PlayerUI _playerTwoUI = default;
    [SerializeField] private PlayerDialogue _playerOneDialogue = default;
    [SerializeField] private PlayerDialogue _playerTwoDialogue = default;
    [SerializeField] private Animator _timerAnimator = default;
    [SerializeField] private Animator _timerMainAnimator = default;
    [SerializeField] protected TextMeshProUGUI _countdownText = default;
    [SerializeField] protected TextMeshProUGUI _readyText = default;
    [SerializeField] protected TextMeshProUGUI _winnerNameText = default;
    [SerializeField] protected TextMeshProUGUI _winsText = default;
    [SerializeField] protected GameObject _keyboardPrompts = default;
    [SerializeField] protected GameObject _controllerPrompts = default;
    [SerializeField] protected GameObject _xboxPrompts = default;
    [SerializeField] protected GameObject[] _readyObjects = default;
    [SerializeField] protected GameObject[] _arcanaObjects = default;
    [SerializeField] protected GameObject _debugNetwork = default;
    [SerializeField] protected GameObject _networkCanvas = default;
    [SerializeField] protected GameObject _infiniteTime = default;
    [SerializeField] protected GameObject _winsImage = default;
    [SerializeField] private GameObject[] _hearts = default;
    [SerializeField] private GameObject _trainingPrompts = default;
    [SerializeField] private MusicMenu _musicMenu = default;
    [SerializeField] private InputHistory[] _inputHistories = default;
    [SerializeField] private InputDisplay[] _inputDisplays = default;
    [SerializeField] private PlayerStatsSO[] _playerStats = default;
    [SerializeField] private TrainingMenu _trainingMenu = default;
    [SerializeField] private GameObject[] _stages = default;
    [SerializeField] private AssistStatsSO[] _assists = default;
    [SerializeField] private BaseMenu _matchOverMenu = default;
    [SerializeField] private BaseMenu _matchOverReplayMenu = default;
    [SerializeField] private BaseMenu _matchOverOnlineMenu = default;
    [SerializeField] private Animator _readyAnimator = default;
    [SerializeField] private Audio _musicAudio = default;
    [SerializeField] private Audio _uiAudio = default;
    public BrainController PlayerOneController { get; set; }
    public BrainController PlayerTwoController { get; set; }
    private PlayerAnimator _playerOneAnimator;
    private PlayerAnimator _playerTwoAnimator;
    private PlayerInput _playerOneInput;
    private PlayerInput _playerTwoInput;
    private Coroutine _roundOverTrainingCoroutine;
    private GameObject _currentStage;
    private List<IHitstop> _hitstopList = new();
    private DemonVector2 _cachedOneResetPosition;
    private DemonVector2 _cachedTwoResetPosition;
    private int _countdown;
    private int _currentRound = 1;
    private bool _reverseReset;
    private bool _hasSwitchedCharacters;
    private bool _canCallSwitchCharacter = true;
    private bool _finalRound;
    private int _playerOneWins;
    private int _playerTwoWins;
    public Sound CurrentMusic { get; private set; }
    public bool IsDialogueRunning { get; set; }
    public bool HasGameStarted { get; set; }
    public bool IsTrainingMode { get { return _isTrainingMode; } set { } }
    public bool InfiniteHealth { get; set; }
    public bool InfiniteArcana { get; set; }
    public bool InfiniteAssist { get; set; }
    public Player PlayerOne { get; private set; }
    public Player PlayerTwo { get; private set; }
    public CinemachineTargetGroup TargetGroup { get { return _targetGroup; } private set { } }
    public PauseMenu PauseMenu { get; set; }
    public static GameplayManager Instance { get; private set; }
    public BrainController PausedController { get; set; }
    public float GameSpeed { get; set; }
    private Keyboard keyboardTwo;
    void Awake()
    {
        MouseSetup.Instance.SetLock(true);
        keyboardTwo = InputSystem.AddDevice<Keyboard>("KeyboardTwo");
        HasGameStarted = false;
        GameSpeed = _gameSpeed;
        CheckInstance();
        if (!SceneSettings.SceneSettingsDecide)
        {
            _debugNetwork.SetActive(true);
            if (!SceneSettings.ReplayMode)
            {
                if (_controllerOne >= 0)
                {
                    SceneSettings.ControllerOne = InputSystem.devices[_controllerOne];
                }
                if (_controllerTwo >= 0)
                {
                    SceneSettings.ControllerTwo = InputSystem.devices[_controllerTwo];
                }
                SceneSettings.ControllerOneScheme = _controllerOneType.ToString();
                SceneSettings.ControllerTwoScheme = _controllerTwoType.ToString();
                SceneSettings.PlayerOne = (int)_characterOne;
                SceneSettings.PlayerTwo = (int)_characterTwo;
                SceneSettings.AssistOne = (int)_assistOne;
                SceneSettings.AssistTwo = (int)_assistTwo;
                SceneSettings.StageIndex = (int)_stage;
                SceneSettings.ColorOne = _playerOneSkin;
                SceneSettings.ColorTwo = _playerTwoSkin;
                SceneSettings.IsTrainingMode = _isTrainingMode;
                SceneSettings.Bit1 = _1BitOn;
                SceneSettings.MusicName = _music.ToString();
            }
        }
        else
        {
            _debugNetwork.SetActive(false);
            if (!SceneSettings.IsOnline)
            {
                NetworkInput.IS_LOCAL = true;
                GameManager.Instance.StartLocalGame();
                _isTrainingMode = SceneSettings.IsTrainingMode;
            }
            else
            {
                _isTrainingMode = false;
                _connectionWidget.StartGGPO(SceneSettings.OnlineOneIp, SceneSettings.OnlineTwoIp, SceneSettings.PrivateOneIp, SceneSettings.PrivateTwoIp,
                SceneSettings.PortOne, SceneSettings.PortTwo, SceneSettings.OnlineIndex);
                StartCoroutine(CheckIfConnectedCoroutine());
            }
        }
        CheckSceneSettings();
    }

    IEnumerator CheckIfConnectedCoroutine()
    {
        yield return new WaitForSecondsRealtime(5f);
        if (!_uiInput.gameObject.activeSelf)
        {
            DisableAllInput(true, true);
            _uiInput.gameObject.SetActive(true);
            _disconnectMenu.Show();
        }
    }

    public PlayerStatsSO[] GetPlayerStats()
    {
        List<PlayerStatsSO> playerStats = new List<PlayerStatsSO>();
        playerStats.Add(_playerStats[SceneSettings.PlayerOne]);
        playerStats.Add(_playerStats[SceneSettings.PlayerTwo]);
        return playerStats.ToArray();
    }
    public AssistStatsSO[] GetAssistStats()
    {
        List<AssistStatsSO> assistStats = new List<AssistStatsSO>();
        assistStats.Add(_assists[SceneSettings.AssistOne]);
        assistStats.Add(_assists[SceneSettings.AssistTwo]);
        return assistStats.ToArray();
    }

    public float[] GetSpawnPositions()
    {
        List<float> spawnPositions = new List<float>();
        spawnPositions.Add(_spawnPositionsX[0]);
        spawnPositions.Add(_spawnPositionsX[1]);
        return spawnPositions.ToArray();
    }

    public void InitializePlayers(GameObject playerOneObject, GameObject playerTwoObject)
    {
        playerOneObject.GetComponent<Player>().playerStats = _playerStats[SceneSettings.PlayerOne];
        playerTwoObject.GetComponent<Player>().playerStats = _playerStats[SceneSettings.PlayerTwo];
        Time.timeScale = GameplayManager.Instance.GameSpeed;
        PlayerOneController = playerOneObject.GetComponent<BrainController>();
        PlayerTwoController = playerTwoObject.GetComponent<BrainController>();
        PlayerOne = playerOneObject.GetComponent<Player>();
        PlayerTwo = playerTwoObject.GetComponent<Player>();
        playerOneObject.GetComponent<CpuController>().SetOtherPlayer(playerTwoObject.transform);
        playerTwoObject.GetComponent<CpuController>().SetOtherPlayer(playerOneObject.transform);
        playerOneObject.SetActive(true);
        playerTwoObject.SetActive(true);
        if (SceneSettings.ControllerOne != null && _controllerOneType != ControllerTypeEnum.Cpu)
        {
            PlayerOneController.SetControllerToPlayer(0);
        }
        else
        {
            PlayerOneController.SetControllerToCpu(0);
        }
        if (SceneSettings.ControllerTwo != null && _controllerTwoType != ControllerTypeEnum.Cpu)
        {
            PlayerTwoController.SetControllerToPlayer(1);
        }
        else
        {
            PlayerTwoController.SetControllerToCpu(1);
        }
        PlayerOne.SetController();
        PlayerTwo.SetController();
        _playerOneAnimator = PlayerOne.transform.GetChild(1).GetComponent<PlayerAnimator>();
        _playerTwoAnimator = PlayerTwo.transform.GetChild(1).GetComponent<PlayerAnimator>();
        _playerOneAnimator.SetSpriteLibraryAsset(SceneSettings.ColorOne);
        if (SceneSettings.ColorTwo == SceneSettings.ColorOne && PlayerOne.PlayerStats.characterName == PlayerTwo.PlayerStats.characterName)
        {
            if (SceneSettings.ColorOne == 3)
            {
                SceneSettings.ColorTwo = 0;
            }
            else
            {
                SceneSettings.ColorTwo++;
            }
        }
        _playerTwoAnimator.SetSpriteLibraryAsset(SceneSettings.ColorTwo);
        _trainingMenu.ConfigurePlayers(PlayerOne, PlayerTwo);
        PlayerOneController.IsPlayerOne = true;
        PlayerOne.SetPlayerUI(_playerOneUI);
        PlayerTwo.SetPlayerUI(_playerTwoUI);
        PlayerOne.SetAssist(_assists[SceneSettings.AssistOne]);
        PlayerTwo.SetAssist(_assists[SceneSettings.AssistTwo]);
        PlayerOne.SetOtherPlayer(PlayerTwo);
        PlayerOne.IsPlayerOne = true;
        if (SceneSettings.ControllerOne != null && _controllerOneType != ControllerTypeEnum.Cpu)
        {
            PlayerOneController.ControllerInputName = SceneSettings.ControllerOne.displayName;
            PlayerOneController.InputDevice = SceneSettings.ControllerOne;
        }
        else
            PlayerOneController.ControllerInputName = ControllerTypeEnum.Cpu.ToString();
        PlayerTwo.SetOtherPlayer(PlayerOne);
        PlayerTwo.IsPlayerOne = false;
        if (SceneSettings.ControllerTwo != null && _controllerTwoType != ControllerTypeEnum.Cpu)
        {
            PlayerTwoController.ControllerInputName = SceneSettings.ControllerTwo.displayName;
            PlayerTwoController.InputDevice = SceneSettings.ControllerTwo;
        }
        else
            PlayerTwoController.ControllerInputName = ControllerTypeEnum.Cpu.ToString();
        PlayerOne.name = $"{_playerStats[SceneSettings.PlayerOne].name}({SceneSettings.ControllerOne})_player";
        PlayerTwo.name = $"{_playerStats[SceneSettings.PlayerTwo].name}({SceneSettings.ControllerTwo})_player";
        PlayerOne.GetComponent<InputBuffer>().Initialize(_inputHistories[0], _inputDisplays[0]);
        PlayerTwo.GetComponent<InputBuffer>().Initialize(_inputHistories[1], _inputDisplays[1]);
        string inputSchemeOne = "";
        string inputSchemeTwo = "";
        if (SceneSettings.ControllerOne != null && SceneSettings.ControllerOneScheme != ControllerTypeEnum.Cpu.ToString())
        {
            if (PlayerOneController.InputDevice != null)
                inputSchemeOne = PlayerOneController.InputDevice.displayName;
        }
        if (SceneSettings.ControllerTwo != null && SceneSettings.ControllerTwoScheme != ControllerTypeEnum.Cpu.ToString())
        {
            if (PlayerTwoController.InputDevice != null)
                inputSchemeTwo = PlayerTwoController.InputDevice.displayName;
        }
        if (inputSchemeOne.Contains("Keyboard"))
        {
            _keyboardPrompts.SetActive(true);
            _xboxPrompts.SetActive(false);
            _controllerPrompts.SetActive(false);
        }
        else if (inputSchemeOne.Contains("Xbox"))
        {
            _keyboardPrompts.SetActive(false);
            _xboxPrompts.SetActive(true);
            _controllerPrompts.SetActive(false);
        }
        else
        {
            _keyboardPrompts.SetActive(false);
            _xboxPrompts.SetActive(false);
            _controllerPrompts.SetActive(true);
        }
        _playerOneInput = PlayerOne.GetComponent<PlayerInput>();
        _playerTwoInput = PlayerTwo.GetComponent<PlayerInput>();
        if (SceneSettings.ControllerOneScheme == "Keyboard" && SceneSettings.ControllerTwoScheme == "Keyboard"
        && PlayerOneController.ControllerInputName == "Keyboard" && PlayerTwoController.ControllerInputName == "Keyboard")
        {
            SceneSettings.ControllerOneScheme = "Keyboard";
            SceneSettings.ControllerTwoScheme = "KeyboardTwo";
            _playerOneInput.SwitchCurrentControlScheme(SceneSettings.ControllerOneScheme, PlayerOneController.InputDevice);
            _playerTwoInput.SwitchCurrentControlScheme(SceneSettings.ControllerTwoScheme, PlayerTwoController.InputDevice);
        }
        else
        {
            if (PlayerOneController.InputDevice != null && _controllerOneType != ControllerTypeEnum.Cpu)
            {
                _playerOneInput.SwitchCurrentControlScheme(SceneSettings.ControllerOneScheme, PlayerOneController.InputDevice);
            }
            if (PlayerTwoController.InputDevice != null && _controllerTwoType != ControllerTypeEnum.Cpu)
            {
                _playerTwoInput.SwitchCurrentControlScheme(SceneSettings.ControllerTwoScheme, PlayerTwoController.InputDevice);
            }
        }
        _inputHistories[0].PlayerController = PlayerOne.GetComponent<PlayerController>();
        _inputHistories[1].PlayerController = PlayerTwo.GetComponent<PlayerController>();
        TargetGroup.AddMember(PlayerOne.CameraPoint, 0.5f, 0.5f);
        TargetGroup.AddMember(PlayerTwo.CameraPoint, 0.5f, 0.5f);
    }

    public void SetCameraTargets(float targetOne, float targetTwo, float time = 0.12f)
    {
        _targetGroup.m_Targets[0].weight = targetOne;
        StartCoroutine(SetCameraTargetsCoroutine(targetOne, targetTwo, time));
    }

    private IEnumerator SetCameraTargetsCoroutine(float targetOne, float targetTwo, float time)
    {
        float elapsedTime = 0;
        float waitTime = time;
        float currentTargetOne = _targetGroup.m_Targets[0].weight;
        float currentTargetTwo = _targetGroup.m_Targets[1].weight;
        while (elapsedTime < waitTime)
        {
            _targetGroup.m_Targets[0].weight = Mathf.Lerp(currentTargetOne, targetOne, elapsedTime / waitTime);
            _targetGroup.m_Targets[1].weight = Mathf.Lerp(currentTargetTwo, targetTwo, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _targetGroup.m_Targets[0].weight = targetOne;
        _targetGroup.m_Targets[1].weight = targetTwo;
        yield return null;
    }

    private void CheckInstance()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void CheckSceneSettings()
    {
        for (int i = 0; i < _stages.Length; i++)
        {
            _stages[i].SetActive(false);
        }
        _currentStage = _stages[SceneSettings.StageIndex];
        foreach (Transform stageColor in _currentStage.transform)
        {
            stageColor.gameObject.SetActive(false);
        }
        _currentStage.SetActive(true);
        int stageColorIndex = SceneSettings.Bit1 ? 1 : 0;
        _currentStage.transform.GetChild(stageColorIndex).gameObject.SetActive(true);
    }

    public void ActivateCpus()
    {
        if (IsTrainingMode && PlayerOne != null)
        {
            PlayerOneController.ActivateCpu();
            PlayerTwoController.ActivateCpu();
        }
    }

    public void DeactivateCpus()
    {
        if (IsTrainingMode && PlayerOne != null)
        {
            PlayerOneController.DeactivateCpu();
            PlayerTwoController.DeactivateCpu();
        }
    }

    public void MaxHealths()
    {
        if (IsTrainingMode && PlayerOne != null)
        {
            PlayerOne.MaxHealthStats();
            PlayerTwo.MaxHealthStats();
        }
    }


    public void SetupGame()
    {
        GameSimulation._players[0].player = GameplayManager.Instance.PlayerOne;
        GameSimulation._players[1].player = GameplayManager.Instance.PlayerTwo;
        _uiInput.gameObject.SetActive(true);
        if (SceneSettings.MusicName == "Random")
        {
            CurrentMusic = _musicAudio.SoundGroup("Music").PlayInRandom();
        }
        else
        {
            CurrentMusic = _musicAudio.SoundGroup("Music").Sound(SceneSettings.MusicName);
            CurrentMusic.Play();
        }
        if (_isTrainingMode)
        {
            _networkCanvas.SetActive(!NetworkInput.IS_LOCAL);
            _cachedOneResetPosition = PlayerOne.GetComponent<PlayerMovement>().Physics.Position;
            _cachedTwoResetPosition = PlayerTwo.GetComponent<PlayerMovement>().Physics.Position;
            _countdownText.gameObject.SetActive(false);
            _hearts[0].gameObject.SetActive(false);
            _hearts[1].gameObject.SetActive(false);
            _winsImage.SetActive(false);
            _trainingPrompts.gameObject.SetActive(true);
            HasGameStarted = true;
            StartTrainingRound();
            GameSimulation.Run = true;
        }
        else
        {
            _musicMenu.ShowMusicMenu(CurrentMusic.name);
            _inputHistories[0].transform.GetChild(0).gameObject.SetActive(false);
            _inputHistories[1].transform.GetChild(0).gameObject.SetActive(false);
            _trainingPrompts.gameObject.SetActive(false);
            StartIntro();
        }
        ReplayManager.Instance.StartReplay();
    }

    public void SetOnlineNames(string playerOne, string playerTwo)
    {
        _playerOneUI.SetPlayerName(playerOne);
        _playerTwoUI.SetPlayerName(playerTwo);
    }

    void FixedUpdate()
    {
        RunHitStop();
        RunReady();
        RunRoundOver();
    }

    public void SetCountdown(int timer)
    {
        if (!_isTrainingMode && GameSimulation.Run)
        {
            _countdownText.text = timer.ToString();
            if (timer <= 0)
            {
                _timerMainAnimator.Rebind();
                RoundOver(true);
            }
            else if (timer <= 10)
                _timerMainAnimator.SetTrigger("TimerLow");
        }
    }

    public void SkipIntro()
    {
        if (IsDialogueRunning || !NetworkInput.IS_LOCAL)
        {
            ReplayManager.Instance.Skip = DemonicsWorld.Frame;
            _playerOneDialogue.StopDialogue();
            _playerTwoDialogue.StopDialogue();
            StartRound();
            _introUI.SkipIntro();
            IsDialogueRunning = false;
        }
    }

    public void StartIntro()
    {
        PlayerOne.ResetPlayer(new Vector2(_spawnPositionsX[0], (float)DemonicsPhysics.GROUND_POINT));
        PlayerTwo.ResetPlayer(new Vector2(_spawnPositionsX[1], (float)DemonicsPhysics.GROUND_POINT));
        for (int i = 0; i < _arcanaObjects.Length; i++)
        {
            _arcanaObjects[i].SetActive(false);
        }
        _introUI.SetPlayerNames(_playerStats[SceneSettings.PlayerOne].characterName.ToString(), _playerStats[SceneSettings.PlayerTwo].characterName.ToString());
        _playerOneDialogue.Initialize(true, _playerStats[SceneSettings.PlayerOne]._dialogue, _playerStats[SceneSettings.PlayerTwo].characterName);
        _playerTwoDialogue.Initialize(false, _playerStats[SceneSettings.PlayerTwo]._dialogue, _playerStats[SceneSettings.PlayerOne].characterName);
        _introUI.StartIntro();
    }

    private void MatchOver()
    {
        _finalRound = false;
        _winnerNameText.text = "";
        _readyText.text = "";
        _currentRound = 1;
        MouseSetup.Instance.SetCursor(true);
        if (SceneSettings.ReplayMode)
        {
            GameplayManager.Instance.PausedController = PlayerOneController;
            DisableAllInput(true);
            _matchOverReplayMenu.Show();
        }
        else
        {
            if (!NetworkInput.IS_LOCAL)
            {
                if (GameManager.Instance.IsRunning)
                {
                    // GameManager.Instance.Shutdown();
                }
                GameplayManager.Instance.PausedController = PlayerOneController;
                DisableAllInput(true);
                _matchOverOnlineMenu.Show();
            }
            else
            {
                GameplayManager.Instance.PausedController = PlayerOneController;
                DisableAllInput(true);
                _matchOverMenu.Show();
            }
            // if (_controllerOneType != ControllerTypeEnum.Cpu && _controllerTwoType != ControllerTypeEnum.Cpu)
            // {
            //     if (_controllerTwoType != ControllerTypeEnum.Keyboard)
            //     {
            //         _matchOverSecondMenu.Show();
            //     }
            // }
        }
        if (SceneSettings.ControllerOne != null && SceneSettings.ControllerTwo != null)
        {
            ReplayManager.Instance.SaveReplay();
        }
        Time.timeScale = 0;
    }

    public void DisconnectOnline()
    {
        GameManager.Instance.DisconnectPlayer(SceneSettings.OnlineIndex);
    }

    public void ConnectOnline()
    {
        _fadeHandler.StartFadeTransition(true, 0.01f);
        PlayerOne.ResetLives();
        PlayerTwo.ResetLives();
        _targetGroup.m_Targets[0].weight = 0.5f;
        _targetGroup.m_Targets[1].weight = 0.5f;
        EnableAllInput();
        Time.timeScale = 1;
        GameManager.Instance.Shutdown();
        _matchOverOnlineMenu.Hide();
        _connectionWidget.RematchConnection();
    }

    public virtual void StartRound()
    {
        _networkCanvas.SetActive(!NetworkInput.IS_LOCAL);
        if (!IsDialogueRunning)
            _fadeHandler.StartFadeTransition(false);
        if (SceneSettings.ReplayMode)
            ReplayManager.Instance.ShowReplayPrompts();
        _timerMainAnimator.Rebind();
        IsDialogueRunning = false;
        for (int i = 0; i < _arcanaObjects.Length; i++)
        {
            _arcanaObjects[i].SetActive(true);
        }
        _countdown = _countdownTime;
        _countdownText.text = Mathf.Round(_countdown).ToString();
        _targetGroup.m_Targets[0].weight = 0.5f;
        _targetGroup.m_Targets[1].weight = 0.5f;
        PlayerOne.ResetPlayer(new Vector2(_spawnPositionsX[0], (float)DemonicsPhysics.GROUND_POINT));
        PlayerTwo.ResetPlayer(new Vector2(_spawnPositionsX[1], (float)DemonicsPhysics.GROUND_POINT));
        PlayerOne.StopComboTimer();
        PlayerTwo.StopComboTimer();
        _showFrame = 30;
        _readyFrame = 60;
        _readyEndFrame = 60;
        _showEnd = false;
        _readyEnd = false;
        _startRun = true;
    }

    private void StartTrainingRound()
    {
        PlayerOne.ResetPlayer(new Vector2(_spawnPositionsX[0], (float)DemonicsPhysics.GROUND_POINT));
        PlayerTwo.ResetPlayer(new Vector2(_spawnPositionsX[1], (float)DemonicsPhysics.GROUND_POINT));
        PlayerOne.ResetLives();
        PlayerTwo.ResetLives();
        _playerOneUI.FadeIn();
        _playerTwoUI.FadeIn();
        _timerAnimator.SetTrigger("FadeIn");
        _infiniteTime.SetActive(true);
        HasGameStarted = true;
    }
    private int _showFrame = 30;
    private int _readyFrame = 60;
    private int _readyEndFrame = 60;
    private bool _showEnd;
    private bool _readyEnd;
    private bool _startRun;
    private void RunReady()
    {
        if (_startRun)
        {
            Time.timeScale = GameSpeed;
            if (DemonicsWorld.WaitFramesOnce(ref _showFrame))
            {
                _showEnd = true;
                _uiAudio.Sound("TextSound").Play();
                _readyAnimator.Play("RoundShow");
                if (_currentRound == 4)
                {
                    _finalRound = true;
                }
                for (int i = 0; i < _readyObjects.Length; i++)
                {
                    _readyObjects[i].SetActive(true);
                }
                if (_finalRound)
                {
                    _readyText.text = $"Final Round";
                }
                else
                {
                    _readyText.text = $"Round {_currentRound}";
                }
            }
            if (_showEnd)
            {
                if (DemonicsWorld.WaitFramesOnce(ref _readyFrame))
                {
                    _readyAnimator.Play("FightShow");
                    _uiAudio.Sound("TextSound").Play();
                    _readyText.text = "Fight!";
                    _countdownText.gameObject.SetActive(true);
                    _playerOneUI.FadeIn();
                    _playerTwoUI.FadeIn();
                    _timerAnimator.SetTrigger("FadeIn");
                    _readyEnd = true;
                    if (_currentRound == 1)
                        if (SceneSettings.IsOnline)
                            if (SceneSettings.OnlineIndex == 0)
                                _playerOneUI.ShowYouOverhead();
                            else
                                _playerTwoUI.ShowYouOverhead();
                }
                if (_readyEnd)
                {
                    if (DemonicsWorld.WaitFramesOnce(ref _readyEndFrame))
                    {
                        _startRun = false;
                        for (int i = 0; i < _readyObjects.Length; i++)
                        {
                            _readyObjects[i].SetActive(false);
                        }
                        _readyText.text = "";
                        PlayerOneController.ActivateInput();
                        PlayerTwoController.ActivateInput();
                        HasGameStarted = true;
                        if (_currentRound == 1)
                        {
                            if (SceneSettings.ReplayMode)
                            {
                                ReplayManager.Instance.StartLoadReplay();
                            }
                        }
                    }
                }
            }
        }
    }

    public virtual void RoundOver(bool timeout)
    {
        if (HasGameStarted)
        {
            _playerOneWon = false;
            _playerTwoWon = false;
            if (_isTrainingMode)
            {
                _roundOverTrainingCoroutine = StartCoroutine(RoundOverTrainingCoroutine());
            }
            else
            {
                string roundOverCause;
                if (GameSimulation._players[0].health <= 0 && GameSimulation._players[1].health <= 0
                || GameSimulation._players[0].health > 0 && GameSimulation._players[1].health > 0)
                {
                    roundOverCause = "DOUBLE KO";
                    if (PlayerOne.Lives > 1 || PlayerTwo.Lives > 1)
                    {
                        if (GameSimulation._players[0].health < GameSimulation._players[1].health)
                        {
                            _playerTwoWon = true;
                            PlayerOne.LoseLife();
                        }
                        else if (GameSimulation._players[1].health < GameSimulation._players[0].health)
                        {
                            _playerOneWon = true;
                            PlayerTwo.LoseLife();
                        }
                        else
                        {
                            PlayerOne.LoseLife();
                            PlayerTwo.LoseLife();
                        }
                    }
                    else if (PlayerOne.Lives == 1 && PlayerTwo.Lives == 1 && _finalRound)
                    {
                        if (GameSimulation._players[0].health < GameSimulation._players[1].health)
                        {
                            _playerTwoWon = true;
                            PlayerOne.LoseLife();
                        }
                        else if (GameSimulation._players[1].health < GameSimulation._players[0].health)
                        {
                            _playerOneWon = true;
                            PlayerTwo.LoseLife();
                        }
                        else
                        {
                            PlayerOne.LoseLife();
                            PlayerTwo.LoseLife();
                        }
                    }
                    else
                    {
                        _finalRound = true;
                    }
                }
                else
                {
                    if (PlayerOne.PlayerStats.maxHealth == GameSimulation._players[0].health && GameSimulation._players[1].health <= 0
                        || PlayerTwo.PlayerStats.maxHealth == GameSimulation._players[1].health && GameSimulation._players[0].health <= 0)
                    {
                        roundOverCause = "PERFECT\nKO";
                    }
                    else
                    {
                        roundOverCause = "KO";
                    }
                    if (GameSimulation._players[0].health > GameSimulation._players[1].health)
                    {
                        _playerOneWon = true;
                        PlayerTwo.LoseLife();

                    }
                    else if (GameSimulation._players[1].health > GameSimulation._players[0].health)
                    {
                        _playerTwoWon = true;
                        PlayerOne.LoseLife();
                    }
                }
                if (timeout)
                {
                    _readyText.text = "TIME UP";
                }
                else
                {
                    _readyText.text = roundOverCause;
                }
                for (int i = 0; i < _readyObjects.Length; i++)
                {
                    _readyObjects[i].SetActive(true);
                }
                _uiAudio.Sound("TextSound").Play();
                _readyAnimator.Play("ReadyTextShow");
                _roundOver = true;
                _roundOverSecondFrame = 120;
                _roundOverThirdFrame = 120;
                _roundOverFourthFrame = 130;
                _roundOverSecond = false;
                _roundOverThird = false;
                _startRoundOver = false;
                _timeout = timeout;
                HasGameStarted = false;
                _uiAudio.Sound("Round").Play();
                _startRoundOver = true;
                PlayerOneController.DeactivateInput();
                PlayerTwoController.DeactivateInput();
            }
        }
    }

    private int _roundOverSecondFrame = 60;
    private int _roundOverThirdFrame = 120;
    private int _roundOverFourthFrame = 130;
    private bool _roundOver;
    private bool _roundOverSecond;
    private bool _roundOverThird;
    private bool _startRoundOver;
    private bool _timeout;
    private bool _playerOneWon;
    private bool _playerTwoWon;
    private void RunRoundOver()
    {
        if (_startRoundOver)
        {
            if (_roundOver)
            {
                if (DemonicsWorld.WaitFramesOnce(ref _roundOverSecondFrame))
                {
                    _uiAudio.Sound("TextSound").Play();
                    _readyAnimator.Play("ReadyTextShow");
                    _currentRound++;
                    if (_playerOneWon)
                    {
                        _readyText.text = "WINNER";
                        _winnerNameText.text = $"{_playerOneUI.PlayerName}";
                    }
                    else if (_playerTwoWon)
                    {
                        _readyText.text = "WINNER";
                        _winnerNameText.text = $"{_playerTwoUI.PlayerName}";
                    }
                    else
                    {
                        _readyText.text = "DRAW";
                    }
                    _roundOverSecond = true;
                }
                if (_roundOverSecond)
                {
                    if (DemonicsWorld.WaitFramesOnce(ref _roundOverThirdFrame))
                    {
                        if (_playerOneWon)
                            StartCoroutine(CameraCenterCoroutine(1));
                        else if (_playerTwoWon)
                            StartCoroutine(CameraCenterCoroutine(0));
                        else
                        {

                        }
                        _roundOverThird = true;
                    }
                    if (_roundOverThird)
                    {
                        if (DemonicsWorld.WaitFramesOnce(ref _roundOverFourthFrame))
                        {
                            for (int i = 0; i < _readyObjects.Length; i++)
                            {
                                _readyObjects[i].SetActive(false);
                            }
                            _winnerNameText.text = "";
                            _readyText.text = "";
                            if (_playerOneWon)
                            {
                                if (PlayerTwo.Lives == 0)
                                {
                                    _playerOneWins++;
                                    _winsText.text = $"{_playerOneWins} - {_playerTwoWins}";
                                    MatchOver();
                                }
                                else
                                {
                                    _fadeHandler.StartFadeTransition(true);
                                    _fadeHandler.onFadeEnd.AddListener(() => StartRound());
                                }
                            }
                            else if (_playerTwoWon)
                            {
                                if (PlayerOne.Lives == 0)
                                {
                                    _playerTwoWins++;
                                    _winsText.text = $"{_playerOneWins} - {_playerTwoWins}";
                                    MatchOver();
                                }
                                else
                                {
                                    _fadeHandler.StartFadeTransition(true);
                                    _fadeHandler.onFadeEnd.AddListener(() => StartRound());
                                }
                            }
                            else
                            {
                                if (PlayerTwo.Lives == 0 || PlayerOne.Lives == 0)
                                {
                                    MatchOver();
                                }
                                else
                                {
                                    _fadeHandler.StartFadeTransition(true);
                                    _fadeHandler.onFadeEnd.AddListener(() => StartRound());
                                }
                            }
                            _startRoundOver = false;
                        }
                    }
                }
            }
        }
    }

    IEnumerator CameraCenterCoroutine(int player)
    {
        yield return new WaitForSeconds(0.2f);
        float waitTime = 0.5f;
        float elapsedTime = 0;
        while (elapsedTime < waitTime)
        {
            _targetGroup.m_Targets[player].weight = Mathf.Lerp(0.5f, 0, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator RoundOverTrainingCoroutine()
    {
        HasGameStarted = false;
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
        _fadeHandler.StartFadeTransition(true);
        _fadeHandler.onFadeEnd.AddListener(() =>
        {
            _fadeHandler.StartFadeTransition(false);
            StartTrainingRound();
        });
    }

    public void SwitchCharacters()
    {
        if (IsTrainingMode && _canCallSwitchCharacter && Time.timeScale > 0)
            StartCoroutine(SwitchCharactersCoroutine());
    }

    IEnumerator SwitchCharactersCoroutine()
    {
        PlayerStatsSO playerStatsOneTemp = PlayerOne.PlayerStats;
        PlayerOne.PlayerStats = PlayerTwo.PlayerStats;
        PlayerTwo.PlayerStats = playerStatsOneTemp;
        PlayerTwoController.IsPlayerOne = !PlayerTwoController.IsPlayerOne;
        PlayerOneController.IsPlayerOne = !PlayerOneController.IsPlayerOne;
        _playerOneUI.ShowPlayerIcon();
        _playerTwoUI.ShowPlayerIcon();
        _canCallSwitchCharacter = false;
        if (_hasSwitchedCharacters)
        {
            if (PlayerOneController.ControllerInputName != ControllerTypeEnum.Cpu.ToString() && PlayerTwoController.ControllerInputName == ControllerTypeEnum.Cpu.ToString())
            {
                PlayerOneController.SetControllerToCpu(0);
                PlayerTwoController.SetControllerToPlayer(1);
                _playerOneInput.enabled = false;
                _playerTwoInput.enabled = true;
            }
            else if (PlayerTwoController.ControllerInputName != ControllerTypeEnum.Cpu.ToString() && PlayerOneController.ControllerInputName == ControllerTypeEnum.Cpu.ToString())
            {
                PlayerOneController.SetControllerToPlayer(0);
                PlayerTwoController.SetControllerToCpu(1);
                _playerOneInput.enabled = true;
                _playerTwoInput.enabled = false;
            }
            string a = PlayerOneController.ControllerInputName;
            string b = PlayerTwoController.ControllerInputName;
            PlayerOneController.ControllerInputName = b;
            PlayerTwoController.ControllerInputName = a;
            if (SceneSettings.ControllerOne != null && _playerOneInput.enabled)
            {
                _playerOneInput.SwitchCurrentControlScheme(SceneSettings.ControllerOneScheme, PlayerOneController.InputDevice);
            }
            if (SceneSettings.ControllerTwo != null && _playerTwoInput.enabled)
            {
                _playerTwoInput.SwitchCurrentControlScheme(SceneSettings.ControllerTwoScheme, PlayerTwoController.InputDevice);
            }
        }
        else
        {
            if (PlayerOneController.ControllerInputName != ControllerTypeEnum.Cpu.ToString() && PlayerTwoController.ControllerInputName == ControllerTypeEnum.Cpu.ToString())
            {
                PlayerOneController.SetControllerToCpu(0);
                PlayerTwoController.SetControllerToPlayer(1);
                _playerOneInput.enabled = false;
                _playerTwoInput.enabled = true;
            }
            else if (PlayerTwoController.ControllerInputName != ControllerTypeEnum.Cpu.ToString() && PlayerOneController.ControllerInputName == ControllerTypeEnum.Cpu.ToString())
            {
                PlayerOneController.SetControllerToPlayer(0);
                PlayerTwoController.SetControllerToCpu(1);
                _playerOneInput.enabled = true;
                _playerTwoInput.enabled = false;
            }
            string a = PlayerOneController.ControllerInputName;
            string b = PlayerTwoController.ControllerInputName;
            PlayerOneController.ControllerInputName = b;
            PlayerTwoController.ControllerInputName = a;
            if (SceneSettings.ControllerTwo != null && _playerOneInput.enabled)
            {
                _playerOneInput.SwitchCurrentControlScheme(SceneSettings.ControllerTwoScheme, PlayerTwoController.InputDevice);
            }
            if (SceneSettings.ControllerOne != null && _playerTwoInput.enabled)
            {
                _playerTwoInput.SwitchCurrentControlScheme(SceneSettings.ControllerOneScheme, PlayerOneController.InputDevice);
            }
        }

        _hasSwitchedCharacters = !_hasSwitchedCharacters;
        yield return new WaitForSecondsRealtime(0.1f);
        _canCallSwitchCharacter = true;
    }

    public virtual void ResetRound(Vector2 movementInput)
    {
        if (_isTrainingMode && Time.timeScale > 0)
        {
            _fadeHandler.StartFadeTransition(true);
            _fadeHandler.onFadeEnd.AddListener(() =>
            {
                HasGameStarted = true;
                Time.timeScale = GameSpeed;
                if (_roundOverTrainingCoroutine != null)
                {
                    StopCoroutine(_roundOverTrainingCoroutine);
                }
                PlayerOne.ResetLives();
                PlayerTwo.ResetLives();
                if (_cachedOneResetPosition == DemonVector2.Zero)
                {
                    _cachedOneResetPosition = new DemonVector2((DemonFloat)_spawnPositionsX[0], DemonicsPhysics.GROUND_POINT);
                    _cachedTwoResetPosition = new DemonVector2((DemonFloat)_spawnPositionsX[1], DemonicsPhysics.GROUND_POINT);
                }
                DemonVector2 playerOneResetPosition = _cachedOneResetPosition;
                DemonVector2 playerTwoResetPosition = _cachedTwoResetPosition;
                ObjectPoolingManager.Instance.DisableAllObjects();

                if (movementInput.y == -1)
                {
                    playerOneResetPosition = new DemonVector2((DemonFloat)_spawnPositionsX[0], DemonicsPhysics.GROUND_POINT);
                    playerTwoResetPosition = new DemonVector2((DemonFloat)_spawnPositionsX[1], DemonicsPhysics.GROUND_POINT);
                }
                else if (movementInput.x == 1)
                {
                    playerOneResetPosition = new DemonVector2((DemonFloat)_spawnPositionsX[0] + _leftSpawn, DemonicsPhysics.GROUND_POINT);
                    playerTwoResetPosition = new DemonVector2((DemonFloat)_spawnPositionsX[1] + _rightSpawn, DemonicsPhysics.GROUND_POINT);
                }
                else if (movementInput.x == -1)
                {
                    playerOneResetPosition = new DemonVector2((DemonFloat)_spawnPositionsX[0] - _rightSpawn, DemonicsPhysics.GROUND_POINT);
                    playerTwoResetPosition = new DemonVector2((DemonFloat)_spawnPositionsX[1] - _leftSpawn, DemonicsPhysics.GROUND_POINT);
                }
                else if (movementInput.y == 1)
                {
                    _reverseReset = !_reverseReset;
                }
                if (_reverseReset)
                {
                    PlayerOne.ResetPlayer(new Vector2((float)playerTwoResetPosition.x, (float)playerTwoResetPosition.y));
                    PlayerTwo.ResetPlayer(new Vector2((float)playerOneResetPosition.x, (float)playerOneResetPosition.y));
                }
                else
                {
                    PlayerOne.ResetPlayer(new Vector2((float)playerOneResetPosition.x, (float)playerOneResetPosition.y));
                    PlayerTwo.ResetPlayer(new Vector2((float)playerTwoResetPosition.x, (float)playerTwoResetPosition.y));
                }
                GameSimulation._players[0].CurrentState.ResetPlayerTraining(GameSimulation._players[0]);
                GameSimulation._players[1].CurrentState.ResetPlayerTraining(GameSimulation._players[1]);
                _cachedOneResetPosition = playerOneResetPosition;
                _cachedTwoResetPosition = playerTwoResetPosition;

                _fadeHandler.StartFadeTransition(false);
            });
        }
    }

    public void StartMatch()
    {
        MouseSetup.Instance.SetCursor(false);
        Time.timeScale = 1;
        GameplayManager.Instance.EnableAllInput();
        if (SceneSettings.RandomStage)
        {
            _currentStage.SetActive(false);
            int randomStageIndex = UnityEngine.Random.Range(0, _stages.Length);
            _currentStage = _stages[randomStageIndex];
            _currentStage.SetActive(true);
        }
        _matchOverMenu.Hide();
        PlayerOne.ResetLives();
        PlayerTwo.ResetLives();
        CurrentMusic.Stop();
        if (SceneSettings.MusicName == "Random")
        {
            CurrentMusic = _musicAudio.SoundGroup("Music").PlayInRandom();
        }
        else
        {
            CurrentMusic = _musicAudio.SoundGroup("Music").Sound(SceneSettings.MusicName);
            CurrentMusic.Play();
        }
        StartRound();
    }

    public void PauseMusic()
    {
        CurrentMusic.Pause();
    }

    public void PlayMusic()
    {
        CurrentMusic.Play();
    }

    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
    public void DisableAllInput(bool isPlayerOne = false, bool skipSwitch = false)
    {
        PlayerInput = _uiInput;
        if (_playerOneInput.enabled)
        {
            if (PlayerOneController.ControllerInputName != ControllerTypeEnum.Cpu.ToString())
            {
                _playerOneInput.enabled = false;
            }
        }

        if (_playerTwoInput.enabled)
        {
            if (PlayerTwoController.ControllerInputName != ControllerTypeEnum.Cpu.ToString())
            {
                _playerTwoInput.enabled = false;
            }
        }
        if (skipSwitch)
            return;
        if (isPlayerOne)
        {
            _uiInput.SwitchCurrentControlScheme(SceneSettings.ControllerOneScheme, PlayerOneController.InputDevice);
        }
        else
        {
            _uiInput.SwitchCurrentControlScheme(SceneSettings.ControllerTwoScheme, PlayerTwoController.InputDevice);
        }
    }
    public PlayerInput PlayerInput { get; set; }
    public void EnableAllInput()
    {
        if (!_playerOneInput.enabled)
        {
            if (PlayerOneController.ControllerInputName != ControllerTypeEnum.Cpu.ToString())
            {
                _playerOneInput.enabled = true;
                if (_hasSwitchedCharacters)
                {
                    _playerOneInput.SwitchCurrentControlScheme(SceneSettings.ControllerTwoScheme, PlayerTwoController.InputDevice);
                }
                else
                {
                    _playerOneInput.SwitchCurrentControlScheme(SceneSettings.ControllerOneScheme, PlayerOneController.InputDevice);
                }
            }
        }

        if (!_playerTwoInput.enabled)
        {
            if (PlayerTwoController.ControllerInputName != ControllerTypeEnum.Cpu.ToString())
            {
                _playerTwoInput.enabled = true;
                if (_hasSwitchedCharacters)
                {
                    _playerTwoInput.SwitchCurrentControlScheme(SceneSettings.ControllerOneScheme, PlayerOneController.InputDevice);
                }
                else
                {
                    _playerTwoInput.SwitchCurrentControlScheme(SceneSettings.ControllerTwoScheme, PlayerTwoController.InputDevice);
                }
            }
        }
    }

    public void AddHitstop(IHitstop hitstop)
    {
        _hitstopList.Add(hitstop);
    }

    public void GlobalHitstop(int hitstopFrames)
    {
        var hitstopItems = FindObjectsOfType<MonoBehaviour>().OfType<IHitstop>();
        foreach (IHitstop hitstop in hitstopItems)
        {
            _hitstopList.Add(hitstop);
        }
        HitStop(hitstopFrames);
    }
    private int _superFreezeOverFrame;
    private bool _superFreezeOver;
    public void SuperFreeze()
    {
        if (!IsTrainingMode)
        {
            GlobalHitstop(120);
        }
    }

    public int _hitstop;
    public void HitStop(int hitstopFrames)
    {
        if (hitstopFrames > 0)
        {
            for (int i = 0; i < _hitstopList.Count; i++)
            {
                if (!_hitstopList[i].IsInHitstop())
                {
                    _hitstopList[i].EnterHitstop();
                }
            }
            _hitstop = hitstopFrames;
        }
    }

    private void RunHitStop()
    {
        if (_hitstop > 0)
        {
            if (DemonicsWorld.WaitFramesOnce(ref _hitstop))
            {
                _hitstop = 0;
                for (int i = 0; i < _hitstopList.Count; i++)
                {
                    _hitstopList[i].ExitHitstop();
                }
                _hitstopList.Clear();
            }
        }
    }

    private void OnDisable()
    {
        if (keyboardTwo != null)
        {
            InputSystem.RemoveDevice(keyboardTwo);
        }
        GameSimulation.Run = false;
        ObjectPoolingManager.Instance.DestroyAllObjects();
    }

    void OnApplicationQuit()
    {
        if (GameManager.Instance.IsRunning)
            GameManager.Instance.Shutdown();
    }
}


