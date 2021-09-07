using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersMenu : BaseMenu
{
    [SerializeField] private BaseMenu _otherMenu = default;
    [SerializeField] private RectTransform[] _playerIcons = default;
    [SerializeField] private bool _onePlayerOnly = default;
    private string _inputOne;
    private string _inputTwo;
    private bool _isMovenentInUse;


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
        Movement("Keyboard", 0);
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
                        _playerIcons[index].anchoredPosition = new Vector2(25.0f, _playerIcons[index].anchoredPosition.y);
                    }
                    else if (_onePlayerOnly)
                    {
                        if (!IsOnLeft() && !IsOnRight())
                        {
                            _playerIcons[index].anchoredPosition = new Vector2(375.0f, _playerIcons[index].anchoredPosition.y);
                            _playerIcons[index].GetChild(0).gameObject.SetActive(false);
                        }
                    }
                    else if (!IsOnRight())
                    {
                        _playerIcons[index].anchoredPosition = new Vector2(375.0f, _playerIcons[index].anchoredPosition.y);
                        _playerIcons[index].GetChild(1).gameObject.SetActive(false);
                    }
                }
                else if (movement < 0.0f)
                {
                    if (_playerIcons[index].anchoredPosition.x == 375.0f)
                    {
                        _playerIcons[index].anchoredPosition = new Vector2(25.0f, _playerIcons[index].anchoredPosition.y);
                    }
                    else if (_onePlayerOnly)
                    {
                        if (!IsOnLeft() && !IsOnRight())
                        {
                            _playerIcons[index].anchoredPosition = new Vector2(-375.0f, _playerIcons[index].anchoredPosition.y);
                            _playerIcons[index].GetChild(0).gameObject.SetActive(false);
                        }
                    }
                    else if (!IsOnLeft())
                    {
                        _playerIcons[index].anchoredPosition = new Vector2(-375.0f, _playerIcons[index].anchoredPosition.y);
                        _playerIcons[index].GetChild(0).gameObject.SetActive(false);
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
                return true;
            }
        }
        return false;
    }

    public void OpenOtherMenu()
    {
        if (_playerIcons[0].anchoredPosition.x != 25.0f || _playerIcons[1].anchoredPosition.x != 25.0f || _playerIcons[2].anchoredPosition.x != 25.0f)
        {
            if (_playerIcons[0].anchoredPosition.x == 375.0f)
            {
                SceneSettings.ControllerOne = "Keyboard";
            }
            else if (_playerIcons[1].anchoredPosition.x == 375.0f)
            {
                SceneSettings.ControllerOne = "ControllerOne";
            }
            else if (_playerIcons[2].anchoredPosition.x == 375.0f)
            {
                SceneSettings.ControllerOne = "ControllerTwo";
            }
            if (_playerIcons[0].anchoredPosition.x == -375.0f)
            {
                SceneSettings.ControllerTwo = "Keyboard";
            }
            else if (_playerIcons[1].anchoredPosition.x == -375.0f)
            {
                SceneSettings.ControllerTwo = "ControllerOne";
            }
            else if (_playerIcons[2].anchoredPosition.x == -375.0f)
            {
                SceneSettings.ControllerTwo = "ControllerTwo";
            }
            gameObject.SetActive(false);
            _otherMenu.Show();
        }
    }

    public void OpenKeyboardCoOp()
    {
        SceneSettings.ControllerOne = "KeyboardTwo";
        SceneSettings.ControllerTwo = "KeyboardOne";
        gameObject.SetActive(false);
        _otherMenu.Show();
    }

	void OnDisable()
	{
		for (int i = 0; i < _playerIcons.Length; i++)
		{
            _playerIcons[i].anchoredPosition = new Vector2(25.0f, _playerIcons[i].anchoredPosition.y);
        }
    }
}
