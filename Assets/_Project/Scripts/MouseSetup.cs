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

    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = _cursorLockMode;
        Cursor.visible = _mouseVisible;
    }

    private void Update()
    {
        if (_playerInput == null)
            return;

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            _mouseVisible = true;
        if (!_playerInput.currentControlScheme.Contains("Keyboard"))
            _mouseVisible = false;
        if (Input.anyKeyDown)
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
                _mouseVisible = false;
        Cursor.visible = _mouseVisible;
    }
}
