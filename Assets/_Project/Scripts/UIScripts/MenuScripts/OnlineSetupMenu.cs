using Demonics.UI;
using Unity.Netcode;
using UnityEngine;

public class OnlineSetupMenu : BaseMenu
{
	[SerializeField] private HostHandler _hostHandler = default;


	public void Host()
	{
		NetworkManager.Singleton.ConnectionApprovalCallback += _hostHandler.ApprovalCheck;
		NetworkManager.Singleton.StartHost();
	}
}
