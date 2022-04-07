using UnityEngine;
using UnityEngine.UI;

public class PromptsImageChanger : MonoBehaviour
{
	[SerializeField] private Image _promptImage = default;
	[SerializeField] private Sprite _promptKeyboardSprite = default;
	[SerializeField] private Sprite _promptControllerSprite = default;
	[SerializeField] private PauseMenu _pauseMenu = default;


	private void SetCorrectPromptSprite(string controller)
	{
		if (controller == ControllerTypeEnum.KeyboardOne.ToString() || controller == ControllerTypeEnum.KeyboardTwo.ToString())
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
		SetCorrectPromptSprite(_pauseMenu.PauseControllerType);
	}
}
