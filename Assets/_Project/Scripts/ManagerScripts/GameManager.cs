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
    [SerializeField] private CharacterTypeEnum _characterOne = default;
    [SerializeField] private CharacterTypeEnum _characterTwo = default;
    [SerializeField] private ControllerTypeEnum _controllerOne = default;
    [SerializeField] private ControllerTypeEnum _controllerTwo = default;
    [SerializeField] private bool _isTrainingMode = default;
    [Range(1, 10)]
    [SerializeField] private int _gameSpeed = 1;
    [Header("Data")]
    [SerializeField] protected PlayerUI _playerOneUI = default;
    [SerializeField] protected PlayerUI _playerTwoUI = default;
    [SerializeField] private Animator _timerAnimator = default;
    [SerializeField] protected TextMeshProUGUI _countdownText = default;
    [SerializeField] protected TextMeshProUGUI _readyText = default;
    [SerializeField] protected TextMeshProUGUI _winnerNameText = default;
    [SerializeField] protected GameObject _bottomLine = default;
    [SerializeField] protected GameObject _leftStopper = default;
    [SerializeField] protected GameObject _rightStopper = default;
    [SerializeField] protected GameObject _infiniteTime = default;
    [SerializeField] private Player[] _characters = default;
    [SerializeField] protected GameObject[] _stages = default;
    [SerializeField] private BaseMenu _matchOverMenu = default;
    [SerializeField] private Animator _readyAnimator = default;
    [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup = default;
    [SerializeField] private Audio _musicAudio = default;
    [SerializeField] private Audio _uiAudio = default;
    protected Player _playerOne;
    protected Player _playerTwo;
    protected BaseController _playerOneController;
    protected BaseController _playerTwoController;
    private Sound _currentMusic;
    private GameObject _currentStage;
    private float _countdown;
    private int _currentRound = 1;
    private bool _reverseReset;

	public bool HasGameStarted { get; set; }
	public bool IsTrainingMode { get { return _isTrainingMode; } set { } }
	public static GameManager Instance { get; private set; }
    public float GameSpeed { get; set; }


	void Awake()
    {
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
        }
        else
        {
            _isTrainingMode = SceneSettings.IsTrainingMode;
        }
        GameObject playerOneObject = Instantiate(_characters[SceneSettings.PlayerOne].gameObject);
        GameObject playerTwoObject = Instantiate(_characters[SceneSettings.PlayerTwo].gameObject);
        playerOneObject.SetActive(true);
        playerTwoObject.SetActive(true);
        if (SceneSettings.ControllerOne != ControllerTypeEnum.Cpu.ToString())
        {
            playerOneObject.AddComponent<PlayerController>();
        }
        else
        {
            playerOneObject.AddComponent<CpuController>();
        }
        if (SceneSettings.ControllerTwo != ControllerTypeEnum.Cpu.ToString())
        {
            playerTwoObject.AddComponent<PlayerController>();
        }
        else
        {
            playerTwoObject.AddComponent<CpuController>();
        }
        if (SceneSettings.ControllerOne == ControllerTypeEnum.Cpu.ToString())
        {
            playerOneObject.GetComponent<CpuController>().SetOtherPlayer(playerTwoObject.transform);
        }
        if (SceneSettings.ControllerTwo == ControllerTypeEnum.Cpu.ToString())
        {
            playerTwoObject.GetComponent<CpuController>().SetOtherPlayer(playerOneObject.transform);
        }
        _playerOne = playerOneObject.GetComponent<Player>();
        _playerTwo = playerTwoObject.GetComponent<Player>();
        _playerOne.SetController();
        _playerTwo.SetController();
        _playerOne.GetComponent<PlayerMovement>().SetController();
        _playerTwo.GetComponent<PlayerMovement>().SetController();
        _playerOne.transform.GetChild(1).GetComponent<PlayerAnimator>().SetSpriteLibraryAsset(SceneSettings.ColorOne);
        if (SceneSettings.ColorTwo == SceneSettings.ColorOne && _playerOne.PlayerStats.characterName == _playerTwo.PlayerStats.characterName)
        {
            SceneSettings.ColorTwo++;
        }
        _playerTwo.transform.GetChild(1).GetComponent<PlayerAnimator>().SetSpriteLibraryAsset(SceneSettings.ColorTwo);
        _playerOneController = playerOneObject.GetComponent<BaseController>();
        _playerOneController.IsPlayerOne = true;
        _playerTwoController = playerTwoObject.GetComponent<BaseController>();
        _playerOne.SetPlayerUI(_playerOneUI);
        _playerTwo.SetPlayerUI(_playerTwoUI);
        _playerOne.SetOtherPlayer(_playerTwo.transform);
        _playerOne.IsPlayerOne = true;
        _playerOneController.ControllerInputName = SceneSettings.ControllerOne;
        _playerTwo.SetOtherPlayer(_playerOne.transform);
        _playerTwo.IsPlayerOne = false;
        _playerTwoController.ControllerInputName = SceneSettings.ControllerTwo;
        _playerOne.name = "PlayerOne";
        _playerTwo.name = "PlayerTwo";

        _cinemachineTargetGroup.AddMember(_playerOne.transform, 0.5f, 0.5f);
        _cinemachineTargetGroup.AddMember(_playerTwo.transform, 0.5f, 0.5f);

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
        if (SceneSettings.RandomStage)
        {
            int randomStageIndex = Random.Range(0, _stages.Length);
            _currentStage = _stages[randomStageIndex];
            _currentStage.SetActive(true);
        }
        else
        {
            _currentStage = _stages[SceneSettings.StageIndex];
            _currentStage.SetActive(true);
        }   
    }

    void Start()
    {
        _currentMusic = _musicAudio.SoundGroup("Music").PlayInRandom();
        if (_isTrainingMode)
        {
            _countdownText.gameObject.SetActive(false);
            HasGameStarted = true;
            StartTrainingRound();
        }
        else
        {
            StartRound();
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
        _playerOne.ResetPlayer();
        _playerTwo.ResetPlayer();
        _leftStopper.SetActive(true);
        _rightStopper.SetActive(true);
        _playerOne.transform.position = new Vector2(-3.5f, -4.5f);
        _playerTwo.transform.position = new Vector2(3.5f, -4.5f);
        _playerOneUI.ResetCombo();
        _playerTwoUI.ResetCombo();
        StartCoroutine(ReadyCoroutine());
    }

    private void StartTrainingRound()
    {
        _playerOne.ResetPlayer();
        _playerTwo.ResetPlayer();
        _playerOne.ResetLives();
        _playerTwo.ResetLives();
        _playerOneUI.FadeIn();
        _playerTwoUI.FadeIn();
        _timerAnimator.SetTrigger("FadeIn");
        _infiniteTime.SetActive(true);
        _leftStopper.SetActive(false);
        _rightStopper.SetActive(false);
        _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
        _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
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
                StartCoroutine(RoundOverTrainingCoroutine());
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

    public virtual void ResetRound(Vector2 movementInput)
    {
        if (_isTrainingMode)
        {
            _playerOneController = _playerOne.GetComponent<PlayerController>();
            _playerTwoController = _playerTwo.GetComponent<PlayerController>();
            _playerOne.ResetPlayer();
            _playerTwo.ResetPlayer();
            _playerOne.ResetLives();
            _playerTwo.ResetLives();
            _leftStopper.SetActive(false);
            _rightStopper.SetActive(false);
            if (movementInput.y > 0.0f)
            {
                _reverseReset = !_reverseReset;
            }
            if (_reverseReset)
            {
                _playerOne.transform.position = new Vector2(3.5f, -4.485f);
                _playerTwo.transform.position = new Vector2(-3.5f, -4.485f);
            }
            else
            {
                _playerOne.transform.position = new Vector2(-3.5f, -4.485f);
                _playerTwo.transform.position = new Vector2(3.5f, -4.485f);
            }
            //else if (movementInput.x < 0.0f)
            //{
            //    if (_reverseReset)
            //    {
            //        _playerOne.transform.position = new Vector2(-5.5f, -4.75f);
            //        _playerTwo.transform.position = new Vector2(-8.5f, -4.75f);
            //    }
            //    else
            //    {
            //        _playerOne.transform.position = new Vector2(-8.5f, -4.75f);
            //        _playerTwo.transform.position = new Vector2(-5.5f, -4.75f);
            //    }
            //}
            //else if (movementInput.y < 0.0f)
            //{
            //    if (_reverseReset)
            //    {
            //        _playerOne.transform.position = new Vector2(3.5f, -4.75f);
            //        _playerTwo.transform.position = new Vector2(-3.5f, -4.75f);
            //    }
            //    else
            //    {
            //        _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
            //        _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
            //    }
            //}
            //else
            //{
            //    if (_reverseReset)
            //    {
            //        _playerOne.transform.position = new Vector2(3.5f, -4.75f);
            //        _playerTwo.transform.position = new Vector2(-3.5f, -4.75f);
            //    }
            //    else
            //    {
            //        _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
            //        _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
            //    }
            //}
        }
    }

    IEnumerator RoundOverCoroutine()
    {
        _uiAudio.Sound("Round").Play();
        bool hasPlayerOneDied = _playerOne.Health <= 0.0f;
        bool hasPlayerTwoDied = _playerTwo.Health <= 0.0f;
        if (!hasPlayerOneDied && hasPlayerTwoDied)
        {
            _playerTwo.LoseLife();
        }
        else
        {
            _playerOne.LoseLife();
        }
        HasGameStarted = false;
        _bottomLine.SetActive(true);
        _uiAudio.Sound("TextSound").Play();
        _readyAnimator.SetTrigger("Show");
        _readyText.text = "KO";
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
        bool hasPlayerOneDied = _playerOne.Health <= 0.0f;
        bool hasPlayerTwoDied = _playerTwo.Health <= 0.0f;
        if (!hasPlayerOneDied && hasPlayerTwoDied)
        {
            _playerTwo.LoseLife();
        }
        else
        {
            _playerOne.LoseLife();
        }
        _bottomLine.SetActive(true);
        _uiAudio.Sound("TextSound").Play();
        _readyAnimator.SetTrigger("Show");
        _readyText.text = "KO";
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
                _winnerNameText.text = $"{_playerOneUI.PlayerName}\n{_playerOneUI.CharacterName}";
                _currentRound++;
            }
            else
            {
                _playerTwoUI.IncreaseWins();
                _winnerNameText.text = $"{_playerTwoUI.PlayerName}\n{_playerTwoUI.CharacterName}";
                _currentRound++;
            }
        }
        yield return new WaitForSecondsRealtime(2.0f);
        if (!hasPlayerOneDied)
        {
            _playerOne.Taunt();
        }
        else if (hasPlayerOneDied)
        {
            _playerTwo.Taunt();
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
		_playerOne.ResetLives();
		_playerTwo.ResetLives();
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
        _playerOneController.enabled = false;
        _playerTwoController.enabled = false;
    }

    public void EnableAllInput()
    {
        _playerOneController.enabled = true;
        _playerTwoController.enabled = true;
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
}
