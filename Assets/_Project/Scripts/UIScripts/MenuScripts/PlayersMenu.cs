using Demonics.Sounds;
using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayersMenu : BaseMenu
{
	[SerializeField] private CharacterMenu _characterMenu = default;
	[SerializeField] private RectTransform[] _playerIcons = default;
	[SerializeField] private GameObject _cpuTextRight = default;
	[SerializeField] private GameObject _cpuTextLeft = default;
	[SerializeField] private BaseMenu _versusMenu = default;
	[SerializeField] private BaseMenu _practiceMenu = default;
	private Audio _audio;
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
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			_playerIcons[i].GetComponent<PlayerIcon>().SetController();
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

	public void OpenOtherMenu()
	{
		if (_playerIcons[0].anchoredPosition.x != _center || _playerIcons[1].anchoredPosition.x != _center || _playerIcons[2].anchoredPosition.x != _center)
		{
			_audio.Sound("Pressed").Play();
			if (_playerIcons[0].anchoredPosition.x == _right)
			{
				SceneSettings.ControllerTwoScheme = _playerIcons[0].GetComponent<PlayerIcon>().PlayerInput.currentControlScheme;
				SceneSettings.ControllerTwo = _playerIcons[0].GetComponent<PlayerIcon>().PlayerInput.devices[0];
			}
			else if (_playerIcons[1].anchoredPosition.x == _right)
			{
				SceneSettings.ControllerTwoScheme = _playerIcons[1].GetComponent<PlayerIcon>().PlayerInput.currentControlScheme;
				SceneSettings.ControllerTwo = _playerIcons[1].GetComponent<PlayerIcon>().PlayerInput.devices[0];
			}
			else if (_playerIcons[2].anchoredPosition.x == _right)
			{
				SceneSettings.ControllerTwoScheme = _playerIcons[2].GetComponent<PlayerIcon>().PlayerInput.currentControlScheme;
				SceneSettings.ControllerTwo = _playerIcons[2].GetComponent<PlayerIcon>().PlayerInput.devices[0];
			}
			else
			{
				SceneSettings.ControllerTwo = null;
			}
			if (_playerIcons[0].anchoredPosition.x == _left)
			{
				SceneSettings.ControllerOneScheme = _playerIcons[0].GetComponent<PlayerIcon>().PlayerInput.currentControlScheme;
				SceneSettings.ControllerOne = _playerIcons[0].GetComponent<PlayerIcon>().PlayerInput.devices[0];
			}
			else if (_playerIcons[1].anchoredPosition.x == _left)
			{
				SceneSettings.ControllerOneScheme = _playerIcons[1].GetComponent<PlayerIcon>().PlayerInput.currentControlScheme;
				SceneSettings.ControllerOne = _playerIcons[1].GetComponent<PlayerIcon>().PlayerInput.devices[0];
			}
			else if (_playerIcons[2].anchoredPosition.x == _left)
			{
				SceneSettings.ControllerOneScheme = _playerIcons[2].GetComponent<PlayerIcon>().PlayerInput.currentControlScheme;
				SceneSettings.ControllerOne = _playerIcons[2].GetComponent<PlayerIcon>().PlayerInput.devices[0];
			}
			else
			{
				SceneSettings.ControllerOne = null;
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
			_characterMenu.Show();
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

	public void OpenKeyboardCoOp()
	{
		_audio.Sound("Pressed").Play();
		SceneSettings.ControllerTwo = _playerIcons[0].GetComponent<PlayerIcon>().PlayerInput.devices[0];
		SceneSettings.ControllerOne = _playerIcons[0].GetComponent<PlayerIcon>().PlayerInput.devices[0];
		gameObject.SetActive(false);
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
		InputSystem.onDeviceChange += UpdateVisiblePlayers;
		UpdateVisiblePlayers(null, default);
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
