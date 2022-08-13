using Demonics.Sounds;
using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayersMenu : BaseMenu
{
	[SerializeField] private InputManager _inputManager = default;
	[SerializeField] private CharacterMenu _characterMenu = default;
	[SerializeField] private RectTransform[] _playerIcons = default;
	[SerializeField] private GameObject _cpuTextRight = default;
	[SerializeField] private GameObject _cpuTextLeft = default;
	[SerializeField] private BaseMenu _versusMenu = default;
	[SerializeField] private BaseMenu _practiceMenu = default;
	private Audio _audio;
	private int _currentIconIndex;
	private int _increment;
	private readonly float _left = -375.0f;
	private readonly float _right = 375.0f;
	private readonly float _center = 0.0f;
	public GameObject CpuTextRight { get { return _cpuTextRight; } private set { } }
	public GameObject CpuTextLeft{ get { return _cpuTextLeft; } private set { } }


	void Awake()
	{
		_audio = GetComponent<Audio>();
	}

	private void UpdateVisiblePlayers(InputDevice inputDevice, InputDeviceChange inputDeviceChange)
	{
		_increment = 0;
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			_playerIcons[i].gameObject.SetActive(false);
		}
		for (int i = 0; i < InputSystem.devices.Count; i++)
		{
			if (!InputSystem.devices[i].displayName.Contains("Mouse") && !InputSystem.devices[i].displayName.Contains("Touchscreen"))
			{
				if (_playerIcons.Length >= i)
				{
					_playerIcons[_increment].gameObject.SetActive(true);
					_playerIcons[_increment].GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = InputSystem.devices[i].displayName;
					_increment++;
				}
			}
		}
	}

	public void UpdateLeftRightCpu()
	{
		if (IsOnRight())
		{
			_cpuTextRight.SetActive(false);
		}
		else
		{
			_cpuTextRight.SetActive(true);
		}
		if (IsOnLeft())
		{
			_cpuTextLeft.SetActive(false);
		}
		else
		{
			_cpuTextLeft.SetActive(true);
		}
	}

	public bool IsOnRight()
	{
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			if (_playerIcons[i].anchoredPosition.x == _right)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsOnLeft()
	{
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			if (_playerIcons[i].anchoredPosition.x == _left)
			{
				return true;
			}
		}
		return false;
	}

	public void OpenOtherMenu(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			if (_playerIcons[0].anchoredPosition.x != _center || _playerIcons[1].anchoredPosition.x != _center || _playerIcons[2].anchoredPosition.x != _center)
			{
				_audio.Sound("Pressed").Play();
				if (_playerIcons[0].anchoredPosition.x == _right)
				{
					SceneSettings.ControllerTwo = 0;
				}
				else if (_playerIcons[1].anchoredPosition.x == _right)
				{
					SceneSettings.ControllerTwo = 2;
				}
				else if (_playerIcons[2].anchoredPosition.x == _right)
				{
					SceneSettings.ControllerTwo = 3;
				}
				else
				{
					SceneSettings.ControllerTwo = -1;
				}
				if (_playerIcons[0].anchoredPosition.x == _left)
				{
					SceneSettings.ControllerOne = 0;
				}
				else if (_playerIcons[1].anchoredPosition.x == _left)
				{
					SceneSettings.ControllerOne = 2;
				}
				else if (_playerIcons[2].anchoredPosition.x == _left)
				{
					SceneSettings.ControllerOne = 3;
				}
				else
				{
					SceneSettings.ControllerOne = -1;
				}
				if (_playerIcons[0].anchoredPosition.x != _center && _playerIcons[1].anchoredPosition.x != _center
					|| _playerIcons[0].anchoredPosition.x != _center && _playerIcons[2].anchoredPosition.x != _center
					|| _playerIcons[1].anchoredPosition.x != _center && _playerIcons[2].anchoredPosition.x != _center)
				{
					_characterMenu.EnablePlayerTwoSelector();
				}
				gameObject.SetActive(false);
				for (int i = 0; i < _playerIcons.Length; i++)
				{
					_playerIcons[i].GetComponent<PlayerIcon>().Center();
					_playerIcons[i].gameObject.SetActive(false);
				}
				_inputManager.gameObject.SetActive(true);
				_inputManager.PlayerInput.SwitchCurrentControlScheme(PlayerIcon.CurrentInputDevice);
				_characterMenu.Show();
			}
		}
	}

	public bool ArePlayerIconsLeft()
	{
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			if (_playerIcons[i].anchoredPosition.x != _left)
			{
				return false;
			}
		}
		return true;
	}

	public bool ArePlayerIconsRight()
	{
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			if (_playerIcons[i].anchoredPosition.x != _right)
			{
				return false;
			}
		}
		return true;
	}

	public void SetCurrentPlayerIcon(int index)
	{
		_currentIconIndex = index;
	}

	public void ConfirmQuickAssign(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			if (gameObject.activeSelf)
			{
				_audio.Sound("Selected").Play();
				_cpuTextLeft.SetActive(false);
				PlayerIcon.CurrentInputDevice = InputSystem.devices[_currentIconIndex];
				_playerIcons[_currentIconIndex].anchoredPosition = new Vector2(_left, 275.0f);
			}
		}
	}

	public void OpenKeyboardCoOp()
	{
		_audio.Sound("Pressed").Play();
		SceneSettings.ControllerTwo = 0;
		SceneSettings.ControllerOne = 0;
		gameObject.SetActive(false);
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			_playerIcons[i].gameObject.SetActive(false);
		}
		_inputManager.gameObject.SetActive(true);
		_inputManager.PlayerInput.SwitchCurrentControlScheme(PlayerIcon.CurrentInputDevice);
		_characterMenu.Show();
	}

	void OnDisable()
	{
		_cpuTextLeft.SetActive(true);
		_cpuTextRight.SetActive(true);
		InputSystem.onDeviceChange -= UpdateVisiblePlayers;
	}

	private void OnEnable()
	{
		_inputManager.gameObject.SetActive(false);
		InputSystem.onDeviceChange += UpdateVisiblePlayers;
		UpdateVisiblePlayers(null, default);
	}

	public void Back(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			for (int i = 0; i < _playerIcons.Length; i++)
			{
				_playerIcons[i].gameObject.SetActive(false);
			}
			_inputManager.gameObject.SetActive(true);
			if (PlayerIcon.CurrentInputDevice != null)
			{
				_inputManager.PlayerInput.SwitchCurrentControlScheme(PlayerIcon.CurrentInputDevice);
			}
			if (SceneSettings.IsTrainingMode)
			{
				OpenMenuHideCurrent(_practiceMenu);
			}
			else
			{
				OpenMenuHideCurrent(_versusMenu);
			}
		}
	}
}
