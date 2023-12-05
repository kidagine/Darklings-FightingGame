using Demonics.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseSetup : Singleton<MouseSetup>
{
    [SerializeField] private CursorLockMode _cursorLockMode = default;
    [SerializeField] private bool _mouseVisible = default;
    [SerializeField] private PlayerInput _playerInput = default;
    [SerializeField] private Texture2D _hoverTexture = default;
    public Texture2D HoverCursor { get { return _hoverTexture; } private set { } }
    public bool LockCamera { get; private set; }

    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = _cursorLockMode;
        Cursor.visible = _mouseVisible;
    }

    public void SetCursor(bool enable)
    {
        _cursorLockMode = enable ? CursorLockMode.None : CursorLockMode.Locked;
        _mouseVisible = enable;
        Cursor.lockState = _cursorLockMode;
        Cursor.visible = _mouseVisible;
    }

    public void SetLock(bool state)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = !state;
        LockCamera = state;
    }

    private void Update()
    {
        if (LockCamera)
            return;
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Cursor.lockState = CursorLockMode.None;
            _mouseVisible = true;
        }
        if (_playerInput != null)
        {
            if (_playerInput.currentControlScheme != null)
                if (!_playerInput.currentControlScheme.Contains("Keyboard"))
                    _mouseVisible = false;
        }
        if (Input.anyKeyDown)
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
                _mouseVisible = false;
        if (Input.GetKeyDown(KeyCode.Space))
            _mouseVisible = true;
        if (Cursor.lockState == CursorLockMode.Locked)
            _mouseVisible = false;
        Cursor.visible = _mouseVisible;

    }
}
