using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : BaseMenu
{
	[SerializeField] private Image _characterOneImage = default;
	[SerializeField] private Image _characterTwoImage = default;


	public void SetCharacterOneImage(Sprite sprite)
	{
		_characterOneImage.enabled = true;
		_characterOneImage.sprite = sprite;
	}

	public void SetCharacterTwoImage(Sprite sprite)
	{
		_characterTwoImage.enabled = true;
		_characterTwoImage.sprite = sprite;
	}
}
