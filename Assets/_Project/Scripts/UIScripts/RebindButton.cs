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
	[Range(0, 3)]
	[SerializeField] private int _controlsIndex = default;
	[Range(1, 4)]
	[SerializeField] private int _compositeIndex = default;

	public InputActionReference ActionReference { get { return _actionReference; } private set { } }
	public int ControlBindingIndex { get; private set; }
	public int CompositeIndex { get { return _compositeIndex; } private set { } }


	void OnEnable()
	{
		UpdatePromptImage();
	}

	public void UpdatePromptImage()
	{
		InputAction focusedInputAction = _playerInput.actions.FindAction(ActionReference.action.id);
		ControlBindingIndex = focusedInputAction.GetBindingIndexForControl(focusedInputAction.controls[_controlsIndex]);
		string currentBindingInput = InputControlPath.ToHumanReadableString(focusedInputAction.bindings[ControlBindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
		_image.sprite = _deviceConfigurator.GetDeviceBindingIcon(_playerInput, currentBindingInput);
	}
}
