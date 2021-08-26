using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PromptsInput : MonoBehaviour
{
    [SerializeField] private PlayerInputActions _promptsInputActions = default;
    [SerializeField] private UnityEvent _confirmUnityEvent = default;
    [SerializeField] private UnityEvent _backUnityEvent = default;
    [SerializeField] private UnityEvent _mainSpecialUnityEvent = default;
    [SerializeField] private UnityEvent _secondarySpecialUnityEvent = default;
    [SerializeField] private UnityEvent _rightPageUnityEvent = default;
    [SerializeField] private UnityEvent _leftPageUnityEvent = default;


    void Awake()
    {
        PromptInputSetup();
    }

	private void PromptInputSetup()
    {
        _promptsInputActions = new PlayerInputActions();
        _promptsInputActions.Prompts.Confirm.performed += Confirm;
        _promptsInputActions.Prompts.Back.performed += Back;
    }

    private void Confirm(InputAction.CallbackContext context)
    {
        _confirmUnityEvent?.Invoke();
    }

    public void Confirm()
    {
        _confirmUnityEvent?.Invoke();
    }

	private void Back(InputAction.CallbackContext context)
	{
		_backUnityEvent?.Invoke();
	}

    public void Back()
    {
        _backUnityEvent?.Invoke();
    }

    private void MainSpecial(InputAction.CallbackContext context)
    {
        _mainSpecialUnityEvent?.Invoke();
    }

    public void MainSpecial()
    {
        _mainSpecialUnityEvent?.Invoke();
    }

    private void RightPage(InputAction.CallbackContext context)
    {
        _rightPageUnityEvent?.Invoke();
    }

    private void LeftPage(InputAction.CallbackContext context)
    {
        _leftPageUnityEvent?.Invoke();
    }

    private void SecondarySpecial(InputAction.CallbackContext context)
    {
        _secondarySpecialUnityEvent?.Invoke();
    }

    public void SecondarySpecial()
    {
        _secondarySpecialUnityEvent?.Invoke();
    }

    void OnEnable()
    {
        _promptsInputActions.Enable();
    }

    void OnDisable()
    {
        _promptsInputActions.Disable();
    }
}
