using Demonics.UI;
using System.Text;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class OnlineClientMenu : BaseMenu
{
	[SerializeField] private TMP_InputField _roomIdInputField = default;
	private string _cachedRoomIdText = "";


	public void Client()
	{
		NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.ASCII.GetBytes(_roomIdInputField.text); 
		NetworkManager.Singleton.StartClient();
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

	private void OnDisable()
	{
		_roomIdInputField.text = "";
	}
}
