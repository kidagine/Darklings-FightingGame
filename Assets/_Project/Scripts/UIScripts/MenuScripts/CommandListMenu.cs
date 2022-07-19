using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class CommandListMenu : BaseMenu
{
	[SerializeField] private TextMeshProUGUI _characterText = default;
	[SerializeField] private TextMeshProUGUI _descriptionText = default;
	[SerializeField] private VideoPlayer _showcaseVideo = default;
	[SerializeField] private PauseMenu _pauseMenu = default;
	[SerializeField] private PauseMenu _pauseTrainingMenu = default;
	[SerializeField] private CommandListButton[] _commandListButtons = default;
	[SerializeField] private GameObject _knockdownImage = default;
	[SerializeField] private GameObject _reversalImage = default;
	[SerializeField] private GameObject _projectileImage = default;
	private PauseMenu _currentPauseMenu;
	private readonly string _baseUrl = "https://kidagine.github.io/Darklings-CommandListVideos/";

	private Player _playerOne;
	private Player _playerTwo;
	private Player _currentlyDisplayedPlayer;


	void Awake()
	{
		_playerOne = GameManager.Instance.PlayerOne;
		_playerTwo = GameManager.Instance.PlayerTwo;
	}

	void Update()
	{
		if (Input.GetButtonDown(_currentPauseMenu.PauseControllerType + "UILeft")
			|| Input.GetButtonDown(_currentPauseMenu.PauseControllerType + "UIRight"))
		{
			if (_playerOne == _currentlyDisplayedPlayer)
			{
				_currentlyDisplayedPlayer = _playerTwo;
			}
			else
			{
				_currentlyDisplayedPlayer = _playerOne;
			}
			SetCommandListData(_currentlyDisplayedPlayer.PlayerStats);
			EventSystem.current.SetSelectedGameObject(null);
			_startingOption.Select();
		}
	}

	private void SetCommandListData(PlayerStatsSO playerStats)
	{
		_characterText.text = playerStats.characterName.ToString();
		_commandListButtons[0].SetData(playerStats.m5Arcana);
		_commandListButtons[1].SetData(playerStats.m2Arcana);
		if (playerStats.jArcana != null)
		{
			_commandListButtons[2].SetData(playerStats.jArcana);
			_commandListButtons[2].gameObject.SetActive(true);
		}
		else
		{
			_commandListButtons[2].gameObject.SetActive(false);
		}
	}

	public void SetCommandListShowcase(ArcanaSO command)
	{
		_descriptionText.text = command.arcanaDescription;
#if UNITY_STANDALONE_WIN
		_showcaseVideo.clip = command.arcanaVideo;
#endif
#if UNITY_WEBGL
		_showcaseVideo.url = _baseUrl + command.arcanaVideo.name + ".mp4";
#endif
		_showcaseVideo.Stop();
		_showcaseVideo.Play();
		_reversalImage.SetActive(false);
		_knockdownImage.SetActive(false);
		_projectileImage.SetActive(false);
		if (command.reversal)
		{
			_reversalImage.SetActive(true);
		}
		if (command.causesKnockdown)
		{
			_knockdownImage.SetActive(true);
		}
		if (command.isProjectile)
		{
			_projectileImage.SetActive(true);
		}
	}

	public void Back()
	{
		_currentPauseMenu.Show();
		Hide();
	}

	private void OnEnable()
	{
		if (GameManager.Instance.IsTrainingMode)
		{
			_currentPauseMenu = _pauseTrainingMenu;
		}
		else
		{
			_currentPauseMenu = _pauseMenu;
		}
		if (_pauseMenu.PlayerOnePaused)
		{
			_currentlyDisplayedPlayer = _playerOne;
		}
		else
		{
			_currentlyDisplayedPlayer = _playerTwo;
		}
		SetCommandListData(_currentlyDisplayedPlayer.PlayerStats);
	}
}
