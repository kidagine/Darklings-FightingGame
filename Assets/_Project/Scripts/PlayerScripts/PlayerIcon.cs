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
    [SerializeField] private TopBarMenu _hotBar = default;
    private RectTransform _rectTransform;
    private Audio _audio;
    private readonly float _left = -375.0f;
    private readonly float _right = 375.0f;
    private GameObject _cpuText;
    private bool _isMovenentInUse;
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
        if (_hotBar.Active)
            return;
        if (gameObject.activeInHierarchy)
        {
            float movement = callbackContext.ReadValue<Vector2>().x;
            if (movement != 0.0f)
            {
                if (!_isMovenentInUse)
                {
                    _isMovenentInUse = true;
                    if (movement > 0.0f)
                    {
                        if (_rectTransform.parent == _playersMenu.PlayerGroups[0])
                        {
                            _audio.Sound("Selected").Play();
                            Center();
                        }
                        else if (!_playersMenu.IsOnRight())
                        {
                            _audio.Sound("Selected").Play();
                            MoveRight();
                        }
                    }
                    else if (movement < 0.0f)
                    {
                        if (_rectTransform.parent == _playersMenu.PlayerGroups[2])
                        {
                            _audio.Sound("Selected").Play();
                            Center();
                        }
                        else if (!_playersMenu.IsOnLeft())
                        {
                            _audio.Sound("Selected").Play();
                            MoveLeft();
                        }
                    }
                }
            }
            if (movement == 0.0f)
                _isMovenentInUse = false;
        }
    }

    public void MoveRight()
    {
        if (_hotBar.Active)
            return;
        transform.GetChild(1).gameObject.SetActive(false);
        _playersMenu.CpuTextRight.SetActive(false);
        _cpuText = _playersMenu.CpuTextRight;
        _rectTransform.SetParent(_playersMenu.PlayerGroups[2]);
    }

    public void MoveLeft()
    {
        if (_hotBar.Active)
            return;
        transform.GetChild(0).gameObject.SetActive(false);
        _playersMenu.CpuTextLeft.SetActive(false);
        _cpuText = _playersMenu.CpuTextLeft;
        _rectTransform.SetParent(_playersMenu.PlayerGroups[0]);
    }

    public void OpenOtherMenu(CallbackContext callbackContext)
    {
        if (_hotBar.Active)
            return;
        if (gameObject.activeInHierarchy)
            if (callbackContext.performed)
                if (_rectTransform.parent != _playersMenu.PlayerGroups[1])
                    _playersMenu.OpenOtherMenu();
    }

    public void ConfirmQuickAssign(CallbackContext callbackContext)
    {
        if (_hotBar.Active)
            return;
        if (gameObject.activeInHierarchy && _rectTransform.parent == _playersMenu.PlayerGroups[1])
            if (callbackContext.performed)
            {
                _playersMenu.CpuTextLeft.SetActive(false);
                _audio.Sound("Selected").Play();
                _rectTransform.SetParent(_playersMenu.PlayerGroups[0]);
            }
    }

    void OnDisable()
    {
        _hotBar.StartCoroutine(ResetPlayerIcon());
    }

    IEnumerator ResetPlayerIcon()
    {
        yield return null;
        _rectTransform.SetParent(_playersMenu.PlayerGroups[1], false);
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
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            _rectTransform.SetParent(_playersMenu.PlayerGroups[1]);
        }
    }
}
