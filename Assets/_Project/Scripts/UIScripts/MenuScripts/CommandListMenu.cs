using Demonics.UI;
using TMPro;
using UnityEngine;

public class CommandListMenu : BaseMenu
{
	[SerializeField] private TextMeshProUGUI _characterText = default;
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
		_commandListButtons[0].SetData(playerStats.m5Arcana.arcanaName);
		_commandListButtons[1].SetData(playerStats.m2Arcana.arcanaName);
		if (playerStats.jArcana != null)
		{
			_commandListButtons[2].SetData(playerStats.jArcana.arcanaName);
			_commandListButtons[2].gameObject.SetActive(true);
		}
		else
		{
			_commandListButtons[2].gameObject.SetActive(false);
		}
	}

	private void OnEnable()
	{
		_currentlyDisplayedPlayer = _playerOne;
		SetCommandListData(_currentlyDisplayedPlayer.PlayerStats);
	}
}
