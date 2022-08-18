using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PromptsImageChanger : MonoBehaviour
{
	[SerializeField] private InputActionReference _actionReference = default;
	[SerializeField] private DeviceConfigurator _deviceConfigurator = default;
	[SerializeField] private InputManager _inputManager = default;
	private PlayerInput _playerInput;
	private Image _image;


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
		_image = transform.GetChild(1).GetComponent<Image>();
	}

	void OnEnable()
	{
		if (_inputManager != null)
		{
			_inputManager.OnInputChange.AddListener(SetCorrectPromptSprite);
		}
		SetCorrectPromptSprite();
	}

	void OnDisable()
	{
		if (_inputManager != null)
		{
			_inputManager.OnInputChange.RemoveListener(SetCorrectPromptSprite);
		}
	}
}
