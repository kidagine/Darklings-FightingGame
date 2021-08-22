using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _playerOne = default;
    [SerializeField] private Player _playerTwo = default;
    [SerializeField] private TextMeshProUGUI _countdownText = default;
    [SerializeField] private TextMeshProUGUI _readyText = default;
    [SerializeField] private GameObject _leftStopper = default;
    [SerializeField] private GameObject _rightStopper = default;
    private PlayerController _playerOneController;
    private PlayerController _playerTwoController;
    private bool _hasGameStarted;
    private float _countdown = 99.0f;

    public static GameManager Instance { get; private set; }
	public bool PlayerOneWon { private get; set; }


	void Awake()
    {
        CheckInstance();
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

    void Start()
    {
        StartRound();
    }

	void Update()
	{
	    if (_hasGameStarted)
		{
            _countdown -= Time.deltaTime;
            _countdownText.text = Mathf.Round(_countdown).ToString();
            if (_countdown <= 0.0f)
            {
                _hasGameStarted = false;
            }
		}
	}

    private void StartRound()
    {
        _countdown = 99.0f;
        _countdownText.text = "99";
        _playerOneController = _playerOne.GetComponent<PlayerController>();
        _playerTwoController = _playerTwo.GetComponent<PlayerController>();
        _playerOne.ResetPlayer();
        _playerTwo.ResetPlayer();
        _leftStopper.SetActive(true);
        _rightStopper.SetActive(true);
        _playerOne.transform.position = new Vector2(-3.5f, -4.75f);
        _playerTwo.transform.position = new Vector2(3.5f, -4.75f);
        StartCoroutine(ReadyCoroutine());
    }

	IEnumerator ReadyCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _readyText.text = "Ready?";
        yield return new WaitForSeconds(1.0f);
        _readyText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        _readyText.text = "";
        _leftStopper.SetActive(false);
        _rightStopper.SetActive(false);
        _hasGameStarted = true;
    }

    public void RoundOver()
    {
        StartCoroutine(RoundOverCoroutine());
    }

    IEnumerator RoundOverCoroutine()
    {
        _hasGameStarted = false;
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
        StartCoroutine(MatchOverCoroutine());
    }

    IEnumerator MatchOverCoroutine()
    {
        _hasGameStarted = false;
        _readyText.text = "MATCH OVER";
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(2.5f);
        _readyText.text = PlayerOneWon is false ? "P1 WINS" : "P2 WINS";
		_playerOne.ResetLives();
        _playerTwo.ResetLives();
		StartRound();
    }
}
