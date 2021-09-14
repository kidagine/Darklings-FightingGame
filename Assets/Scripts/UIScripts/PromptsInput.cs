using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PromptsInput : MonoBehaviour
{
    [SerializeField] private PlayerInputActions _promptsInputActions = default;
    [SerializeField] private UnityEvent _confirmUnityEvent = default;
    [SerializeField] private UnityEvent _backUnityEvent = default;
    [SerializeField] private UnityEvent _mainSpecialUnityEvent = default;


	private void Update()
    {
        if (Input.GetButtonDown("Keyboard" + "Confirm"))
        {
            _confirmUnityEvent?.Invoke();
        }
        if (Input.GetButtonDown("Keyboard" + "Back"))
        {
            _backUnityEvent?.Invoke();
        }
    }
}
