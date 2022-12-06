using UnityEngine;

public class MouseSetup : MonoBehaviour
{
    [SerializeField] private CursorLockMode _cursorLockMode = default;
    [SerializeField] private bool _mouseVisible = default;


    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = _cursorLockMode;
        Cursor.visible = _mouseVisible;
    }
}
