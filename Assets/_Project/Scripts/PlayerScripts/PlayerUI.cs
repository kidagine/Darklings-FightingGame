using Demonics.Sounds;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	[SerializeField] private Animator[] _lostLivesAnimator = default;
	[SerializeField] private Slider _healthSlider = default;
	[SerializeField] private Slider _healthDamagedSlider = default;
	[SerializeField] private Slider _arcanaSlider = default;
	[SerializeField] private Slider _assistSlider = default;
	[SerializeField] private Image _portraitImage = default;
	[SerializeField] private TextMeshProUGUI _characterName = default;
	[SerializeField] private TextMeshProUGUI _playerName = default;
	[SerializeField] private TextMeshProUGUI _assistName = default;
	[SerializeField] private TextMeshProUGUI _notificationText = default;
	[SerializeField] private TextMeshProUGUI _comboText = default;
	[SerializeField] private TextMeshProUGUI _whoPausedText = default;
	[SerializeField] private TextMeshProUGUI _whoPausedTrainingText = default;
	[SerializeField] private TextMeshProUGUI _arcanaAmountText = default;
	[SerializeField] private Animator _arcanaAnimator = default;
	[SerializeField] private Transform _healthDividerPivot = default;
	[SerializeField] private GameObject _healthDividerPrefab = default;
	[SerializeField] private Transform _arcanaDividerPivot = default;
	[SerializeField] private GameObject _arcanaDividerPrefab = default;
	[SerializeField] private Slider _pauseSlider = default;
	[SerializeField] private PauseMenu _pauseMenu = default;
	[SerializeField] private PauseMenu _trainingPauseMenu = default;
	[SerializeField] private TrainingMenu _trainingMenu = default;
	[Header("1BitVisuals")]
	[SerializeField] private Image _healthImage = default;
	[SerializeField] private Image _arcanaImage = default;
	[SerializeField] private Image _assistImage = default;
	[SerializeField] private Image[] _heartImages = default;
	private GameObject[] _playerIcons;
	private Coroutine _openPauseHoldCoroutine;
	private Coroutine _notificiationCoroutine;
	private Coroutine _resetComboCoroutine;
	private Coroutine _showPlayerIconCoroutine;
	private Coroutine _damagedHealthCoroutine;
	private Animator _animator;
	private Audio _audio;
	private BrainController _controller;
	private float _currentEndDamageValue;
	private int _currentLifeIndex;
	private int _currentComboCount;
	private bool _hasComboEnded;
	private bool _initializedStats;
	public string PlayerName { get; private set; }
	public string CharacterName { get; private set; }


	void Awake()
	{
		_animator = GetComponent<Animator>();
		_audio = GetComponent<Audio>();
		_comboText.transform.parent.gameObject.SetActive(false);
		_notificationText.transform.parent.gameObject.SetActive(false);
	}

	public void InitializeUI(PlayerStatsSO playerStats, BrainController controller, GameObject[] playerIcons)
	{
		_playerIcons = playerIcons;
		_controller = controller;
		if (!_initializedStats)
		{
			if (_controller.IsPlayerOne)
			{
				if (SceneSettings.NameOne == "")
				{

					if (SceneSettings.ControllerOne == "Cpu")
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
					if (SceneSettings.ControllerTwo == "Cpu")
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
			CharacterName = playerStats.characterName.ToString();
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
			SetMaxArcana(playerStats.maxArcana);
			_initializedStats = true;
		}
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
		float healthSliderWidth = _healthSlider.GetComponent<RectTransform>().sizeDelta.x;
		_healthSlider.maxValue = value;
		_healthDamagedSlider.maxValue = value;
		_healthDamagedSlider.value = value;
		float increaseValue = healthSliderWidth / value;
		float currentPositionX = 0.0f;
		for (int i = 0; i < value + 1; i++)
		{
			GameObject healthDivider = Instantiate(_healthDividerPrefab, _healthDividerPivot);
			healthDivider.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPositionX, 24.0f);
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

	public void DecreaseArcana()
	{
		_arcanaAnimator.SetTrigger("Decrease");
	}

	public void SetArcana(float value)
	{
		_arcanaSlider.value = value;
		_arcanaAmountText.text = Mathf.Floor(value).ToString();
	}

	public void SetAssist(float value)
	{
		_assistSlider.value = value;
	}

	public void SetHealth(float value)
	{
		_healthSlider.value = value;
	}

	public void ResetHealthDamaged()
	{
		if (_damagedHealthCoroutine != null)
		{
			StopCoroutine(_damagedHealthCoroutine);
		}
		_healthDamagedSlider.value = _healthSlider.maxValue;
	}

	public void UpdateHealthDamaged()
	{
		if (_damagedHealthCoroutine != null)
		{
			StopCoroutine(_damagedHealthCoroutine);
		}
		_damagedHealthCoroutine = StartCoroutine(SetHealthDamagedCoroutine(_healthSlider.value));
	}

	IEnumerator SetHealthDamagedCoroutine(float value)
	{
		yield return new WaitForSeconds(1.0f);
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
		SceneSettings.ChangeCharacter = true;
		SceneManager.LoadScene(1);
	}

	public void QuitMatch()
	{
		_trainingMenu.ResetTrainingOptions();
		Time.timeScale = 1.0f;
		SceneSettings.ChangeCharacter = false;
		SceneManager.LoadScene(1);
	}

	public void OpenPause(bool isPlayerOne)
	{
		if (isPlayerOne)
		{
			_whoPausedText.text = "Player 1 Paused";
		}
		else
		{
			_whoPausedText.text = "Player 2 Paused";
		}
		Time.timeScale = 0.0f;
		GameManager.Instance.DisableAllInput();
		GameManager.Instance.PauseMusic();
		_trainingPauseMenu.PauseControllerType = _controller.ControllerInputName;
		_pauseMenu.Show();
	}

	public void OpenTrainingPause(bool isPlayerOne)
	{
		if (isPlayerOne)
		{
			_whoPausedTrainingText.text = "Player 1 Paused";
		}
		else
		{
			_whoPausedTrainingText.text = "Player 2 Paused";
		}
		Time.timeScale = 0.0f;
		GameManager.Instance.DisableAllInput();
		GameManager.Instance.PauseMusic();
		_trainingPauseMenu.PauseControllerType = _controller.ControllerInputName;
		_trainingPauseMenu.Show();
	}

	public void ClosePause()
	{
		Time.timeScale = GameManager.Instance.GameSpeed;
		GameManager.Instance.EnableAllInput();
		GameManager.Instance.PlayMusic();
		_pauseMenu.Hide();
		_trainingPauseMenu.Hide();
	}

	public void IncreaseCombo()
	{
		if (_hasComboEnded)
		{
			StopCoroutine(_resetComboCoroutine);
			_hasComboEnded = false;
			_comboText.transform.parent.gameObject.SetActive(false);
			_currentComboCount = 0;
			_comboText.text = "Hits 0";
		}
		_currentComboCount++;
		_comboText.text = "Hits " + _currentComboCount.ToString();
		if (_currentComboCount > 1)
		{
			_comboText.transform.parent.gameObject.SetActive(false);
			_comboText.transform.parent.gameObject.SetActive(true);
		}
	}

	public void ResetCombo()
	{
		_hasComboEnded = true;
		_resetComboCoroutine = StartCoroutine(ResetComboCoroutine());
	}

	public void DisplayNotification(string text)
	{
		_audio.Sound("Punish").Play();
		_notificationText.transform.parent.gameObject.SetActive(true);
		_notificationText.text = text;
		if (_notificiationCoroutine != null)
		{
			StopCoroutine(_notificiationCoroutine);
		}
		_notificiationCoroutine = StartCoroutine(ResetDisplayNotificationCoroutine());
	}

	IEnumerator ResetDisplayNotificationCoroutine()
	{
		yield return new WaitForSeconds(1.0f);
		_notificationText.transform.parent.gameObject.SetActive(false);
		_notificationText.text = "";
	}

	IEnumerator ResetComboCoroutine()
	{
		yield return new WaitForSeconds(1.0f);
		_comboText.transform.parent.gameObject.SetActive(false);
		_currentComboCount = 0;
		_comboText.text = "";
	}

	public void ShowPlayerIcon()
	{
		if (_showPlayerIconCoroutine != null)
		{
			for (int i = 0; i < _playerIcons.Length; i++)
			{
				_playerIcons[i].SetActive(false);
			}
			StopCoroutine(_showPlayerIconCoroutine);
		}
		_showPlayerIconCoroutine = StartCoroutine(ShowPlayerIconCoroutine());
	}

	IEnumerator ShowPlayerIconCoroutine()
	{
		int index = _controller.IsPlayerOne == true ? 0 : 1;
		_playerIcons[index].SetActive(true);
		yield return new WaitForSeconds(1.0f);
		_playerIcons[index].SetActive(false);
	}

	public void Turn1BitVisuals()
	{
		_healthImage.color = Color.white;
		_arcanaImage.color = Color.white;
		_assistImage.color = Color.white;
		for (int i = 0; i < _heartImages.Length; i++)
		{
			_heartImages[i].color = Color.white;
		}
	}
}
