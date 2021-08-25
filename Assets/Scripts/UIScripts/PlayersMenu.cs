using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersMenu : BaseMenu
{
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
                    _playerOne.anchoredPosition = new Vector2(25.0f, 0.0f);
                }
                else
                {
                    _playerOne.anchoredPosition = new Vector2(375.0f, 0.0f);
                }
            }
            else if (movement.x < 0.0f)
            {
                if (_playerOne.anchoredPosition.x == 375.0f)
                {
                    _playerOne.anchoredPosition = new Vector2(25.0f, 0.0f);
                }
                else
                {
                    _playerOne.anchoredPosition = new Vector2(-375.0f, 0.0f);
                }
            }
        }
    }
}
