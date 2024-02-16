using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerIcon : MonoBehaviour
{
    [SerializeField] private PlayersMenu _playersMenu = default;
    [SerializeField] private PlayerInput _playerInput = default;
    [SerializeField] private TextMeshProUGUI _controllerText = default;
    [SerializeField] private TextMeshProUGUI _numberText = default;
    [SerializeField] private GameObject _leftArrow = default;
    [SerializeField] private GameObject _rightArrow = default;
    [SerializeField] private Color[] _colors = default;
    private RectTransform _rectTransform;
    private Audio _audio;
    private GameObject _cpuText;
    private bool _isMovenentInUse;
    private int _number;
    private static int LastNumber;
    public PlayerInput PlayerInput { get { return _playerInput; } private set { } }


    private void Awake()
    {
        _audio = GetComponent<Audio>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetController()
    {
        gameObject.SetActive(true);
        if (_playerInput.devices.Count > 0)
        {
            if (_number == 0)
            {
                LastNumber++;
                _number = LastNumber;
                _numberText.text = _number.ToString();
                _numberText.color = _colors[_number - 1];
            }

            if (_playerInput.devices[0].displayName == "Keyboard")
                _controllerText.text = "Keyboard";
            else
                _controllerText.text = "Controller";
        }
        else
            gameObject.SetActive(false);
    }

    public void Movement(CallbackContext callbackContext)
    {
        if (gameObject.activeInHierarchy)
        {
            float movement = callbackContext.ReadValue<Vector2>().x;
            if (movement != 0.0f)
                if (!_isMovenentInUse)
                {
                    _isMovenentInUse = true;
                    if (movement > 0.0f)
                    {
                        _audio.Sound("Selected").Play();
                        if (_rectTransform.parent == _playersMenu.PlayerGroups[0])
                            Center();
                        else
                            MoveRight();
                    }
                    else if (movement < 0.0f)
                    {
                        _audio.Sound("Selected").Play();
                        if (_rectTransform.parent == _playersMenu.PlayerGroups[2])
                            Center();
                        else
                            MoveLeft();
                    }
                }
            if (movement == 0.0f)
                _isMovenentInUse = false;
        }
    }

    public void MoveRight()
    {
        if (_playersMenu.PlayerGroups[2].childCount > 0)
            return;
        _rightArrow.SetActive(false);
        _playersMenu.CpuTextRight.SetActive(false);
        _cpuText = _playersMenu.CpuTextRight;
        _rectTransform.SetParent(_playersMenu.PlayerGroups[2]);
    }

    public void MoveLeft()
    {
        if (_playersMenu.PlayerGroups[0].childCount > 0)
            return;
        _leftArrow.gameObject.SetActive(false);
        _playersMenu.CpuTextLeft.SetActive(false);
        _cpuText = _playersMenu.CpuTextLeft;
        _rectTransform.SetParent(_playersMenu.PlayerGroups[0]);
    }

    public void ConfirmQuickAssign()
    {
        _playersMenu.CpuTextLeft.SetActive(false);
        _audio.Sound("Selected").Play();
        _rectTransform.SetParent(_playersMenu.PlayerGroups[0]);
        _cpuText = _playersMenu.CpuTextLeft;
    }

    public void Center()
    {
        if (gameObject.activeSelf)
        {
            if (_cpuText != null)
            {
                _cpuText.SetActive(true);
                _cpuText = null;
            }
            _leftArrow.SetActive(true);
            _rightArrow.SetActive(true);
            _rectTransform.SetParent(_playersMenu.PlayerGroups[1]);
            _rectTransform.SetSiblingIndex(_number - 1);
        }
    }

    public void CheckCPU()
    {
        if (_cpuText != null)
        {
            _cpuText.SetActive(true);
            _cpuText = null;
        }
    }

    private void OnDisable()
    {
        LastNumber = 0;
    }

    private void OnEnable()
    {
        Center();
    }
}