using Demonics.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindButton : BaseButton
{
	[SerializeField] private InputActionReference _actionReference = default;
	[SerializeField] private DeviceConfigurator _deviceConfigurator = default;
	[SerializeField] private Image _image = default;
	[SerializeField] private PlayerInput _playerInput = default;
	public InputActionReference ActionReference { get { return _actionReference; } set { } }


	void OnEnable()
	{
		UpdatePromptImage();
	}

	public void UpdatePromptImage()
	{
		InputAction focusedInputAction = _playerInput.actions.FindAction(ActionReference.action.id);
		int controlBindingIndex = focusedInputAction.GetBindingIndexForControl(focusedInputAction.controls[0]);
		string currentBindingInput = InputControlPath.ToHumanReadableString(focusedInputAction.bindings[controlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
		_image.sprite = _deviceConfigurator.GetDeviceBindingIcon(_playerInput, currentBindingInput);
	}
}
