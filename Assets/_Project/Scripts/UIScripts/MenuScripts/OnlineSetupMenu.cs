using Demonics.UI;
using System.Text;
using Unity.Netcode;
using UnityEngine;

public class OnlineSetupMenu : BaseMenu
{
	[SerializeField] private HostHandler _hostHandler = default;


	public void Host()
	{
		NetPortalManager.Instance.AddPlayerData("Lol");
		NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
		NetworkManager.Singleton.StartHost();
	}

	private void ApprovalCheck(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
	{
		string payload = Encoding.ASCII.GetString(connectionData);
		var connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload);
		Debug.Log("ADSA");
		if (connectionPayload != null)
		{
			Debug.Log("1");
			Debug.Log("A" + connectionPayload.PlayerName);
			bool approveConnection = connectionPayload.RoomId == "abc";
			NetPortalManager.Instance.AddPlayerData(clientId, connectionPayload.PlayerName);
			callback(true, null, approveConnection, null, null);

		}
		else
		{
			Debug.Log("2");
			callback(true, null, true, null, null);
		}
	}
}
