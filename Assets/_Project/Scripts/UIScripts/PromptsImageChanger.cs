using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PromptsImageChanger : MonoBehaviour
{
    [SerializeField] private InputActionReference _actionReference = default;
    [SerializeField] private DeviceConfigurator _deviceConfigurator = default;
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private Image _image = default;
    private PlayerInput _playerInput;


    private void SetCorrectPromptSprite()
    {
        InputAction inputAction = _actionReference.action;
        int controlBindingIndex = inputAction.GetBindingIndexForControl(inputAction.controls[0]);
        string currentBindingInput = InputControlPath.ToHumanReadableString(inputAction.bindings[controlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _image.sprite = _deviceConfigurator.GetDeviceBindingIcon(_playerInput, currentBindingInput);
    }

    void Awake()
    {
        _playerInput = _inputManager.GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        _inputManager?.OnInputChange.AddListener(SetCorrectPromptSprite);
        SetCorrectPromptSprite();
    }

    void OnDisable() => _inputManager?.OnInputChange.RemoveListener(SetCorrectPromptSprite);
}
