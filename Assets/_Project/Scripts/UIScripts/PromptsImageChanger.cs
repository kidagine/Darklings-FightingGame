using UnityEngine;
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
		_inputManager.OnInputChange.AddListener(() => SetCorrectPromptSprite());
	}

	private void SetCorrectPromptSprite()
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

	void OnEnable()
	{
		SetCorrectPromptSprite();
	}
}
