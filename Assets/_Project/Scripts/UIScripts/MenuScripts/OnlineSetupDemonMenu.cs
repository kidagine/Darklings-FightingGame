using Demonics.UI;
using TMPro;
using UnityEngine;

public class OnlineSetupDemonMenu : BaseMenu
{
	[SerializeField] private TMP_InputField _nameInputField = default;
	private OnlinePlayerInfo _onlinePlayerInfo;


	public void SetPlayerName(string text)
	{
		PlayerPrefs.SetString("playerName", text);
	}
}
