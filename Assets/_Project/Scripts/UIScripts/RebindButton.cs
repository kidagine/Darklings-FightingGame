using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindButton : BaseButton
{
    [SerializeField] private InputActionReference _actionReference = default;
    [SerializeField] private DeviceConfigurator _deviceConfigurator = default;
    [SerializeField] private Image _image = default;
    [SerializeField] private PlayerInput _playerInput = default;
    [Range(-1, 4)]
    [SerializeField] private int _compositeIndex = -1;

    public InputActionReference ActionReference { get { return _actionReference; } private set { } }
    public int ControlBindingIndex { get; private set; }
    public int CompositeIndex { get { return _compositeIndex; } private set { } }


    void OnEnable()
    {
        if (_playerInput.devices[0].displayName.Contains("Keyboard"))
        {
            if (CompositeIndex == 0)
            {
                ControlBindingIndex = 1;
            }
            else
            {
                ControlBindingIndex = CompositeIndex + 5;
            }
        }
        else
        {
            if (CompositeIndex == 0)
            {
                ControlBindingIndex = 0;
            }
            else
            {
                ControlBindingIndex = CompositeIndex;
            }
        }
        UpdatePromptImage();
    }

    public void UpdatePromptImage()
    {
        InputAction inputAction = _actionReference.action;
        string currentBindingInput = InputControlPath.ToHumanReadableString(inputAction.bindings[ControlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _image.sprite = _deviceConfigurator.GetDeviceBindingIcon(_playerInput, currentBindingInput);
    }
}
