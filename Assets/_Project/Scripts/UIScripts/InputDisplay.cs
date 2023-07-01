using UnityEngine;
using UnityEngine.UI;

public class InputDisplay : MonoBehaviour
{
    [SerializeField] private bool _playerOne = default;
    [SerializeField] private Color _activedInputs = default;
    [SerializeField] private Color _disabledInputs = default;
    [SerializeField] private Color _activedActions = default;
    [SerializeField] private Color _disabledActions = default;
    [Header("Directions")]
    [SerializeField] private Image _up = default;
    [SerializeField] private Image _down = default;
    [SerializeField] private Image _left = default;
    [SerializeField] private Image _right = default;
    [SerializeField] private Image _upLeft = default;
    [SerializeField] private Image _upRight = default;
    [SerializeField] private Image _downLeft = default;
    [SerializeField] private Image _downRight = default;
    [Header("Actions")]
    [SerializeField] private Image _light = default;
    [SerializeField] private Image _medium = default;
    [SerializeField] private Image _heavy = default;
    [SerializeField] private Image _arcana = default;
    [SerializeField] private Image _shadow = default;
    [SerializeField] private Image _throw = default;
    [SerializeField] private Image[] _triggers = default;

    public void UpdateDisplay(InputList inputList)
    {
        for (int i = 0; i < _triggers.Length; i++)
            SetTriggerDisplay(_triggers[i], inputList.inputTriggers[i].held);
    }

    private void SetTriggerDisplay(Image triggerImage, bool triggerHeld)
    {
        triggerImage.color = triggerHeld ? _activedActions : _disabledActions;
    }

    private void SetInputDirection(Vector2Int direction, bool playerOne)
    {
        if (_playerOne != playerOne)
            return;

        _up.color = _disabledInputs;
        _down.color = _disabledInputs;
        _left.color = _disabledInputs;
        _right.color = _disabledInputs;
        _downLeft.color = _disabledInputs;
        _downRight.color = _disabledInputs;
        _upLeft.color = _disabledInputs;
        _upRight.color = _disabledInputs;

        Image inputImage = null;
        if (direction.y == 1)
            inputImage = _up;
        if (direction.y == -1)
            inputImage = _down;
        if (direction.x == -1)
            inputImage = _left;
        if (direction.x == 1)
            inputImage = _right;
        if (direction.y == -1 && direction.x == -1)
            inputImage = _downLeft;
        if (direction.y == -1 && direction.x == 1)
            inputImage = _downRight;
        if (direction.y == 1 && direction.x == -1)
            inputImage = _upLeft;
        if (direction.y == 1 && direction.x == 1)
            inputImage = _upRight;

        if (inputImage != null)
            inputImage.color = _activedInputs;
    }

    private void SetInputAction(InputEnum inputEnum, bool state, bool playerOne)
    {
        if (_playerOne != playerOne)
            return;
        Color color = state ? _activedActions : _disabledActions;
        switch (inputEnum)
        {
            case InputEnum.Light:
                _light.color = color;
                break;
            case InputEnum.Medium:
                _medium.color = color;
                break;
            case InputEnum.Heavy:
                _heavy.color = color;
                break;
            case InputEnum.Special:
                _arcana.color = color;
                break;
            case InputEnum.Assist:
                _shadow.color = color;
                break;
            case InputEnum.Throw:
                _throw.color = color;
                break;
            case InputEnum.Parry:
                _light.color = color;
                _medium.color = color;
                break;
            case InputEnum.RedFrenzy:
                _medium.color = color;
                _heavy.color = color;
                break;
        }
    }
}
