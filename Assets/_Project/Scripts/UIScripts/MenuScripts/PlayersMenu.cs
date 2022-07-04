using Demonics;
using Demonics.Sounds;
using Demonics.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersMenu : BaseMenu
{
	[SerializeField] private CharacterMenu _characterMenu = default;
	[SerializeField] private RectTransform[] _playerIcons = default;
	[SerializeField] private GameObject _cpuTextRight = default;
	[SerializeField] private GameObject _cpuTextLeft = default;
	[SerializeField] private BaseMenu _versusMenu = default;
	[SerializeField] private BaseMenu _practiceMenu = default;
	private Audio _audio;
	private bool _isMovenentInUse;


	void Awake()
	{
		_audio = GetComponent<Audio>();
	}

	void Start()
	{
		InputSystem.onDeviceChange +=
		(device, change) =>
		{
			switch (change)
			{
				case InputDeviceChange.Added:
					if (device.name == "XInputControllerWindows")
					{
						_playerIcons[1].gameObject.SetActive(true);
					}
					else if (device.name == "XInputControllerWindows1")
					{
						_playerIcons[2].gameObject.SetActive(true);
					}
					break;

				case InputDeviceChange.Removed:
					if (device.name == "XInputControllerWindows")
					{
						_playerIcons[1].anchoredPosition = new Vector2(25.0f, _playerIcons[1].anchoredPosition.y);
						_playerIcons[1].gameObject.SetActive(false);
					}
					else if (device.name == "XInputControllerWindows1")
					{
						_playerIcons[2].anchoredPosition = new Vector2(25.0f, _playerIcons[2].anchoredPosition.y);
						_playerIcons[2].gameObject.SetActive(false);
					}
					break;
			}
		};
	}

	void Update()
	{
		Movement("KeyboardOne", 0);
		Movement("KeyboardTwo", 0);
		Movement("ControllerOne", 1);
		Movement("ControllerTwo", 2);
	}

	private void Movement(string inputName, int index)
	{
		float movement = Input.GetAxisRaw(inputName + "Horizontal");
		if (movement != 0.0f)
		{
			if (!_isMovenentInUse)
			{
				if (movement > 0.0f)
				{
					if (_playerIcons[index].anchoredPosition.x == -375.0f)
					{
						_audio.Sound("Selected").Play();
						_cpuTextLeft.SetActive(true);
						_playerIcons[index].anchoredPosition = new Vector2(25.0f, _playerIcons[index].anchoredPosition.y);
					}
					else if (!IsOnRight())
					{
						_audio.Sound("Selected").Play();
						_playerIcons[index].anchoredPosition = new Vector2(375.0f, _playerIcons[index].anchoredPosition.y);
					}
				}
				else if (movement < 0.0f)
				{
					if (_playerIcons[index].anchoredPosition.x == 375.0f)
					{
						_audio.Sound("Selected").Play();
						_cpuTextRight.SetActive(true);
						_playerIcons[index].anchoredPosition = new Vector2(25.0f, _playerIcons[index].anchoredPosition.y);
					}
					else if (!IsOnLeft())
					{
						_audio.Sound("Selected").Play();
						_playerIcons[index].anchoredPosition = new Vector2(-375.0f, _playerIcons[index].anchoredPosition.y);
					}
				}
				_isMovenentInUse = true;
			}
		}
		else if (movement == 0.0f)
		{
			_isMovenentInUse = false;
		}
	}

	private bool IsOnRight()
	{
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			if (_playerIcons[i].anchoredPosition.x == 375.0f)
			{
				_cpuTextRight.SetActive(false);
				return true;
			}
		}
		return false;
	}

	private bool IsOnLeft()
	{
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			if (_playerIcons[i].anchoredPosition.x == -375.0f)
			{
				_cpuTextLeft.SetActive(false);
				return true;
			}
		}
		return false;
	}

	public void OpenOtherMenu()
	{
		if (_playerIcons[0].anchoredPosition.x != 25.0f || _playerIcons[1].anchoredPosition.x != 25.0f || _playerIcons[2].anchoredPosition.x != 25.0f)
		{
			_audio.Sound("Pressed").Play();
			if (_playerIcons[0].anchoredPosition.x == 375.0f)
			{
				SceneSettings.ControllerTwo = "KeyboardOne";
			}
			else if (_playerIcons[1].anchoredPosition.x == 375.0f)
			{
				SceneSettings.ControllerTwo = "ControllerOne";
			}
			else if (_playerIcons[2].anchoredPosition.x == 375.0f)
			{
				SceneSettings.ControllerTwo = "ControllerTwo";
			}
			else
			{
				Debug.Log("a");
				SceneSettings.ControllerTwo = "Cpu";
			}
			if (_playerIcons[0].anchoredPosition.x == -375.0f)
			{
				SceneSettings.ControllerOne = "KeyboardOne";
			}
			else if (_playerIcons[1].anchoredPosition.x == -375.0f)
			{
				SceneSettings.ControllerOne = "ControllerOne";
			}
			else if (_playerIcons[2].anchoredPosition.x == -375.0f)
			{
				SceneSettings.ControllerOne = "ControllerTwo";
			}
			else
			{
				SceneSettings.ControllerOne = "Cpu";
			}
			if (_playerIcons[0].anchoredPosition.x != 25.0f && _playerIcons[1].anchoredPosition.x != 25.0f 
				|| _playerIcons[0].anchoredPosition.x != 25.0f && _playerIcons[2].anchoredPosition.x != 25.0f
				|| _playerIcons[1].anchoredPosition.x != 25.0f && _playerIcons[2].anchoredPosition.x != 25.0f)
			{
				_characterMenu.EnablePlayerTwoSelector();
			}
			gameObject.SetActive(false);
			_characterMenu.Show();
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
		for (int i = 0; i < _playerIcons.Length; i++)
		{
			_playerIcons[i].anchoredPosition = new Vector2(25.0f, _playerIcons[i].anchoredPosition.y);
		}
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
