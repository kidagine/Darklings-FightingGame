using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersMenu : BaseMenu
{
    [SerializeField] private BaseMenu _otherMenu = default;
    [SerializeField] private RectTransform _playerOne = default;
    [SerializeField] private RectTransform _playerTwo = default;


    public void Movement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 movement = context.ReadValue<Vector2>();
            if (movement.x > 0.0f)
            {
                if (_playerOne.anchoredPosition.x == -375.0f)
                {
                    _playerOne.anchoredPosition = new Vector2(25.0f, _playerOne.anchoredPosition.y);
                }
                else
                {
                    _playerOne.anchoredPosition = new Vector2(375.0f, _playerOne.anchoredPosition.y);
                }   
            }
            else if (movement.x < 0.0f)
            {
                if (_playerOne.anchoredPosition.x == 375.0f)
                {
                    _playerOne.anchoredPosition = new Vector2(25.0f, _playerOne.anchoredPosition.y);
                }
                else
                {
                    _playerOne.anchoredPosition = new Vector2(-375.0f, _playerOne.anchoredPosition.y);
                }
            }
        }
    }

    public void OpenOtherMenu()
    {
        if (_playerOne.anchoredPosition.x != 25.0f)
        {
            gameObject.SetActive(false);
            _otherMenu.Show();
        }
    }
}
