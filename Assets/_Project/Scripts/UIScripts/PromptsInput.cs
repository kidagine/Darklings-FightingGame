using UnityEngine;
using UnityEngine.Events;

public class PromptsInput : MonoBehaviour
{
    [SerializeField] private UnityEvent _confirmUnityEvent = default;
    [SerializeField] private UnityEvent _backUnityEvent = default;
    [SerializeField] private UnityEvent _mainSpecialUnityEvent = default;


	private void Update()
    {
        if (Input.GetButtonDown("KeyboardOne" + "Confirm") || Input.GetButtonDown("ControllerOne" + "Confirm") || Input.GetButtonDown("ControllerTwo" + "Confirm"))
        {
            _confirmUnityEvent?.Invoke();
        }
        if (Input.GetButtonDown("KeyboardOne" + "Back") || Input.GetButtonDown("ControllerOne" + "Back"))
        {
            _backUnityEvent?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _mainSpecialUnityEvent?.Invoke();
        }
    }
 }
