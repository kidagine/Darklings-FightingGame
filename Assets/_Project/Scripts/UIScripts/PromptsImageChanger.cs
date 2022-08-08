using UnityEngine;
using UnityEngine.UI;

public class PromptsImageChanger : MonoBehaviour
{
	[SerializeField] private Image _promptImage = default;
	[SerializeField] private Sprite _promptKeyboardSprite = default;
	[SerializeField] private Sprite _promptControllerSprite = default;
	[SerializeField] private PauseMenu _pauseMenu = default;


	void Awake()
	{
		InputManager.Instance.OnInputChange.AddListener(() => SetCorrectPromptSprite());
	}

	private void SetCorrectPromptSprite()
	{
		string inputScheme = InputManager.Instance.InputScheme;
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
