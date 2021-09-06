using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected PlayerUI _playerOneUI = default;
    [SerializeField] protected PlayerUI _playerTwoUI = default;
    [SerializeField] protected Player _playerOne = default;
    [SerializeField] protected Player _playerTwo = default;
    [SerializeField] protected TextMeshProUGUI _countdownText = default;
    [SerializeField] protected TextMeshProUGUI _readyText = default;
    [SerializeField] protected GameObject _leftStopper = default;
    [SerializeField] protected GameObject _rightStopper = default;
    [SerializeField] protected GameObject[] _stages = default;
    [SerializeField] protected bool _hasCountDown = true;
    [SerializeField] protected bool _hasTimer = true;
    protected PlayerController _playerOneController;
    protected PlayerController _playerTwoController;
    private float _countdown;

	public bool HasGameStarted { get; set; }
	public static GameManager Instance { get; private set; }
	public bool PlayerOneWon { private get; set; }


	void Awake()
    {
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
        StartRound();
    }

	void Update()
	{
	    if (HasGameStarted && _hasCountDown)
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
        _readyText.text = "ROUND OVER";
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(0.5f);
        _readyText.text = "TIME OUT";
        yield return new WaitForSeconds(0.5f);
        _readyText.text = "";
        StartRound();
    }

    public virtual void StartRound()
    {
        _countdown = 99.0f;
        _countdownText.text = Mathf.Round(_countdown).ToString();
        _playerOneController = _playerOne.GetComponent<PlayerController>();
        _playerTwoController = _playerTwo.GetComponent<PlayerController>();
        _playerOne.ResetPlayer();
        _playerTwo.ResetPlayer();
        _leftStopper.SetActive(true);
        _rightStopper.SetActive(true);
        _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
        _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
        _playerOneUI.ResetCombo();
        _playerTwoUI.ResetCombo();
        StartCoroutine(ReadyCoroutine());
    }

	IEnumerator ReadyCoroutine()
    {
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.5f);
        _readyText.text = "Ready?";
        yield return new WaitForSeconds(1.0f);
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
            StartCoroutine(RoundOverCoroutine());
        }
    }

    public virtual void ResetRound(Vector2 movementInput)
    {
    }

    IEnumerator RoundOverCoroutine()
    {
        HasGameStarted = false;
        _readyText.text = "ROUND OVER";
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(2.5f);
        _readyText.text = PlayerOneWon is false ? "P1 WINS" : "P2 WINS";
        yield return new WaitForSeconds(2.0f);
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
        _readyText.text = "MATCH OVER";
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(2.5f);
        _readyText.text = PlayerOneWon is false ? "P1 WINS" : "P2 WINS";
        yield return new WaitForSeconds(2.0f);
        _playerOne.ResetLives();
        _playerTwo.ResetLives();
		StartRound();
    }
}
