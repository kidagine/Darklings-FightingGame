using UnityEngine;
using UnityEngine.UI;

public class PromptsImageChanger : MonoBehaviour
{
	[SerializeField] private InputManager _inputManager = default;
	[SerializeField] private Image _promptImage = default;
	[SerializeField] private Sprite _promptKeyboardSprite = default;
	[SerializeField] private Sprite _promptXboxSprite = default;
	[SerializeField] private Sprite _promptControllerSprite = default;
	[SerializeField] private PauseMenu _pauseMenu = default;

	private void SetCorrectPromptSprite()
	{
		if (_inputManager != null)
		{
			string inputScheme = _inputManager.InputScheme;
			if (inputScheme.Contains("Keyboard"))
			{
				_promptImage.sprite = _promptKeyboardSprite;
			}
			else if (inputScheme.Contains("Xbox"))
			{
				_promptImage.sprite = _promptXboxSprite;
			}
			else
			{
				_promptImage.sprite = _promptControllerSprite;
			}
		}
		else if (_pauseMenu != null)
		{
			string inputScheme = _pauseMenu.PauseControllerType;
			if (inputScheme.Contains("Keyboard"))
			{
				_promptImage.sprite = _promptKeyboardSprite;
			}
			else if (inputScheme.Contains("Xbox"))
			{
				_promptImage.sprite = _promptXboxSprite;
			}
			else
			{
				_promptImage.sprite = _promptControllerSprite;
			}
		}
	}

	public void SetPromptSpriteOnCommand(string inputScheme)
	{
		if (inputScheme.Contains("Keyboard"))
		{
			_promptImage.sprite = _promptKeyboardSprite;
		}
		else if (inputScheme.Contains("Xbox"))
		{
			_promptImage.sprite = _promptXboxSprite;
		}
		else
		{
			_promptImage.sprite = _promptControllerSprite;
		}
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
