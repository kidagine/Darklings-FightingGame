using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PromptsImageChanger : MonoBehaviour
{
	[SerializeField] private InputManager _inputManager = default;
	[SerializeField] private Image _promptImage = default;
	[SerializeField] private Sprite _promptKeyboardSprite = default;
	[SerializeField] private Sprite _promptControllerSprite = default;
	[SerializeField] private PauseMenu _pauseMenu = default;


	void Awake()
	{
		if (_inputManager != null)
		{
			_inputManager.OnInputChange.AddListener(() => SetCorrectPromptSprite());
		}
	}

	private void SetCorrectPromptSprite()
	{
		if (_pauseMenu == null)
		{
			string inputScheme = _inputManager.InputScheme;
			if (inputScheme == "Keyboard")
			{
				_promptImage.sprite = _promptKeyboardSprite;
			}
			else
			{
				_promptImage.sprite = _promptControllerSprite;
			}
		}
		else
		{
			string inputScheme = _pauseMenu.PauseControllerType;
			if (inputScheme == "Keyboard")
			{
				_promptImage.sprite = _promptKeyboardSprite;
			}
			else
			{
				_promptImage.sprite = _promptControllerSprite;
			}
		}
	}

	void OnEnable()
	{
		SetCorrectPromptSprite();
	}

	void OnDisable()
	{
		if (_inputManager != null)
		{
			_inputManager.OnInputChange.RemoveListener(() => SetCorrectPromptSprite());
		}
	}
}
