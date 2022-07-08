using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CommandListMenu : BaseMenu
{
	[SerializeField] private TextMeshProUGUI _characterText = default;
	[SerializeField] private TextMeshProUGUI _descriptionText = default;
	[SerializeField] private VideoPlayer _showcaseVideo = default;
	[SerializeField] private PauseMenu _pauseMenu = default;
	[SerializeField] private CommandListButton[] _commandListButtons = default;
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
		if (Input.GetButtonDown(_pauseMenu.PauseControllerType + "UILeft")
			|| Input.GetButtonDown(_pauseMenu.PauseControllerType + "UIRight"))
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
		_showcaseVideo.clip = command.arcanaVideo;
		_showcaseVideo.Stop();
		_showcaseVideo.Play();
	}

	private void OnEnable()
	{
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
