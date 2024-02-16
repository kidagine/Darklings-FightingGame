using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PromptsPause : MonoBehaviour
{
    [SerializeField] private InputActionReference _actionReference = default;
    [SerializeField] private DeviceConfigurator _deviceConfigurator = default;
    private Image _image;


    void Awake()
    {
        if (transform.childCount > 0)
            _image = transform.GetChild(0).GetComponent<Image>();
        else
            _image = GetComponent<Image>();
    }

    void OnEnable()
    {
        SetCorrectPromptSprite();
    }

    private void SetCorrectPromptSprite()
    {
        int index = 0;
        if (GameplayManager.Instance.PauseMenu.PlayerInput.currentControlScheme == ControllerTypeEnum.Keyboard.ToString())
            index = 1;
        InputAction inputAction = _actionReference.action;
        string currentBindingInput = InputControlPath.ToHumanReadableString(inputAction.bindings[index].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _image.sprite = _deviceConfigurator.GetDeviceBindingIcon(GameplayManager.Instance.PauseMenu.PlayerInput, currentBindingInput);
    }
}
