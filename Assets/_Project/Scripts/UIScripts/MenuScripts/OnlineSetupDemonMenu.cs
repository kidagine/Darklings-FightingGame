using Demonics.UI;
using TMPro;
using UnityEngine;

public class OnlineSetupDemonMenu : BaseMenu
{
	[SerializeField] private TMP_InputField _nameInputField = default;
	private OnlinePlayerInfo _onlinePlayerInfo;


	public OnlinePlayerInfo GetOnlinePlayerInfo(ulong clientId)
	{
		_onlinePlayerInfo = new OnlinePlayerInfo(clientId, _nameInputField.text, "waiting", 0);
		return _onlinePlayerInfo;
	}
}
