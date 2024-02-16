using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Animator[] _lostLivesAnimator = default;
    [SerializeField] private GameObject _overlayUI = default;
    [SerializeField] private GameObject _otherOverlayUI = default;
    [SerializeField] private Slider _healthSlider = default;
    [SerializeField] private Slider _healthDamagedSlider = default;
    [SerializeField] private Slider _healthRecoverableSlider = default;
    [SerializeField] private Slider _arcanaSlider = default;
    [SerializeField] private Slider _assistSlider = default;
    [SerializeField] private Image _arcanaBackground = default;
    [SerializeField] private Image _portraitImage = default;
    [SerializeField] private Image _assistBorder = default;
    [SerializeField] private Notification _notification = default;
    [SerializeField] private TextMeshProUGUI _characterName = default;
    [SerializeField] private TextMeshProUGUI _playerName = default;
    [SerializeField] private TextMeshProUGUI _assistName = default;
    [SerializeField] private TextMeshProUGUI _arcanaAmountText = default;
    [SerializeField] private TextMeshProUGUI _hitsNumberText = default;
    [SerializeField] private TextMeshProUGUI _maxArcanaText = default;
    [SerializeField] private Animator _arcanaAnimator = default;
    [SerializeField] private Animator _comboAnimator = default;
    [SerializeField] private Slider _pauseSlider = default;
    [SerializeField] private Slider _comboTimerSlider = default;
    [SerializeField] private Image _comboTimerImage = default;
    [SerializeField] private Image _arcanaFill = default;
    [SerializeField] private Image _comboTimerLock = default;
    [SerializeField] private Image _borderHealth = default;
    [SerializeField] private Image _borderPortrait = default;
    [SerializeField] private Image _darkScreen = default;
    [SerializeField] private Sprite _assistEmpty = default;
    [SerializeField] private Sprite _assistHalf = default;
    [SerializeField] private Sprite _assistFull = default;
    [SerializeField] private PauseMenu _pauseMenu = default;
    [SerializeField] private PauseMenu _trainingPauseMenu = default;
    [SerializeField] private PauseMenu _replayPauseMenu = default;
    [SerializeField] private TrainingMenu _trainingMenu = default;
    [SerializeField] private DisconnectMenu _disconnectMenu = default;
    [SerializeField] private TextMeshProUGUI _overheadText = default;
    [SerializeField] private Audio _audio = default;
    [SerializeField] private Color _healthNormalColor = default;
    [SerializeField] private Color _healthLimitColor = default;
    [SerializeField] private Color _healthDamagedColor = default;
    [SerializeField] private Color _arcanaAvailableColor = default;
    [SerializeField] private Color _arcanaUnavailableColor = default;
    [SerializeField] private Color _arcanaMeterBackColor = default;
    [SerializeField] private Color _arcanaMeter1Color = default;
    [SerializeField] private Color _arcanaMeter2Color = default;
    [SerializeField] private Color _arcanaMeter3Color = default;
    [Header("1BitVisuals")]
    [SerializeField] private Image _healthImage = default;
    private Coroutine _openPauseHoldCoroutine;
    private Coroutine _notificiationCoroutine;
    private Coroutine _resetComboCoroutine;
    private Coroutine _showPlayerIconCoroutine;
    private Coroutine _healthCoroutine;
    private Coroutine _damagedHealthCoroutine;
    private Coroutine _damagedCoroutine;
    private Coroutine _arcanaCoroutine;
    private Animator _animator;
    private BrainController _controller;
    private RectTransform _comboGroup;
    private Color _healthCurrentColor;
    private float _currentEndDamageValue;
    private int _currentLifeIndex;
    private bool _hasComboEnded;
    private bool _initializedStats;
    public string PlayerName { get; private set; }
    public string CharacterName { get; private set; }

    public int CurrentComboCount { get; private set; }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _hitsNumberText.transform.parent.parent.gameObject.SetActive(false);
        _notification.gameObject.SetActive(false);
        _comboTimerSlider.transform.GetChild(0).gameObject.SetActive(false);
        _comboTimerSlider.transform.GetChild(1).gameObject.SetActive(false);
        _comboGroup = _hitsNumberText.transform.parent.parent.GetComponent<RectTransform>();
    }

    public void InitializeUI(PlayerStatsSO playerStats, BrainController controller, TextMeshProUGUI overhead)
    {
        _overheadText = overhead;
        _borderHealth.color = Color.white;
        _borderPortrait.color = Color.white;
        _healthCurrentColor = _healthNormalColor;
        _healthImage.color = _healthCurrentColor;
        _controller = controller;
        if (!_initializedStats)
        {
            if (_controller.IsPlayerOne)
            {
                if (SceneSettings.NameOne == "")
                {

                    if (SceneSettings.ControllerOne == null)
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
                    PlayerName = SceneSettings.NameOne;
                    _playerName.text = PlayerName;
                }

            }
            else
            {
                if (SceneSettings.NameTwo == "")
                {
                    if (SceneSettings.ControllerTwo == null)
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
                else
                {
                    PlayerName = SceneSettings.NameTwo;
                    _playerName.text = PlayerName;
                }
            }
            CharacterName = Regex.Replace(playerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
            _characterName.text = CharacterName;
            if (_controller.IsPlayerOne)
            {
                SetPortrait(playerStats.portraits[SceneSettings.ColorOne]);
            }
            else
            {
                SetPortrait(playerStats.portraits[SceneSettings.ColorTwo]);
            }
            SetMaxHealth(playerStats.maxHealth);
            SetMaxArcana(PlayerStatsSO.ARCANA_MULTIPLIER);
            _initializedStats = true;
        }
    }

    public void SetDarkScreen(bool state)
    {
        _darkScreen.gameObject.SetActive(state);
        _overlayUI.gameObject.SetActive(!state);
        _otherOverlayUI.gameObject.SetActive(!state);
        _trainingMenu.CanvasGroup.alpha = state ? 0 : 1;
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
        _playerName.text = PlayerName;
    }

    public void SetAssistName(string name)
    {
        _assistName.text = name;
    }

    private void SetPortrait(Sprite portrait)
    {
        _portraitImage.sprite = portrait;
    }

    private void SetMaxHealth(float value)
    {
        _healthSlider.maxValue = value;
        _healthDamagedSlider.maxValue = value;
        _healthDamagedSlider.value = value;
        _healthRecoverableSlider.maxValue = value;
        _healthRecoverableSlider.value = value;
    }

    private void SetMaxArcana(float value)
    {
        float arcanaSliderWidth = _arcanaSlider.GetComponent<RectTransform>().sizeDelta.x;
        _arcanaSlider.maxValue = value;
    }

    public void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }

    public void DecreaseArcana()
    {
        _arcanaAnimator.SetTrigger("Decrease");
    }

    public void ShowYouOverhead()
    {
        StartCoroutine(ShowYouOverheadCoroutine());
    }

    private IEnumerator ShowYouOverheadCoroutine()
    {
        _overheadText.text = "You";
        _overheadText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        _overheadText.gameObject.SetActive(false);
    }

    public void SetArcana(float value)
    {
        int arcana = Mathf.FloorToInt(value / PlayerStatsSO.ARCANA_MULTIPLIER);
        if (value != 3000)
        {
            value = value - (PlayerStatsSO.ARCANA_MULTIPLIER * arcana);
            _maxArcanaText.gameObject.SetActive(false);
        }
        else if (!_maxArcanaText.gameObject.activeSelf)
        {
            _arcanaBackground.color = _arcanaMeter2Color;
            _arcanaFill.color = _arcanaMeter3Color;
            _audio.Sound("MaxArcana").Play();
            _maxArcanaText.gameObject.SetActive(true);
        }
        _arcanaSlider.value = value;
        _arcanaAmountText.text = arcana.ToString();
        if (arcana == 0)
        {
            _arcanaAmountText.color = _arcanaUnavailableColor;
            _arcanaBackground.color = _arcanaMeterBackColor;
            _arcanaFill.color = _arcanaMeter1Color;
        }
        else
        {
            _arcanaAmountText.color = _arcanaAvailableColor;
            if (arcana == 1)
            {
                _arcanaBackground.color = _arcanaMeter1Color;
                _arcanaFill.color = _arcanaMeter2Color;
            }
            else if (arcana == 2)
            {
                _arcanaBackground.color = _arcanaMeter2Color;
                _arcanaFill.color = _arcanaMeter3Color;
            }
        }
    }
    public void SetAssist(int value)
    {
        if (value >= GameSimulation.maxShadowGauge)
            _assistBorder.sprite = _assistFull;
        else if (value >= GameSimulation.maxShadowGauge / 2)
            _assistBorder.sprite = _assistHalf;
        else
            _assistBorder.sprite = _assistEmpty;
        _assistSlider.value = value;
    }

    public void SetHealth(int value)
    {
        if (value <= 3000)
        {
            _borderHealth.color = Color.red;
            _borderPortrait.color = Color.red;
            _healthCurrentColor = _healthLimitColor;
        }
        _healthImage.color = _healthCurrentColor;
        _healthSlider.value = value;
    }

    public void CheckDemonLimit(int value)
    {
        if (value > 3000)
        {
            _borderHealth.color = Color.white;
            _borderPortrait.color = Color.white;
            _healthCurrentColor = _healthNormalColor;
            _healthImage.color = _healthCurrentColor;
        }
    }

    public void UpdateHealth()
    {
        if (_healthCoroutine != null)
        {
            StopCoroutine(_healthCoroutine);
        }
        _healthCoroutine = StartCoroutine(SetHealthCoroutine());
    }
    public void Disconnected()
    {
        Time.timeScale = 0;
        GameplayManager.Instance.DisableAllInput(true);
        GameplayManager.Instance.PauseMusic();
        _disconnectMenu.Show();
    }

    IEnumerator SetHealthCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        float startValue = _healthSlider.value;
        float endValue = _healthRecoverableSlider.value;
        float elapsedTime = 0;
        float duration = 0.1f;
        while (elapsedTime < duration)
        {
            _healthSlider.value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _healthSlider.value = endValue;
    }

    public void SetRecoverableHealth(int value)
    {
        _healthRecoverableSlider.value = value;
    }

    public void MaxHealth(float value)
    {
        _healthSlider.value = value;
        _healthDamagedSlider.value = value;
        if (_damagedHealthCoroutine != null)
        {
            StopCoroutine(_damagedHealthCoroutine);
        }
    }

    public void Damaged()
    {
        if (_damagedCoroutine != null)
            StopCoroutine(_damagedCoroutine);
        _damagedCoroutine = StartCoroutine(DamagedCoroutine());
    }

    IEnumerator DamagedCoroutine()
    {
        _healthImage.color = _healthDamagedColor;
        _portraitImage.color = _healthDamagedColor;
        yield return new WaitForSecondsRealtime(0.04f);
        _healthImage.color = _healthCurrentColor;
        _portraitImage.color = Color.white;
    }

    public void ResetHealthDamaged()
    {
        if (_damagedHealthCoroutine != null)
            StopCoroutine(_damagedHealthCoroutine);
        _healthDamagedSlider.value = _healthSlider.maxValue;
    }
    public void SetHealthDamaged(float value)
    {
        if (_damagedHealthCoroutine != null)
            StopCoroutine(_damagedHealthCoroutine);
        _healthDamagedSlider.value = value;
    }

    public void UpdateHealthDamaged(int healthRecoverable)
    {
        SetRecoverableHealth(healthRecoverable);
        if (_damagedHealthCoroutine != null)
            StopCoroutine(_damagedHealthCoroutine);
        _damagedHealthCoroutine = StartCoroutine(SetHealthDamagedCoroutine(_healthRecoverableSlider.value));
    }

    IEnumerator SetHealthDamagedCoroutine(float value)
    {
        yield return new WaitForSeconds(0.5f);
        float startValue = _healthDamagedSlider.value;
        _currentEndDamageValue = value;
        float elapsedTime = 0.0f;
        float duration = 0.2f;
        while (elapsedTime < duration)
        {
            _healthDamagedSlider.value = Mathf.Lerp(startValue, _currentEndDamageValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _healthDamagedSlider.value = _currentEndDamageValue;
    }

    public void SetLives()
    {
        _lostLivesAnimator[_currentLifeIndex].Play("LifeLost");
        _currentLifeIndex++;
    }

    public void SetComboTimer(DemonFloat value, Color color)
    {
        _comboTimerImage.color = color;
        _comboTimerSlider.value = (float)value;
    }

    public void SetComboTimerActive(bool state)
    {
        _comboTimerSlider.transform.GetChild(0).gameObject.SetActive(state);
        _comboTimerSlider.transform.GetChild(1).gameObject.SetActive(state);
    }

    public void SetComboTimerLock(bool state) => _comboTimerLock.gameObject.SetActive(state);

    public void ResetLives()
    {
        _lostLivesAnimator[0].Rebind();
        _lostLivesAnimator[1].Rebind();
        _currentLifeIndex = 0;
    }

    public void OpenPauseHold(bool isPlayerOne)
    {
        _pauseSlider.gameObject.SetActive(true);
        _openPauseHoldCoroutine = StartCoroutine(OpenPauseHoldCoroutine(isPlayerOne));
    }

    IEnumerator OpenPauseHoldCoroutine(bool isPlayerOne)
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
        OpenPause(isPlayerOne);
    }

    public void ClosePauseHold()
    {
        if (_openPauseHoldCoroutine != null)
        {
            _pauseSlider.gameObject.SetActive(false);
            StopCoroutine(_openPauseHoldCoroutine);
        }
    }

    public void ChangeCharacter()
    {
        _trainingMenu.ResetTrainingOptions();
        Time.timeScale = 1.0f;
        SceneSettings.SceneSettingsDecide = false;
        SceneSettings.ChangeCharacter = true;
        SceneManager.LoadScene(0);
    }

    public void QuitMatch()
    {
        _trainingMenu.ResetTrainingOptions();
        Time.timeScale = 1.0f;
        SceneSettings.SceneSettingsDecide = false;
        SceneSettings.ChangeCharacter = false;
        SceneSettings.ReplayMode = false;
        SceneManager.LoadScene(0);
    }

    public void OpenPause(bool isPlayerOne)
    {
        MouseSetup.Instance.SetLock(false);
        _pauseMenu.SetWhoPaused(isPlayerOne);
        Time.timeScale = 0;
        GameplayManager.Instance.PausedController = _controller;
        GameplayManager.Instance.DisableAllInput(isPlayerOne);
        GameplayManager.Instance.PauseMusic();
        _pauseMenu.PlayerInput = GameplayManager.Instance.PlayerInput;
        _pauseMenu.Show();
    }

    public void OpenTrainingPause(bool isPlayerOne)
    {
        MouseSetup.Instance.SetLock(false);
        _trainingPauseMenu.SetWhoPaused(isPlayerOne);
        Time.timeScale = 0;
        GameplayManager.Instance.PausedController = _controller;
        GameplayManager.Instance.DisableAllInput(isPlayerOne);
        GameplayManager.Instance.PauseMusic();
        _trainingPauseMenu.PlayerInput = GameplayManager.Instance.PlayerInput;
        _trainingPauseMenu.Show();
    }

    public void ClosePause()
    {
        MouseSetup.Instance.SetLock(true);
        Time.timeScale = GameplayManager.Instance.GameSpeed;
        GameplayManager.Instance.EnableAllInput();
        GameplayManager.Instance.PlayMusic();
        _pauseMenu.Hide();
        _trainingPauseMenu.Hide();
        _replayPauseMenu.Hide();
    }

    public void IncreaseCombo(int combo)
    {
        if (_hasComboEnded)
        {
            _comboAnimator.Rebind();
            StopCoroutine(_resetComboCoroutine);
            _hasComboEnded = false;
            _hitsNumberText.transform.parent.parent.gameObject.SetActive(false);
            _hitsNumberText.text = "0 Hits";
            if (_comboGroup.anchoredPosition.x == -110.0f)
            {
                _comboGroup.anchoredPosition = new Vector2(-40.0f, _comboGroup.anchoredPosition.y);
            }
        }
        _hitsNumberText.text = combo.ToString();
        if (combo > 1)
        {
            _hitsNumberText.transform.parent.parent.gameObject.SetActive(false);
            _hitsNumberText.transform.parent.parent.gameObject.SetActive(true);
        }
        if (combo >= 10 && _comboGroup.anchoredPosition.x == -40.0f)
        {
            _comboGroup.anchoredPosition = new Vector2(-110.0f, _comboGroup.anchoredPosition.y);
        }
    }

    public void ResetCombo()
    {
        _hasComboEnded = true;
        _resetComboCoroutine = StartCoroutine(ResetComboCoroutine());
    }

    public void DisplayNotification(NotificationTypeEnum notificationType)
    {
        _notification.SetNotification(notificationType);
        _notification.gameObject.SetActive(false);
        _notification.gameObject.SetActive(true);
        if (_notificiationCoroutine != null)
            StopCoroutine(_notificiationCoroutine);
        _notificiationCoroutine = StartCoroutine(ResetDisplayNotificationCoroutine());
    }

    IEnumerator ResetDisplayNotificationCoroutine()
    {
        yield return new WaitForSeconds(1.1f);
        _notification.gameObject.SetActive(false);
    }

    IEnumerator ResetComboCoroutine()
    {
        _comboAnimator.Play("ComboEnded");
        yield return new WaitForSeconds(1.0f);
        _hitsNumberText.transform.parent.parent.gameObject.SetActive(false);
        CurrentComboCount = 0;
        _hitsNumberText.text = "";
    }

    public void ShowPlayerIcon()
    {
        if (!SceneSettings.IsTrainingMode)
            return;
        if (_showPlayerIconCoroutine != null)
        {
            _overheadText.gameObject.SetActive(false);
            StopCoroutine(_showPlayerIconCoroutine);
        }
        _showPlayerIconCoroutine = StartCoroutine(ShowPlayerIconCoroutine());
    }

    IEnumerator ShowPlayerIconCoroutine()
    {
        int index = _controller.IsPlayerOne == true ? 0 : 1;
        _overheadText.text = $"P{index + 1}";
        _overheadText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _overheadText.gameObject.SetActive(false);
    }
}
