using Demonics.UI;
using System.Text;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class OnlineSetupMenu : BaseMenu
{
	[SerializeField] private HostHandler _hostHandler = default;
	[SerializeField] private TMP_InputField _playerNameInputField = default;

	public void Host()
	{
		NetPortalManager.Instance.AddPlayerData(_playerNameInputField.text);
		NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
		NetworkManager.Singleton.StartHost();
	}

	private void ApprovalCheck(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
	{
		string payload = Encoding.ASCII.GetString(connectionData);
		var connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload);
		if (connectionPayload != null)
		{
			bool approveConnection = connectionPayload.RoomId == "abc";
			NetPortalManager.Instance.AddPlayerData(clientId, connectionPayload.PlayerName);
			callback(true, null, approveConnection, null, null);

		}
		else
		{
			callback(true, null, true, null, null);
		}
	}
}
