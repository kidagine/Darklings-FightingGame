using Cinemachine;
using Demonics.Sounds;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab = default;
    [SerializeField] private bool _sceneSettingsDecide = true;
    [SerializeField] private string _controllerOne = default;
    [SerializeField] private string _controllerTwo = default;
    [SerializeField] private bool _isTrainingMode = default;
    [SerializeField] protected PlayerUI _playerOneUI = default;
    [SerializeField] protected PlayerUI _playerTwoUI = default;
    [SerializeField] protected TextMeshProUGUI _countdownText = default;
    [SerializeField] protected TextMeshProUGUI _readyText = default;
    [SerializeField] protected GameObject _leftStopper = default;
    [SerializeField] protected GameObject _rightStopper = default;
    [SerializeField] protected GameObject[] _stages = default;
    [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup = default;
    [Range(1, 10)]
    [SerializeField] private int _gameSpeed = 1;
    protected Player _playerOne;
    protected Player _playerTwo;
    protected BaseController _playerOneController;
    protected BaseController _playerTwoController;
    private Audio _audio;
    private Sound _currentMusic;
    private float _countdown;
    private bool _reverseReset;

    public bool HasGameStarted { get; set; }
	public bool IsTrainingMode { get { return _isTrainingMode; } set { } }
	public static GameManager Instance { get; private set; }


	void Awake()
    {
        _audio = GetComponent<Audio>();
        CheckInstance();
        CheckSceneSettings();
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
        _stages[SceneSettings.StageIndex].SetActive(true);
    }

    void Start()
    {
        _currentMusic = _audio.SoundGroup("Music").PlayInRandom();
        if (!_sceneSettingsDecide)
        {
            SceneSettings.ControllerOne = _controllerOne;
            SceneSettings.ControllerTwo = _controllerTwo;
        }
        else
        {
            _isTrainingMode = SceneSettings.IsTrainingMode;
        }
        GameObject playerOneObject = Instantiate(_playerPrefab);
        GameObject playerTwoObject = Instantiate(_playerPrefab);
        if (SceneSettings.ControllerOne != "")
        {
            playerOneObject.AddComponent<PlayerController>();
        }
        else
        {
            playerOneObject.AddComponent<CpuController>();
        }
        if (SceneSettings.ControllerTwo != "")
        {
            playerTwoObject.AddComponent<PlayerController>();
        }
        else
        {
            playerTwoObject.AddComponent<CpuController>();
        }
        if (SceneSettings.ControllerOne == "")
        {
            playerOneObject.GetComponent<CpuController>().SetOtherPlayer(playerTwoObject.transform);
        }
        if (SceneSettings.ControllerTwo == "")
        {
            playerTwoObject.GetComponent<CpuController>().SetOtherPlayer(playerOneObject.transform);
        }
        _playerOne = playerOneObject.GetComponent<Player>();
        _playerTwo = playerTwoObject.GetComponent<Player>();
        _playerOne.SetController();
        _playerTwo.SetController();
        _playerOne.GetComponent<PlayerMovement>().SetController();
        _playerTwo.GetComponent<PlayerMovement>().SetController();
        _playerOneController = playerOneObject.GetComponent<BaseController>();
        _playerTwoController = playerTwoObject.GetComponent<BaseController>();
        _playerOne.SetPlayerUI(_playerOneUI);
        _playerOne.SetOtherPlayer(_playerTwo.transform);
        _playerOne.IsPlayerOne = true;
        _playerOneController.ControllerInputName = SceneSettings.ControllerOne;
        _playerTwo.SetPlayerUI(_playerTwoUI);
        _playerTwo.SetOtherPlayer(_playerOne.transform);
        _playerTwo.IsPlayerOne = false;
        _playerTwoController.ControllerInputName = SceneSettings.ControllerTwo;
        _playerOne.name = "PlayerOne";
        _playerTwo.name = "PlayerTwo";

        _cinemachineTargetGroup.AddMember(_playerOne.transform, 0.5f, 0.5f);
        _cinemachineTargetGroup.AddMember(_playerTwo.transform, 0.5f, 0.5f);

        if (_isTrainingMode)
        {
            _playerOneUI.transform.GetChild(2).gameObject.SetActive(false);
            _playerTwoUI.transform.GetChild(2).gameObject.SetActive(false);
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
        _audio.Sound("TextSound").Play();
        _readyText.text = "ROUND OVER";
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(0.5f);
        _audio.Sound("TextSound").Play();
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
        _playerOneController = _playerOne.GetComponent<PlayerController>();
        _playerTwoController = _playerTwo.GetComponent<PlayerController>();
        _playerOne.ResetPlayer();
        _playerTwo.ResetPlayer();
        _playerOne.ResetLives();
        _playerTwo.ResetLives();
        _leftStopper.SetActive(false);
        _rightStopper.SetActive(false);
        _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
        _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
        HasGameStarted = true;
    }

    IEnumerator ReadyCoroutine()
    {
        Time.timeScale = _gameSpeed;
        yield return new WaitForSeconds(0.5f);
        _audio.Sound("TextSound").Play();
        _readyText.text = "Ready?";
        yield return new WaitForSeconds(1.0f);
        _audio.Sound("TextSound").Play();
        _readyText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
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
            if (movementInput.x > 0.0f)
            {
                if (_reverseReset)
                {
                    _playerOne.transform.position = new Vector2(8.5f, -4.75f);
                    _playerTwo.transform.position = new Vector2(5.5f, -4.75f);
                }
                else
                {
                    _playerOne.transform.position = new Vector2(5.5f, -4.75f);
                    _playerTwo.transform.position = new Vector2(8.5f, -4.75f);
                }
            }
            else if (movementInput.x < 0.0f)
            {
                if (_reverseReset)
                {
                    _playerOne.transform.position = new Vector2(-5.5f, -4.75f);
                    _playerTwo.transform.position = new Vector2(-8.5f, -4.75f);
                }
                else
                {
                    _playerOne.transform.position = new Vector2(-8.5f, -4.75f);
                    _playerTwo.transform.position = new Vector2(-5.5f, -4.75f);
                }
            }
            else if (movementInput.y < 0.0f)
            {
                if (_reverseReset)
                {
                    _playerOne.transform.position = new Vector2(3.5f, -4.75f);
                    _playerTwo.transform.position = new Vector2(-3.5f, -4.75f);
                }
                else
                {
                    _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
                    _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
                }
            }
            else
            {
                if (_reverseReset)
                {
                    _playerOne.transform.position = new Vector2(3.5f, -4.75f);
                    _playerTwo.transform.position = new Vector2(-3.5f, -4.75f);
                }
                else
                {
                    _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
                    _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
                }
            }
        }
    }

    IEnumerator RoundOverCoroutine()
    {
        _audio.Sound("Round").Play();
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
        _audio.Sound("TextSound").Play();
        _readyText.text = "ROUND OVER";
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(1.5f);
        if (hasPlayerOneDied && hasPlayerTwoDied)
        {
            _audio.Sound("TextSound").Play();
            _readyText.text = "TIE";
        }
        else
        {
            if (!hasPlayerOneDied)
            {
                _audio.Sound("TextSound").Play();
                _readyText.text = "P1 WINS";
            }
            else
            {
                _audio.Sound("TextSound").Play();
                _readyText.text = "P2 WINS";
            }
        }
        yield return new WaitForSeconds(1.0f);
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
        _audio.Sound("Round").Play();
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
        _audio.Sound("TextSound").Play();
        _readyText.text = "MATCH OVER";
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(1.5f);
        if (hasPlayerOneDied && hasPlayerTwoDied)
        {
            _audio.Sound("TextSound").Play();
            _readyText.text = "TIE";
        }
        else
        {
            if (!hasPlayerOneDied)
            {
                _audio.Sound("TextSound").Play();
                _readyText.text = "P1 WINS";
            }
            else
            {
                _audio.Sound("TextSound").Play();
                _readyText.text = "P2 WINS";
            }
        }
        yield return new WaitForSeconds(1.0f);
        _playerOne.ResetLives();
        _playerTwo.ResetLives();
        _currentMusic.Stop();
        _currentMusic = _audio.SoundGroup("Music").PlayInRandom();
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
}
