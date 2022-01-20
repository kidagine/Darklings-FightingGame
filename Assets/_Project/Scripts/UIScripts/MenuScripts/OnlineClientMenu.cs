using Demonics.UI;
using System.Text;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class OnlineClientMenu : BaseMenu
{
	[SerializeField] private TMP_InputField _roomIdInputField = default;
	[SerializeField] private TMP_InputField _playerNameInputField = default;
	[SerializeField] private BaseMenu _onlineHostMenu = default;
	private string _cachedRoomIdText = "";



	void OnEnable()
	{
		//NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnect;
	}

	void OnDisable()
	{
		_cachedRoomIdText = "";
		_roomIdInputField.text = "";
	}

	private void HandleClientConnect(ulong clientId)
	{
		OpenMenuHideCurrent(_onlineHostMenu);
	}

	public void Client()
	{
		string connectionPayload = JsonUtility.ToJson(new ConnectionPayload()
		{
			RoomId = "abc",
			PlayerName = _playerNameInputField.text
		});
		byte[] payloadBytes = Encoding.UTF8.GetBytes(connectionPayload);
		NetworkManager.Singleton.NetworkConfig.ConnectionData = payloadBytes;
		NetworkManager.Singleton.StartClient();
		OpenMenuHideCurrent(_onlineHostMenu);
	}

	public void CheckRoomIdInputField(string field)
	{
		if (_cachedRoomIdText.Length < field.Length)
		{
			if (field.Length == 4 || field.Length == 9)
			{
				_roomIdInputField.text += "-";
				_roomIdInputField.MoveToEndOfLine(false, false);
			}
		}
		_cachedRoomIdText = field;
	}

	public void Paste()
	{
		_roomIdInputField.text = GUIUtility.systemCopyBuffer;
	}
}