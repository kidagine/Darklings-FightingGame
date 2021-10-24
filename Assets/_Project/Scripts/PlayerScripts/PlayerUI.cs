using Demonics.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _lostLives = default;
    [SerializeField] private Slider _healthSlider = default;
    [SerializeField] private Slider _arcanaSlider = default;
    [SerializeField] private Image _portraitImage = default;
    [SerializeField] private TextMeshProUGUI _characterName = default;
    [SerializeField] private TextMeshProUGUI _playerName = default;
    [SerializeField] private TextMeshProUGUI _comboText = default;
    [SerializeField] private TextMeshProUGUI _winsText = default;
    [SerializeField] private Transform _healthDividerPivot = default;
    [SerializeField] private GameObject _healthDividerPrefab = default;
    [SerializeField] private Transform _arcanaDividerPivot = default;
    [SerializeField] private GameObject _arcanaDividerPrefab = default;
    [SerializeField] private Slider _pauseSlider = default;
    [SerializeField] private BaseMenu _pauseMenu = default;
    private Coroutine _openPauseHoldCoroutine;
    private Animator _animator;
    private int _currentLifeIndex;
    private int _currentComboCount;
    private int _currentWins;
    private bool _hasComboEnded;
    private bool _initializedStats;

	public string PlayerName { get; private set; }
    public string CharacterName { get; private set; }


	void Awake()
	{
        _animator = GetComponent<Animator>();
    }

	public void InitializeUI(PlayerStatsSO playerStats, bool isPlayerOne)
    {
        if (!_initializedStats)
        {
            if (isPlayerOne)
            {
                if (SceneSettings.ControllerOne == "")
                {
                    PlayerName = "Cpu 1";
                    _playerName.text = PlayerName;
                }
                else
                {
                    PlayerName = "Player 1";
                    _playerName.text = PlayerName;
                }
            }
            else
            {
                if (SceneSettings.ControllerTwo == "")
                {
                    PlayerName = "Cpu 2";
                    _playerName.text = PlayerName;
                }
                else
                {
                    PlayerName = "Player 2";
                    _playerName.text = PlayerName;
                }
            }
            CharacterName = playerStats.characterName;
            _characterName.text = CharacterName;
            if (isPlayerOne)
            {
                SetPortrait(playerStats.portraits[SceneSettings.ColorOne]);
            }
            else 
            {
                SetPortrait(playerStats.portraits[SceneSettings.ColorTwo]);
            }
            SetMaxHealth(playerStats.maxHealth);
            SetMaxArcana(playerStats.maxArcana);
            _initializedStats = true;
        }
    }

    private void SetPortrait(Sprite portrait)
    {
        _portraitImage.sprite = portrait;
    }

    private void SetMaxHealth(float value)
    {
        float healthSliderWidth = _healthSlider.GetComponent<RectTransform>().sizeDelta.x;
        _healthSlider.maxValue = value;
        float increaseValue = healthSliderWidth / value;
        float currentPositionX = 0.0f;
        for (int i = 0; i < value + 1; i++)
		{
            GameObject healthDivider = Instantiate(_healthDividerPrefab, _healthDividerPivot);
            healthDivider.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPositionX, 0.0f);
            currentPositionX -= increaseValue;
		}
    }

    private void SetMaxArcana(float value)
    {
        float arcanaSliderWidth = _arcanaSlider.GetComponent<RectTransform>().sizeDelta.x;
        _arcanaSlider.maxValue = value;
        float increaseValue = arcanaSliderWidth / value;
        float currentPositionX = increaseValue;
        for (int i = 0; i < value - 1; i++)
        {
            GameObject arcanaDivider = Instantiate(_arcanaDividerPrefab, _arcanaDividerPivot);
            arcanaDivider.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPositionX, 0.0f);
            currentPositionX += increaseValue;
        }
    }

    public void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }

    public void SetArcana(float value)
    {
        _arcanaSlider.value = value;
    }

    public void SetHealth(float value)
    {
        _healthSlider.value = value;
    }

    public void SetLives()
    {
        _lostLives[_currentLifeIndex].SetActive(true);
        _currentLifeIndex++;
    }

    public void ResetLives()
    {
        _lostLives[0].SetActive(false);
        _lostLives[1].SetActive(false);
        _currentLifeIndex = 0;
    }

    public void IncreaseWins()
    {
        _currentWins++;
        if (_currentWins == 1)
        {
            _winsText.text = $"{_currentWins} Win";
        }
        else
        {
            _winsText.text = $"{_currentWins} Wins";
        }
    }

    public void IncreaseCombo()
    {
        if (_hasComboEnded)
        {
            _hasComboEnded = false;
            _comboText.gameObject.SetActive(false);
            _currentComboCount = 0;
            _comboText.text = "Hits 0";
        }
        _currentComboCount++;
        _comboText.text = "Hits " + _currentComboCount.ToString();
        if (_currentComboCount > 1)
        {
            _comboText.gameObject.SetActive(true);
        }
    }

    public void ResetCombo()
    {
        StartCoroutine(ResetComboCoroutine());
    }

    public void OpenPauseHold()
    {
        _pauseSlider.gameObject.SetActive(true);
        _openPauseHoldCoroutine = StartCoroutine(OpenPauseHoldCoroutine());
    }

    IEnumerator OpenPauseHoldCoroutine()
    {
        float t = 0.0f;
        while (_pauseSlider.value < _pauseSlider.maxValue)
        {
            _pauseSlider.value = Mathf.Lerp(0.0f, _pauseSlider.maxValue, t);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _pauseSlider.value = 0.0f;
        _pauseSlider.gameObject.SetActive(false);
        OpenPause();
    }

    public void ClosePauseHold()
    {
        if (_openPauseHoldCoroutine != null)
        {
            _pauseSlider.gameObject.SetActive(false);
            StopCoroutine(_openPauseHoldCoroutine);
        }
    }

    public void QuitMatch()
    {
        SceneSettings.ControllerOne = "";
        SceneSettings.ControllerTwo = "";
    }

    public void OpenPause()
    {
        Time.timeScale = 0.0f;
        GameManager.Instance.DisableAllInput();
        GameManager.Instance.PauseMusic();
        _pauseMenu.Show();
    }

    public void ClosePause()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.EnableAllInput();
        GameManager.Instance.PlayMusic();
        _pauseMenu.Hide();
    }

    IEnumerator ResetComboCoroutine()
    {
        _hasComboEnded = true;
        yield return new WaitForSeconds(1.0f);
        _comboText.gameObject.SetActive(false);
        _currentComboCount = 0;
        _comboText.text = "Hits 0";
    }
}
