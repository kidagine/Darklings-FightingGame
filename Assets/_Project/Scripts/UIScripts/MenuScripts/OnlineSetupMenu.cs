using Demonics.UI;
using System.Text;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class OnlineSetupMenu : BaseMenu
{
	[SerializeField] private HostHandler _hostHandler = default;
	[SerializeField] private TMP_InputField _playerNameInputField = default;
	[SerializeField] private BaseSelector _characterSelector = default;
	[SerializeField] private BaseSelector _assistSelector = default;
	[SerializeField] private BaseSelector _colorSelector = default;

	public void Host()
	{
		PlayerData playerData = new PlayerData(_playerNameInputField.text, _characterSelector.Value, _assistSelector.Value, _colorSelector.Value);
		NetPortalManager.Instance.AddPlayerData(playerData);
		NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
		NetworkManager.Singleton.StartHost();
	}

	public void ApprovalCheck(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
	{
		string payload = Encoding.ASCII.GetString(connectionData);
		var connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload);
		if (connectionPayload != null)
		{
			bool approveConnection = connectionPayload.RoomId == _hostHandler.RoomId.Value;
			//bool approveConnection = connectionPayload.RoomId == "abc";
			Debug.Log(connectionPayload.RoomId);
			Debug.Log(_hostHandler.RoomId.Value);
			Debug.Log(approveConnection);
			if (approveConnection)
			{
				PlayerData playerData = new PlayerData(connectionPayload.PlayerName, connectionPayload.Character, connectionPayload.Assist, connectionPayload.Color);
				NetPortalManager.Instance.AddPlayerData(clientId, playerData);
				callback(true, null, approveConnection, null, null);
			}
		}
		else
		{
			callback(true, null, true, null, null);
		}
	}
}
