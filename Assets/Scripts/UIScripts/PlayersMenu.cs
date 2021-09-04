using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersMenu : BaseMenu
{
    [SerializeField] private BaseMenu _otherMenu = default;
    [SerializeField] private RectTransform[] _playerIcons = default;


	void Start()
	{
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Debug.Log("New device added: " + device);
                    break;

                case InputDeviceChange.Removed:
                    Debug.Log("Device removed: " + device);
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
        if (movement > 0.0f)
        {
            if (_playerIcons[index].anchoredPosition.x == -375.0f)
            {
                _playerIcons[index].anchoredPosition = new Vector2(25.0f, _playerIcons[index].anchoredPosition.y);
            }
            else
            {
                _playerIcons[index].anchoredPosition = new Vector2(375.0f, _playerIcons[index].anchoredPosition.y);
            }
        }
        else if (movement < 0.0f)
        {
            _playerIcons[index].anchoredPosition = new Vector2(-375.0f, _playerIcons[index].anchoredPosition.y);
            if (_playerIcons[index].anchoredPosition.x == 375.0f)
            {
                _playerIcons[index].anchoredPosition = new Vector2(25.0f, _playerIcons[index].anchoredPosition.y);
            }
            else
            {
                _playerIcons[index].anchoredPosition = new Vector2(-375.0f, _playerIcons[index].anchoredPosition.y);
            }
        }
    }

    public void OpenOtherMenu()
    {
        if (_playerIcons[0].anchoredPosition.x != 25.0f)
        {
            gameObject.SetActive(false);
            _otherMenu.Show();
        }
    }
}
