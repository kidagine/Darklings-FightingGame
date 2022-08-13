using Demonics.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RebindMenu : BaseMenu
{
	[SerializeField] private Button _firstCharacterButton = default;
	[SerializeField] private CharacterAssistSelector _characterAssistSelector = default;
	[SerializeField] private CharacterColorSelector _characterColorSelector = default;
	[SerializeField] private GameObject _assignButtonImage = default;		

	public void HideRebind()
	{
		Hide();
		if (!_characterAssistSelector.gameObject.activeSelf && !_characterColorSelector.gameObject.activeSelf)
		{
			_firstCharacterButton.Select();
		}
	}

	public void AssignButton()
	{
		EventSystem.current.sendNavigationEvents = false;
		_assignButtonImage.SetActive(true);
	}

	public void ResetRebindToDefault()
	{

	}
}
