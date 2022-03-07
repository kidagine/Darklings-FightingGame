using Demonics.UI;
using TMPro;
using UnityEngine;

public class TrainingMenu : BaseMenu
{
	[SerializeField] private GameObject _p1 = default;
	[SerializeField] private GameObject _p2 = default;
	[SerializeField] private InputHistory _inputHistoryOne = default;
	[SerializeField] private InputHistory _inputHistoryTwo = default;
	[SerializeField] private TextMeshProUGUI _startupOneText = default;
	[SerializeField] private TextMeshProUGUI _activeOneText = default;
	[SerializeField] private TextMeshProUGUI _recoveryOneText = default;
	[SerializeField] private TextMeshProUGUI _startupTwoText = default;
	[SerializeField] private TextMeshProUGUI _activeTwoText = default;
	[SerializeField] private TextMeshProUGUI _recoveryTwoText = default;
	[SerializeField] private RectTransform _scrollView = default;
	[SerializeField] private BaseMenu _trainingPauseMenu = default;
	[Header("Selectors")]
	private TrainingMenu _trainingMenu;


	void Start()
	{
		_trainingMenu = GetComponent<TrainingMenu>();
	}

	void Update()
	{
		if (Input.GetButtonDown("ControllerOne" + "Pause") || Input.GetButtonDown("ControllerTwo" + "Pause") || Input.GetButtonDown("Keyboard" + "Pause"))
		{
			_trainingMenu.OpenMenuHideCurrent(_trainingPauseMenu);
		}
	}

	public void SetHitboxes(int value)
	{
		if (value == 1)
		{
			TrainingSettings.ShowHitboxes = true;
		}
		else
		{
			TrainingSettings.ShowHitboxes = false;
		}
	}

	public void SetSlowdown(int value)
	{
		switch (value)
		{
			case 0:
				GameManager.Instance.GameSpeed = 1.0f;
				break;
			case 1:
				GameManager.Instance.GameSpeed = 0.75f;
				break;
			case 2:
				GameManager.Instance.GameSpeed = 0.5f;
				break;
			case 3:
				GameManager.Instance.GameSpeed = 0.25f;
				break;
			case 4:
				GameManager.Instance.GameSpeed = 0.10f;
				break;
		}
	}


	public void SetCpu(int value)
	{
		switch (value)
		{
			case 0:
				GameManager.Instance.IsCpuOff = true;
				GameManager.Instance.DeactivateCpus();
				break;
			case 1:
				GameManager.Instance.IsCpuOff = false;
				GameManager.Instance.DeactivateCpus();
				GameManager.Instance.ActivateCpus();
				break;
		}
	}

	public void SetArcana(int value)
	{
		switch (value)
		{
			case 0:
				GameManager.Instance.InfiniteArcana = false;
				break;
			case 1:
				GameManager.Instance.InfiniteArcana = true;
				break;
		}
	}

	public void SetAssist(int value)
	{
		switch (value)
		{
			case 0:
				GameManager.Instance.InfiniteAssist = false;
				break;
			case 1:
				GameManager.Instance.InfiniteAssist = true;
				break;
		}
	}

	public void SetHealth(int value)
	{
		switch (value)
		{
			case 0:
				GameManager.Instance.InfiniteHealth = false;
				break;
			case 1:
				GameManager.Instance.MaxHealths();
				GameManager.Instance.InfiniteHealth = true;
				break;
		}
	}


	public void SetDisplay(int value)
	{
		switch (value)
		{
			case 0:
				_p1.SetActive(false);
				_p2.SetActive(false);
				break;
			case 1:
				_p1.SetActive(true);
				_p2.SetActive(false);
				break;
			case 2:
				_p1.SetActive(false);
				_p2.SetActive(true);
				break;
			case 3:
				_p1.SetActive(true);
				_p2.SetActive(true);
				break;
		}
	}

	public void FramedataValue(bool isPlayerOne, int startupFrames, int activeFrames, int recoveryFrames)
	{
		if (isPlayerOne)
		{
			if (_startupOneText.gameObject.activeSelf)
			{
				_startupOneText.text = startupFrames.ToString();
			}
			if (_activeOneText.gameObject.activeSelf)
			{
				_activeOneText.text = activeFrames.ToString();
			}
			if (_recoveryOneText.gameObject.activeSelf)
			{
				_recoveryOneText.text = recoveryFrames.ToString();
			}
		}
		else
		{
			if (_startupTwoText.gameObject.activeSelf)
			{
				_startupTwoText.text = startupFrames.ToString();
			}
			if (_activeTwoText.gameObject.activeSelf)
			{
				_activeTwoText.text = activeFrames.ToString();
			}
			if (_recoveryTwoText.gameObject.activeSelf)
			{
				_recoveryTwoText.text = recoveryFrames.ToString();
			}
		}
	}

	public void SetInputHistory(int value)
	{
		switch (value)
		{
			case 0:
				_inputHistoryOne.gameObject.SetActive(false);
				_inputHistoryTwo.gameObject.SetActive(false);
				break;
			case 1:
				_inputHistoryOne.gameObject.SetActive(true);
				_inputHistoryTwo.gameObject.SetActive(true);
				break;
		}
	}

	public void SetBlock(int value)
	{
		switch (value)
		{
			case 0:
				TrainingSettings.BlockAlways = false;
				break;
			case 1:
				TrainingSettings.BlockAlways = true;
				break;
		}
	}

	public void RestoreToDefault()
	{

	}

	public void ResetTrainingOptions()
	{
		TrainingSettings.ShowHitboxes = false;
		GameManager.Instance.InfiniteHealth = false;
		GameManager.Instance.InfiniteArcana = false;
		GameManager.Instance.GameSpeed = 1.0f;
		GameManager.Instance.ActivateCpus();
	}

	private void OnEnable()
	{
		_scrollView.anchoredPosition = Vector2.zero;
	}
}
