using Cinemachine;
using Demonics.Sounds;
using Demonics.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
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
	[SerializeField] private bool _isTrainingMode = default;
	[SerializeField] private bool _isOnlineMode = default;
	[Range(1, 10)]
	[SerializeField] private int _gameSpeed = 1;
	[Header("Data")]
	[SerializeField] private Transform[] _spawnPositions = default;
	[SerializeField] protected PlayerUI _playerOneUI = default;
	[SerializeField] protected PlayerUI _playerTwoUI = default;
	[SerializeField] private Animator _timerAnimator = default;
	[SerializeField] protected TextMeshProUGUI _countdownText = default;
	[SerializeField] protected TextMeshProUGUI _readyText = default;
	[SerializeField] protected TextMeshProUGUI _winnerNameText = default;
	[SerializeField] protected GameObject _bottomLine = default;
	[SerializeField] protected GameObject _leftStopper = default;
	[SerializeField] protected GameObject _rightStopper = default;
	[SerializeField] protected GameObject _player = default;
	[SerializeField] protected GameObject _infiniteTime = default;
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
	private Coroutine _roundOverTrainingCoroutine;
	private Sound _currentMusic;
	private GameObject _currentStage;
	private float _countdown;
	private int _currentRound = 1;
	private bool _reverseReset;
	private bool _hasSwitchedCharacters;
	private bool _canCallSwitchCharacter = true;

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
		CheckSceneSettings();
		if (!SceneSettings.SceneSettingsDecide)
		{
			SceneSettings.ControllerOne = _controllerOne.ToString();
			SceneSettings.ControllerTwo = _controllerTwo.ToString();
			SceneSettings.PlayerOne = (int)_characterOne;
			SceneSettings.PlayerTwo = (int)_characterTwo;
			SceneSettings.AssistOne = (int)_assistOne;
			SceneSettings.AssistTwo = (int)_assistTwo;
			SceneSettings.IsTrainingMode = _isTrainingMode;
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

		if (_isOnlineMode)
		{
			//List<GameObject> players = NetworkExtenderManager.Instance.SpawnConnectedClients(_player);
			//if (players != null)
			//{
			//	GameObject playerOneObject = players[0];
			//	GameObject playerTwoObject = players[1];
			//	playerOneObject.GetComponent<PlayerStats>().PlayerStatsSO = _playerStats[SceneSettings.PlayerOne];
			//	playerTwoObject.GetComponent<PlayerStats>().PlayerStatsSO = _playerStats[SceneSettings.PlayerTwo];
			//	InitializePlayers(playerOneObject, playerTwoObject);
			//}
		}
		else
		{
			GameObject playerOneObject = Instantiate(_player);
			playerOneObject.GetComponent<PlayerStats>().PlayerStatsSO = _playerStats[SceneSettings.PlayerOne];
			GameObject playerTwoObject = Instantiate(_player);
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
		PlayerOne.transform.GetChild(1).GetComponent<PlayerAnimationEvents>().SetTrainingMenu(_trainingMenu);
		PlayerTwo.transform.GetChild(1).GetComponent<PlayerAnimationEvents>().SetTrainingMenu(_trainingMenu);
		PlayerOne.transform.GetChild(1).GetComponent<PlayerAnimator>().SetSpriteLibraryAsset(SceneSettings.ColorOne);
		if (SceneSettings.ColorTwo == SceneSettings.ColorOne && PlayerOne.PlayerStats.characterName == PlayerTwo.PlayerStats.characterName)
		{
			SceneSettings.ColorTwo++;
		}
		PlayerTwo.transform.GetChild(1).GetComponent<PlayerAnimator>().SetSpriteLibraryAsset(SceneSettings.ColorTwo);
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
		if (!SceneSettings.SceneSettingsDecide)
		{
			_currentStage = _stages[(int)_stage];
			_currentStage.SetActive(true);
		}
		else
		{
			_currentStage = _stages[SceneSettings.StageIndex];
			_currentStage.SetActive(true);
		}

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
		_currentMusic = _musicAudio.SoundGroup("Music").PlayInRandom();
		if (_isTrainingMode)
		{
			_countdownText.gameObject.SetActive(false);
			_hearts[0].gameObject.SetActive(false);
			_hearts[1].gameObject.SetActive(false);
			_trainingPrompts.gameObject.SetActive(true);
			HasGameStarted = true;
			StartTrainingRound();
		}
		else
		{
			_inputHistories[0].gameObject.SetActive(false);
			_inputHistories[1].gameObject.SetActive(false);
			StartRound();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			List<GameObject> players = NetworkExtenderManager.Instance.SpawnConnectedClients(_player);
			if (players != null)
			{
				GameObject playerOneObject = players[0];
				GameObject playerTwoObject = players[1];
				playerOneObject.GetComponent<PlayerStats>().PlayerStatsSO = _playerStats[SceneSettings.PlayerOne];
				playerTwoObject.GetComponent<PlayerStats>().PlayerStatsSO = _playerStats[SceneSettings.PlayerTwo]; 
				InitializePlayers(playerOneObject, playerTwoObject);
			}
		}
		if (HasGameStarted && !_isTrainingMode)
		{
			_countdown -= Time.deltaTime;
			_countdownText.text = Mathf.Round(_countdown).ToString();
			if (_countdown <= 0.0f)
			{
				StartCoroutine(RoundTieCoroutine());
			}
		}
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
		_countdown = 99.0f;
		_countdownText.text = Mathf.Round(_countdown).ToString();
		PlayerOne.ResetPlayer();
		PlayerTwo.ResetPlayer();
		_leftStopper.SetActive(true);
		_rightStopper.SetActive(true);
		PlayerOne.transform.position = _spawnPositions[0].position;
		PlayerTwo.transform.position = _spawnPositions[1].position;
		_playerOneUI.ResetCombo();
		_playerTwoUI.ResetCombo();
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
		_leftStopper.SetActive(false);
		_rightStopper.SetActive(false);
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
		_bottomLine.SetActive(true);
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
		_bottomLine.SetActive(false);
		_readyText.text = "";
		_leftStopper.SetActive(false);
		_rightStopper.SetActive(false);
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
			_leftStopper.SetActive(false);
			_rightStopper.SetActive(false);
			if (movementInput.y > 0.0f)
			{
				_reverseReset = !_reverseReset;
			}
			if (_reverseReset)
			{
				PlayerOne.transform.position = _spawnPositions[0].position;
				PlayerTwo.transform.position = _spawnPositions[1].position;
			}
			else
			{
				PlayerOne.transform.position = _spawnPositions[1].position;
				PlayerTwo.transform.position = _spawnPositions[0].position;
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
		_bottomLine.SetActive(true);
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
		_bottomLine.SetActive(false);
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
		_bottomLine.SetActive(true);
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
				_playerOneUI.IncreaseWins();
				_playerTwoUI.ResetWinsRow();
				_winnerNameText.text = $"{_playerOneUI.PlayerName}\n{_playerOneUI.CharacterName}";
				_currentRound++;
			}
			else
			{
				_playerTwoUI.IncreaseWins();
				_playerOneUI.ResetWinsRow();
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
		_bottomLine.SetActive(false);
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
		_currentMusic = _musicAudio.SoundGroup("Music").PlayInRandom();
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
