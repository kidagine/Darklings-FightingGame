using Cinemachine;
using Demonics.Sounds;
using Demonics.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[Header("Debug")]
	[SerializeField] private StageTypeEnum _stage = default;
	[SerializeField] private CharacterTypeEnum _characterOne = default;
	[SerializeField] private CharacterTypeEnum _characterTwo = default;
	[SerializeField] private AssistTypeEnum _assistOne = default;
	[SerializeField] private AssistTypeEnum _assistTwo = default;
	[SerializeField] private ControllerTypeEnum _controllerOne = default;
	[SerializeField] private ControllerTypeEnum _controllerTwo = default;
	[SerializeField] private MusicTypeEnum _music = default;
	[Range(0, 10)]
	[SerializeField] private int _playerOneSkin = default;
	[Range(0, 10)]
	[SerializeField] private int _playerTwoSkin = default;
	[SerializeField] private bool _isTrainingMode = default;
	[SerializeField] private bool _isOnlineMode = default;
	[SerializeField] private bool _1BitOn = default;
	[Range(1, 10)]
	[SerializeField] private int _gameSpeed = 1;
	[Header("Data")]
	[SerializeField] private Transform[] _spawnPositions = default;
	[SerializeField] private IntroUI _introUI = default;
	[SerializeField] protected PlayerUI _playerOneUI = default;
	[SerializeField] protected PlayerUI _playerTwoUI = default;
	[SerializeField] private PlayerDialogue _playerOneDialogue = default;
	[SerializeField] private PlayerDialogue _playerTwoDialogue = default;
	[SerializeField] private Animator _timerAnimator = default;
	[SerializeField] private Animator _introAnimator = default;
	[SerializeField] protected TextMeshProUGUI _countdownText = default;
	[SerializeField] protected TextMeshProUGUI _readyText = default;
	[SerializeField] protected TextMeshProUGUI _winnerNameText = default;
	[SerializeField] protected TextMeshProUGUI _winsText = default;
	[SerializeField] protected GameObject _keyboardPrompts = default;
	[SerializeField] protected GameObject _controllerPrompts = default;
	[SerializeField] protected GameObject[] _readyObjects = default;
	[SerializeField] protected GameObject[] _arcanaObjects = default;
	[SerializeField] protected GameObject _playerLocal = default;
	[SerializeField] protected GameObject _playerNetcode = default;
	[SerializeField] protected GameObject _infiniteTime = default;
	[SerializeField] protected GameObject _winsImage = default;
	[SerializeField] private GameObject _networkCanvas = default;
	[SerializeField] private GameObject[] _hearts = default;
	[SerializeField] private GameObject _trainingPrompts = default;
	[SerializeField] private InputHistory[] _inputHistories = default;
	[SerializeField] private PlayerStatsSO[] _playerStats = default;
	[SerializeField] private TrainingMenu _trainingMenu = default;
	[SerializeField] private GameObject[] _stages = default;
	[SerializeField] private AssistStatsSO[] _assists = default;
	[SerializeField] private BaseMenu _matchOverMenu = default;
	[SerializeField] private Animator _readyAnimator = default;
	[SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup = default;
	[SerializeField] private Audio _musicAudio = default;
	[SerializeField] private Audio _uiAudio = default;
	private PlayerMovement _playerMovementOne;
	private PlayerMovement _playerMovementTwo;
	protected BrainController _playerOneController;
	protected BrainController _playerTwoController;
	private PlayerAnimator _playerOneAnimator;
	private PlayerAnimator _playerTwoAnimator;
	private Coroutine _roundOverTrainingCoroutine;
	private Sound _currentMusic;
	private GameObject _currentStage;
	private Vector2 _cachedOneResetPosition;
	private Vector2 _cachedTwoResetPosition;
	private float _countdown;
	private int _currentRound = 1;
	private bool _reverseReset;
	private bool _hasSwitchedCharacters;
	private bool _canCallSwitchCharacter = true;
	private bool _isDialogueRunning;
	private int _playerOneWins;
	private int _playerTwoWins;

	public bool IsCpuOff { get; set; }
	public bool HasGameStarted { get; set; }
	public bool IsTrainingMode { get { return _isTrainingMode; } set { } }
	public bool InfiniteHealth { get; set; }
	public bool InfiniteArcana { get; set; }
	public bool InfiniteAssist { get; set; }
	public Player PlayerOne { get; private set; }
	public Player PlayerTwo { get; private set; }
	public static GameManager Instance { get; private set; }
	public float GameSpeed { get; set; }


	void Awake()
	{
		HasGameStarted = false;
		GameSpeed = _gameSpeed;
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1;
		CheckInstance();
		if (!SceneSettings.SceneSettingsDecide)
		{
			SceneSettings.ControllerOne = _controllerOne.ToString();
			SceneSettings.ControllerTwo = _controllerTwo.ToString();
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
			if (_isOnlineMode)
			{
				_networkCanvas.SetActive(true);
			}
		}
		else
		{
			_networkCanvas.SetActive(false);
			_isTrainingMode = SceneSettings.IsTrainingMode;
		}
		CheckSceneSettings();

		if (_isTrainingMode)
		{
			if (SceneSettings.ControllerOne == ControllerTypeEnum.ControllerOne.ToString() || SceneSettings.ControllerOne == ControllerTypeEnum.ControllerOne.ToString())
			{
				_controllerPrompts.SetActive(true);
				_keyboardPrompts.SetActive(false);
			}
			else
			{
				_controllerPrompts.SetActive(false);
				_keyboardPrompts.SetActive(true);
			}
		}

		if (_isOnlineMode)
		{

		}
		else
		{
			GameObject playerOneObject = Instantiate(_playerLocal);
			playerOneObject.GetComponent<PlayerStats>().PlayerStatsSO = _playerStats[SceneSettings.PlayerOne];
			GameObject playerTwoObject = Instantiate(_playerLocal);
			playerTwoObject.GetComponent<PlayerStats>().PlayerStatsSO = _playerStats[SceneSettings.PlayerTwo];
			InitializePlayers(playerOneObject, playerTwoObject);
		}
	}


	private void InitializePlayers(GameObject playerOneObject, GameObject playerTwoObject)
	{
		_playerOneController = playerOneObject.GetComponent<BrainController>();
		_playerTwoController = playerTwoObject.GetComponent<BrainController>();
		PlayerOne = playerOneObject.GetComponent<Player>();
		PlayerTwo = playerTwoObject.GetComponent<Player>();
		_playerMovementOne = playerOneObject.GetComponent<PlayerMovement>();
		_playerMovementTwo = playerTwoObject.GetComponent<PlayerMovement>();
		playerOneObject.GetComponent<CpuController>().SetOtherPlayer(playerTwoObject.transform);
		playerTwoObject.GetComponent<CpuController>().SetOtherPlayer(playerOneObject.transform);
		playerOneObject.SetActive(true);
		playerTwoObject.SetActive(true);
		PlayerOne.transform.position = _spawnPositions[0].position;
		PlayerTwo.transform.position = _spawnPositions[1].position;
		if (SceneSettings.ControllerOne != ControllerTypeEnum.Cpu.ToString())
		{
			_playerOneController.SetControllerToPlayer();
		}
		else
		{
			_playerOneController.SetControllerToCpu();
		}
		if (SceneSettings.ControllerTwo != ControllerTypeEnum.Cpu.ToString())
		{
			_playerTwoController.SetControllerToPlayer();
		}
		else
		{
			_playerTwoController.SetControllerToCpu();
		}
		PlayerOne.SetController();
		PlayerTwo.SetController();
		_playerMovementOne.SetController();
		_playerMovementTwo.SetController();
		_playerOneAnimator = PlayerOne.transform.GetChild(1).GetComponent<PlayerAnimator>();
		_playerTwoAnimator = PlayerTwo.transform.GetChild(1).GetComponent<PlayerAnimator>();
		PlayerOne.transform.GetChild(1).GetComponent<PlayerAnimationEvents>().SetTrainingMenu(_trainingMenu);
		PlayerTwo.transform.GetChild(1).GetComponent<PlayerAnimationEvents>().SetTrainingMenu(_trainingMenu);
		_playerOneAnimator.SetSpriteLibraryAsset(SceneSettings.ColorOne);
		if (SceneSettings.ColorTwo == SceneSettings.ColorOne && PlayerOne.PlayerStats.characterName == PlayerTwo.PlayerStats.characterName)
		{
			SceneSettings.ColorTwo++;
		}
		_playerTwoAnimator.SetSpriteLibraryAsset(SceneSettings.ColorTwo);
		_playerOneController.IsPlayerOne = true;
		PlayerOne.SetPlayerUI(_playerOneUI);
		PlayerTwo.SetPlayerUI(_playerTwoUI);
		PlayerOne.SetAssist(_assists[SceneSettings.AssistOne]);
		PlayerTwo.SetAssist(_assists[SceneSettings.AssistTwo]);
		PlayerOne.SetOtherPlayer(_playerMovementTwo);
		PlayerOne.IsPlayerOne = true;
		_playerOneController.ControllerInputName = SceneSettings.ControllerOne;
		PlayerTwo.SetOtherPlayer(_playerMovementOne);
		PlayerTwo.IsPlayerOne = false;
		_playerTwoController.ControllerInputName = SceneSettings.ControllerTwo;
		PlayerOne.name = $"{_playerStats[SceneSettings.PlayerOne].name}({SceneSettings.ControllerOne})_player";
		PlayerTwo.name = $"{_playerStats[SceneSettings.PlayerTwo].name}({SceneSettings.ControllerTwo})_player";
		PlayerOne.GetComponent<InputBuffer>().Initialize(_inputHistories[0]);
		PlayerTwo.GetComponent<InputBuffer>().Initialize(_inputHistories[1]);
		_cinemachineTargetGroup.AddMember(PlayerOne.transform, 0.5f, 0.5f);
		_cinemachineTargetGroup.AddMember(PlayerTwo.transform, 0.5f, 0.5f);
	}

	private void CheckInstance()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
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
		if (SceneSettings.Bit1)
		{
			_playerOneUI.Turn1BitVisuals();
			_playerTwoUI.Turn1BitVisuals();
		}
		_currentStage.transform.GetChild(stageColorIndex).gameObject.SetActive(true);
	}

	public void ActivateCpus()
	{
		_playerOneController.ActivateCpu();
		_playerTwoController.ActivateCpu();
	}

	public void DeactivateCpus()
	{
		if (IsTrainingMode)
		{
			_playerOneController.DeactivateCpu();
			_playerTwoController.DeactivateCpu();
		}
	}

	public void MaxHealths()
	{
		if (IsTrainingMode)
		{
			PlayerOne.MaxHealthStats();
			PlayerTwo.MaxHealthStats();
		}
	}

	void Start()
	{
		if (SceneSettings.MusicName == "Random")
		{
			_currentMusic = _musicAudio.SoundGroup("Music").PlayInRandom();
		}
		else
		{
			_currentMusic = _musicAudio.SoundGroup("Music").Sound(SceneSettings.MusicName);
			_currentMusic.Play();
		}
		if (_isTrainingMode)
		{
			_cachedOneResetPosition = PlayerOne.transform.position;
			_cachedTwoResetPosition = PlayerTwo.transform.position;
			_countdownText.gameObject.SetActive(false);
			_hearts[0].gameObject.SetActive(false);
			_hearts[1].gameObject.SetActive(false);
			_winsImage.SetActive(false);
			_trainingPrompts.gameObject.SetActive(true);
			HasGameStarted = true;
			if (!_isOnlineMode)
			{
				StartTrainingRound();
			}
		}
		else
		{
			_inputHistories[0].gameObject.SetActive(false);
			_inputHistories[1].gameObject.SetActive(false);
			_trainingPrompts.gameObject.SetActive(false);
			StartIntro();
		}
	}

	void Update()
	{
		if (HasGameStarted && !_isTrainingMode)
		{
			_countdown -= Time.deltaTime;
			_countdownText.text = Mathf.Round(_countdown).ToString();
			if (_countdown <= 0.0f)
			{
				StartCoroutine(RoundTieCoroutine());
			}
		}
		if (_isDialogueRunning)
		{
			if (Input.anyKeyDown)
			{
				_playerOneDialogue.StopDialogue();
				_playerTwoDialogue.StopDialogue();
				StartRound();
				_introAnimator.SetBool("IsIntroRunning", false);
				_isDialogueRunning = false;
			}
		}
	}

	void StartIntro()
	{
		for (int i = 0; i < _arcanaObjects.Length; i++)
		{
			_arcanaObjects[i].SetActive(false);
		}
		_isDialogueRunning = true;
		_introUI.SetPlayerNames(_characterOne.ToString(), _characterTwo.ToString());
		_playerOneController.DeactivateInput();
		_playerTwoController.DeactivateInput();
		_playerOneDialogue.Initialize(true, _playerStats[SceneSettings.PlayerOne]._dialogue, _characterTwo);
		_playerTwoDialogue.Initialize(false, _playerStats[SceneSettings.PlayerTwo]._dialogue, _characterOne);
		_introAnimator.SetBool("IsIntroRunning", true);
	}

	IEnumerator RoundTieCoroutine()
	{
		HasGameStarted = false;
		_uiAudio.Sound("TextSound").Play();
		_readyText.text = "ROUND OVER";
		Time.timeScale = 0.25f;
		yield return new WaitForSeconds(0.5f);
		_uiAudio.Sound("TextSound").Play();
		_readyText.text = "TIME'S OUT";
		yield return new WaitForSeconds(0.5f);
		_readyText.text = "";
		StartRound();
	}

	public virtual void StartRound()
	{
		_isDialogueRunning = false;
		for (int i = 0; i < _arcanaObjects.Length; i++)
		{
			_arcanaObjects[i].SetActive(true);
		}
		_countdown = 99.0f;
		_countdownText.text = Mathf.Round(_countdown).ToString();
		PlayerOne.ResetPlayer();
		PlayerTwo.ResetPlayer();
		_playerOneController.DeactivateInput();
		_playerTwoController.DeactivateInput();
		PlayerOne.transform.position = _spawnPositions[0].position;
		PlayerTwo.transform.position = _spawnPositions[1].position;
		_playerOneUI.ResetCombo();
		_playerTwoUI.ResetCombo();
		_playerOneAnimator.Intro();
		_playerTwoAnimator.Intro();
		StartCoroutine(ReadyCoroutine());
	}

	private void StartTrainingRound()
	{
		PlayerOne.ResetPlayer();
		PlayerTwo.ResetPlayer();
		PlayerOne.ResetLives();
		PlayerTwo.ResetLives();
		_playerOneUI.FadeIn();
		_playerTwoUI.FadeIn();
		_timerAnimator.SetTrigger("FadeIn");
		_infiniteTime.SetActive(true);
		PlayerOne.transform.position = _spawnPositions[0].position;
		PlayerTwo.transform.position = _spawnPositions[1].position;
		HasGameStarted = true;
	}

	IEnumerator ReadyCoroutine()
	{
		Time.timeScale = GameSpeed;
		yield return new WaitForSeconds(0.5f);
		_uiAudio.Sound("TextSound").Play();
		_readyAnimator.SetTrigger("Show");
		for (int i = 0; i < _readyObjects.Length; i++)
		{
			_readyObjects[i].SetActive(true);
		}
		if (_currentRound == 3)
		{
			_readyText.text = $"Final Round";
		}
		else
		{
			_readyText.text = $"Round {_currentRound}";
		}
		yield return new WaitForSeconds(1.0f);
		_readyAnimator.SetTrigger("Show");
		_uiAudio.Sound("TextSound").Play();
		_readyText.text = "Fight!";
		_countdownText.gameObject.SetActive(true);
		_playerOneUI.FadeIn();
		_playerTwoUI.FadeIn();
		_timerAnimator.SetTrigger("FadeIn");
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < _readyObjects.Length; i++)
		{
			_readyObjects[i].SetActive(false);
		}
		_readyText.text = "";
		_playerOneController.ActivateInput();
		_playerTwoController.ActivateInput();
		HasGameStarted = true;
	}

	public virtual void RoundOver()
	{
		if (HasGameStarted)
		{
			if (_isTrainingMode)
			{
				_roundOverTrainingCoroutine = StartCoroutine(RoundOverTrainingCoroutine());
			}
			else
			{
				StartCoroutine(RoundOverCoroutine());
			}
		}
	}

	IEnumerator RoundOverTrainingCoroutine()
	{
		HasGameStarted = false;
		Time.timeScale = 0.25f;
		yield return new WaitForSeconds(1.5f);
		StartTrainingRound();
	}

	public void SwitchCharacters()
	{
		if (IsTrainingMode && _canCallSwitchCharacter)
		{
			StartCoroutine(SwitchCharactersCoroutine());
		}
	}

	IEnumerator SwitchCharactersCoroutine()
	{
		_playerTwoController.IsPlayerOne = !_playerTwoController.IsPlayerOne;
		_playerOneController.IsPlayerOne = !_playerOneController.IsPlayerOne;
		_playerOneUI.ShowPlayerIcon();
		_playerTwoUI.ShowPlayerIcon();
		_canCallSwitchCharacter = false;
		if (_hasSwitchedCharacters)
		{
			if (_playerOneController.ControllerInputName != ControllerTypeEnum.Cpu.ToString() && _playerTwoController.ControllerInputName == ControllerTypeEnum.Cpu.ToString())
			{
				_playerOneController.SetControllerToCpu();
				_playerTwoController.SetControllerToPlayer();
			}
			else if (_playerTwoController.ControllerInputName != ControllerTypeEnum.Cpu.ToString() && _playerOneController.ControllerInputName == ControllerTypeEnum.Cpu.ToString())
			{
				_playerOneController.SetControllerToPlayer();
				_playerTwoController.SetControllerToCpu();
			}
			_playerOneController.ControllerInputName = SceneSettings.ControllerOne;
			_playerTwoController.ControllerInputName = SceneSettings.ControllerTwo;
		}
		else
		{
			if (_playerOneController.ControllerInputName != ControllerTypeEnum.Cpu.ToString() && _playerTwoController.ControllerInputName == ControllerTypeEnum.Cpu.ToString())
			{
				_playerOneController.SetControllerToCpu();
				_playerTwoController.SetControllerToPlayer();
			}
			else if (_playerTwoController.ControllerInputName != ControllerTypeEnum.Cpu.ToString() && _playerOneController.ControllerInputName == ControllerTypeEnum.Cpu.ToString())
			{
				_playerOneController.SetControllerToPlayer();
				_playerTwoController.SetControllerToCpu();
			}
			_playerOneController.ControllerInputName = SceneSettings.ControllerTwo;
			_playerTwoController.ControllerInputName = SceneSettings.ControllerOne;
		}
		_hasSwitchedCharacters = !_hasSwitchedCharacters;
		yield return new WaitForSecondsRealtime(0.1f);
		_canCallSwitchCharacter = true;
	}

	public virtual void ResetRound(Vector2 movementInput)
	{
		if (_isTrainingMode)
		{
			HasGameStarted = true;
			Time.timeScale = GameSpeed;
			if (_roundOverTrainingCoroutine != null)
			{
				StopCoroutine(_roundOverTrainingCoroutine);
			}
			PlayerOne.ResetPlayer();
			PlayerTwo.ResetPlayer();
			PlayerOne.ResetLives();
			PlayerTwo.ResetLives();

			if (movementInput.y == 1.0f)
			{
				_reverseReset = !_reverseReset;
				if (!_reverseReset)
				{
					PlayerOne.transform.position = _cachedOneResetPosition;
					PlayerTwo.transform.position = _cachedTwoResetPosition;
				}
				else
				{
					PlayerTwo.transform.position = _cachedOneResetPosition;
					PlayerOne.transform.position = _cachedTwoResetPosition;
				}
			}

			if (movementInput == Vector2.zero)
			{
				if (!_reverseReset)
				{
					PlayerOne.transform.position = _cachedOneResetPosition;
					PlayerTwo.transform.position = _cachedTwoResetPosition;
				}
				else
				{
					PlayerOne.transform.position = _cachedTwoResetPosition;
					PlayerTwo.transform.position = _cachedOneResetPosition;
				}
			}
			else if (movementInput.y == -1.0f)
			{
				if (!_reverseReset)
				{
					PlayerOne.transform.position = _spawnPositions[0].position;
					PlayerTwo.transform.position = _spawnPositions[1].position;
				}
				else
				{
					PlayerTwo.transform.position = _spawnPositions[0].position;
					PlayerOne.transform.position = _spawnPositions[1].position;
				}
				_cachedOneResetPosition = PlayerOne.transform.position;
				_cachedTwoResetPosition = PlayerTwo.transform.position;
			}
			else if(movementInput.x == 1.0f)
			{
				if (!_reverseReset)
				{
					PlayerOne.transform.position = new Vector2(_spawnPositions[0].position.x + 9, _spawnPositions[0].position.y);
					PlayerTwo.transform.position = new Vector2(_spawnPositions[1].position.x + 6, _spawnPositions[1].position.y);
				}
				else
				{
					PlayerTwo.transform.position = new Vector2(_spawnPositions[0].position.x + 9, _spawnPositions[0].position.y);
					PlayerOne.transform.position = new Vector2(_spawnPositions[1].position.x + 6, _spawnPositions[1].position.y);
				}
				_cachedOneResetPosition = PlayerOne.transform.position;
				_cachedTwoResetPosition = PlayerTwo.transform.position;
			}
			else if (movementInput.x == -1.0f)
			{
				if (!_reverseReset)
				{
					PlayerOne.transform.position = new Vector2(_spawnPositions[0].position.x - 6, _spawnPositions[0].position.y);
					PlayerTwo.transform.position = new Vector2(_spawnPositions[1].position.x - 9, _spawnPositions[1].position.y);
				}
				else
				{
					PlayerTwo.transform.position = new Vector2(_spawnPositions[0].position.x - 6, _spawnPositions[0].position.y);
					PlayerOne.transform.position = new Vector2(_spawnPositions[1].position.x - 9, _spawnPositions[1].position.y);
				}
				_cachedOneResetPosition = PlayerOne.transform.position;
				_cachedTwoResetPosition = PlayerTwo.transform.position;
			}
		}
	}

	IEnumerator RoundOverCoroutine()
	{
		_uiAudio.Sound("Round").Play();
		bool hasPlayerOneDied = PlayerOne.Health <= 0.0f;
		bool hasPlayerTwoDied = PlayerTwo.Health <= 0.0f;
		if (!hasPlayerOneDied && hasPlayerTwoDied)
		{
			PlayerTwo.LoseLife();
		}
		else
		{
			PlayerOne.LoseLife();
		}
		HasGameStarted = false;
		for (int i = 0; i < _readyObjects.Length; i++)
		{
			_readyObjects[i].SetActive(true);
		}
		_uiAudio.Sound("TextSound").Play();
		_readyAnimator.SetTrigger("Show");
		if (PlayerOne.PlayerStats.maxHealth == PlayerOne.Health || PlayerTwo.PlayerStats.maxHealth == PlayerTwo.Health)
		{
			_readyText.text = "PERFECT";
		}
		else
		{
			_readyText.text = "KO";
		}
		Time.timeScale = 0.25f;
		yield return new WaitForSecondsRealtime(1.0f);
		_uiAudio.Sound("TextSound").Play();
		_readyAnimator.SetTrigger("Show");
		if (hasPlayerOneDied && hasPlayerTwoDied)
		{
			_readyText.text = "TIE";
		}
		else
		{
			_readyText.text = "WINNER";
			if (!hasPlayerOneDied)
			{
				_winnerNameText.text = $"{_playerOneUI.PlayerName}\n{_playerOneUI.CharacterName}";
				_currentRound++;
			}
			else
			{
				_winnerNameText.text = $"{_playerTwoUI.PlayerName}\n{_playerTwoUI.CharacterName}";
				_currentRound++;
			}
		}
		yield return new WaitForSecondsRealtime(2.0f);
		for (int i = 0; i < _readyObjects.Length; i++)
		{
			_readyObjects[i].SetActive(false);
		}
		_winnerNameText.text = "";
		_readyText.text = "";
		StartRound();
	}

	public void MatchOver()
	{
		if (HasGameStarted)
		{
			StartCoroutine(MatchOverCoroutine());
		}
	}

	IEnumerator MatchOverCoroutine()
	{
		HasGameStarted = false;
		_uiAudio.Sound("Round").Play();
		bool hasPlayerOneDied = PlayerOne.Health <= 0.0f;
		bool hasPlayerTwoDied = PlayerTwo.Health <= 0.0f;
		if (!hasPlayerOneDied && hasPlayerTwoDied)
		{
			PlayerTwo.LoseLife();
		}
		else
		{
			PlayerOne.LoseLife();
		}
		for (int i = 0; i < _readyObjects.Length; i++)
		{
			_readyObjects[i].SetActive(true);
		}
		_uiAudio.Sound("TextSound").Play();
		_readyAnimator.SetTrigger("Show");
		if (PlayerOne.PlayerStats.maxHealth == PlayerOne.Health || PlayerTwo.PlayerStats.maxHealth == PlayerTwo.Health)
		{
			_readyText.text = "PERFECT";
		}
		else
		{
			_readyText.text = "KO";
		}
		Time.timeScale = 0.25f;
		yield return new WaitForSecondsRealtime(1.0f);
		_uiAudio.Sound("TextSound").Play();
		_readyAnimator.SetTrigger("Show");
		if (hasPlayerOneDied && hasPlayerTwoDied)
		{
			_readyText.text = "TIE";
		}
		else
		{
			_readyText.text = "WINNER";
			if (!hasPlayerOneDied)
			{
				_playerOneWins++;
				_winsText.text = $"{_playerOneWins}-{_playerTwoWins}";
				_winnerNameText.text = $"{_playerOneUI.PlayerName}\n{_playerOneUI.CharacterName}";
				_currentRound++;
			}
			else
			{
				_playerTwoWins++;
				_winsText.text = $"{_playerOneWins}-{_playerTwoWins}";
				_winnerNameText.text = $"{_playerTwoUI.PlayerName}\n{_playerTwoUI.CharacterName}";
				_currentRound++;
			}
		}
		yield return new WaitForSecondsRealtime(2.0f);
		if (!hasPlayerOneDied)
		{
			PlayerOne.Taunt();
		}
		else if (hasPlayerOneDied)
		{
			PlayerTwo.Taunt();
		}
		yield return new WaitForSecondsRealtime(2.0f);
		for (int i = 0; i < _readyObjects.Length; i++)
		{
			_readyObjects[i].SetActive(false);
		}
		_winnerNameText.text = "";
		_readyText.text = "";
		_currentRound = 1;
		_matchOverMenu.Show();
		Time.timeScale = 0.0f;
		DisableAllInput();
	}

	public void StartMatch()
	{
		if (SceneSettings.RandomStage)
		{
			_currentStage.SetActive(false);
			int randomStageIndex = Random.Range(0, _stages.Length);
			_currentStage = _stages[randomStageIndex];
			_currentStage.SetActive(true);
		}
		_matchOverMenu.Hide();
		PlayerOne.ResetLives();
		PlayerTwo.ResetLives();
		_currentMusic.Stop();
		if (SceneSettings.MusicName == "Random")
		{
			_currentMusic = _musicAudio.SoundGroup("Music").PlayInRandom();
		}
		else
		{
			_currentMusic = _musicAudio.SoundGroup("Music").Sound(SceneSettings.MusicName);
			_currentMusic.Play();
		}
		StartRound();
	}

	public void PauseMusic()
	{
		_currentMusic.Pause();
	}

	public void PlayMusic()
	{
		_currentMusic.Play();
	}

	public void LoadScene(int index)
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(index);
	}

	public void DisableAllInput()
	{
		_playerOneController.ActiveController.enabled = false;
		_playerTwoController.ActiveController.enabled = false;
	}

	public void EnableAllInput()
	{
		_playerOneController.ActiveController.enabled = true;
		_playerTwoController.ActiveController.enabled = true;
	}

	public void SlowdownPunish()
	{
		StartCoroutine(SlowdownPunishCoroutine());
	}

	IEnumerator SlowdownPunishCoroutine()
	{
		Time.timeScale = 0.25f;
		yield return new WaitForSecondsRealtime(0.25f);
		Time.timeScale = GameSpeed;
	}

	public void HitStop(float hitstop)
	{
		if (hitstop > 0.0f)
		{
			StartCoroutine(HitStopCoroutine(hitstop));
		}
	}

	IEnumerator HitStopCoroutine(float hitstop)
	{
		Time.timeScale = 0.0f;
		yield return new WaitForSecondsRealtime(hitstop);
		Time.timeScale = GameSpeed;
	}
}
