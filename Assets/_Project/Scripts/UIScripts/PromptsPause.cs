using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PromptsPause : MonoBehaviour
{
	[SerializeField] private InputActionReference _actionReference = default;
	[SerializeField] private DeviceConfigurator _deviceConfigurator = default;
	[SerializeField] private PauseMenu _pauseMenu = default;
	private Image _image;


	void Awake()
	{
		if (transform.childCount > 0)
		{
			_image = transform.GetChild(1).GetComponent<Image>();
		}
		else
		{
			_image = GetComponent<Image>();
		}
	}

	void OnEnable()
	{
		SetCorrectPromptSprite();
	}

	private void SetCorrectPromptSprite()
	{
		int index = 0;
		if (_pauseMenu.PlayerInput.currentControlScheme == ControllerTypeEnum.Keyboard.ToString())
		{
			index = 1;
		}
		InputAction inputAction = _actionReference.action;
		string currentBindingInput = InputControlPath.ToHumanReadableString(inputAction.bindings[index].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
		_image.sprite = _deviceConfigurator.GetDeviceBindingIcon(_pauseMenu.PlayerInput, currentBindingInput);
	}
}
