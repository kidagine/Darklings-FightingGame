using Demonics.UI;
using TMPro;
using UnityEngine;

public class OnlineSetupDemonMenu : BaseMenu
{
	public void SetPlayerName(string text)
	{
		PlayerPrefs.SetString("playerName", text);
	}
}
