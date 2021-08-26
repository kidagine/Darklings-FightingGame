using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
    [SerializeField] private string _actionName = default;
    [SerializeField] private DeviceConfigurator _deviceConfigurator = default;
    [SerializeField] private Image _image = default;


    void Start()
    {
        InputManager.Instance.ControlsChanged += UpdatePromptImage;
		UpdatePromptImage();
	}

    private void UpdatePromptImage()
    {
		InputAction focusedInputAction = InputManager.Instance.GetPlayerInputAction(_actionName);
		int controlBindingIndex = focusedInputAction.GetBindingIndexForControl(focusedInputAction.controls[0]);
		string currentBindingInput = InputControlPath.ToHumanReadableString(focusedInputAction.bindings[controlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
		_image.sprite = _deviceConfigurator.GetDeviceBindingIcon(InputManager.Instance.GetPlayerInput(), currentBindingInput);
	}
}
