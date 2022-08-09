using Demonics.Sounds;
using Demonics.UI;
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
	private int _index;
	private readonly float _left = -375.0f;
	private readonly float _right = 375.0f;
	private readonly float _center = 0.0f;
	public GameObject CpuTextRight { get { return _cpuTextRight; } private set { } }
	public GameObject CpuTextLeft{ get { return _cpuTextLeft; } private set { } }


	void Awake()
	{
		_audio = GetComponent<Audio>();
	}

	void Start()
	{
		_inputManager.OnDeviceChange.AddListener(UpdateVisiblePlayers);
		UpdateVisiblePlayers();
	}

	private void UpdateVisiblePlayers()
	{
		if (InputSystem.devices.Count > 0)
		{
			if (InputSystem.devices[0].displayName.Contains("Keyboard"))
			{
				_playerIcons[0].gameObject.SetActive(true);
			}
			else
			{
				_playerIcons[0].gameObject.SetActive(false);
			}
		}
		else
		{
			_playerIcons[0].gameObject.SetActive(false);
		}
		if (InputSystem.devices.Count > 2)
		{
			if (InputSystem.devices[2].displayName.Contains("Controller"))
			{
				_playerIcons[1].gameObject.SetActive(true);
			}
			else
			{
				_playerIcons[1].gameObject.SetActive(false);
			}
		}
		else
		{
			_playerIcons[1].gameObject.SetActive(false);
		}
		if (InputSystem.devices.Count > 3)
		{
			if (InputSystem.devices[3].displayName.Contains("Controller"))
			{
				_playerIcons[2].gameObject.SetActive(true);
			}
			else
			{
				_playerIcons[2].gameObject.SetActive(false);
			}
		}
		else
		{
			_playerIcons[2].gameObject.SetActive(false);
		}
	}

	public bool IsOnRight()
	{
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			if (_playerIcons[i].anchoredPosition.x == _right)
			{
				_cpuTextRight.SetActive(false);
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
				_cpuTextLeft.SetActive(false);
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
					SceneSettings.ControllerTwo = "KeyboardOne";
				}
				else if (_playerIcons[1].anchoredPosition.x == _right)
				{
					SceneSettings.ControllerTwo = "ControllerOne";
				}
				else if (_playerIcons[2].anchoredPosition.x == _right)
				{
					SceneSettings.ControllerTwo = "ControllerTwo";
				}
				else
				{
					SceneSettings.ControllerTwo = "Cpu";
				}
				if (_playerIcons[0].anchoredPosition.x == _left)
				{
					SceneSettings.ControllerOne = "KeyboardOne";
				}
				else if (_playerIcons[1].anchoredPosition.x == _left)
				{
					SceneSettings.ControllerOne = "ControllerOne";
				}
				else if (_playerIcons[2].anchoredPosition.x == _left)
				{
					SceneSettings.ControllerOne = "ControllerTwo";
				}
				else
				{
					SceneSettings.ControllerOne = "Cpu";
				}
				if (_playerIcons[0].anchoredPosition.x != _center && _playerIcons[1].anchoredPosition.x != _center
					|| _playerIcons[0].anchoredPosition.x != _center && _playerIcons[2].anchoredPosition.x != _center
					|| _playerIcons[1].anchoredPosition.x != _center && _playerIcons[2].anchoredPosition.x != _center)
				{
					_characterMenu.EnablePlayerTwoSelector();
				}
				for (int i = 0; i < _playerIcons.Length; i++)
				{
					_playerIcons[i].GetComponent<PlayerIcon>().Center();
				}
				_characterMenu.Show();
				gameObject.SetActive(false);
			}
		}
	}

	public void SetCurrentPlayerIcon(int index)
	{
		_index = index;
	}

	public void ConfirmQuickAssign(CallbackContext callbackContext)
	{
		if (callbackContext.performed)
		{
			if (gameObject.activeSelf)
			{
				_audio.Sound("Selected").Play();
				_cpuTextLeft.SetActive(false);
				_playerIcons[_index].anchoredPosition = new Vector2(_left, 275.0f);
			}
		}
	}

	public void OpenKeyboardCoOp()
	{
		_audio.Sound("Pressed").Play();
		SceneSettings.ControllerTwo = "KeyboardTwo";
		SceneSettings.ControllerOne = "KeyboardOne";
		gameObject.SetActive(false);
		_characterMenu.Show();
	}

	void OnDisable()
	{
		_cpuTextLeft.SetActive(true);
		_cpuTextRight.SetActive(true);
	}

	public void Back()
	{
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
