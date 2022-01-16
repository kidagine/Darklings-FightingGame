using Demonics.UI;
using System.Text;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class OnlineClientMenu : BaseMenu
{
	[SerializeField] private TMP_InputField _roomIdInputField = default;
	[SerializeField] private BaseMenu _onlineHostMenu = default;
	private string _cachedRoomIdText = "";



	void OnEnable()
	{
		NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnect;
	}

	void OnDisable()
	{
		_cachedRoomIdText = "";
		_roomIdInputField.text = "";
	}

	private void HandleClientConnect(ulong clientId)
	{

	}

	public void Client()
	{
		NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.ASCII.GetBytes(_roomIdInputField.text);
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
