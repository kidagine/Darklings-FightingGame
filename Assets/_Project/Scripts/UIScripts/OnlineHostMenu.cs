using Demonics.UI;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnlineHostMenu : BaseMenu
{
	[SerializeField] private TextMeshProUGUI _roomID = default;
	[SerializeField] private TextMeshProUGUI _playerOneReadyText = default;
	[SerializeField] private TextMeshProUGUI _playerTwoReadyText = default;
	[SerializeField] private BaseButton _readyButton = default;
	[SerializeField] private BaseButton _cancelButton = default;
	[SerializeField] private GameObject _test = default;
	private readonly string _glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";


	void OnEnable()
	{
		Host();
		_roomID.text = $"Room ID: {GenerateRoomID()}";
		NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnect;
		NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
	}

	void OnDisable()
	{
		_cancelButton.gameObject.SetActive(false);
		_readyButton.gameObject.SetActive(true);
		NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnect;
		NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
	}

	private void HandleClientConnect(ulong clientId)
	{
		_test.gameObject.SetActive(true);
		if (clientId == NetworkManager.Singleton.LocalClientId)
		{
			_roomID.text = $"Room ID: {Encoding.ASCII.GetString(NetworkManager.Singleton.NetworkConfig.ConnectionData)}";
		}
	}

	public void HandleClientDisconnect(ulong clientId)
	{
		_test.gameObject.SetActive(false);
	}


	private void Host()
	{
		NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
		NetworkManager.Singleton.StartHost();
	}

	private void ApprovalCheck(byte[] connnectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
	{
		string roomId = Encoding.ASCII.GetString(connnectionData);
		Debug.Log(roomId);
		Debug.Log(_roomID.text);
		bool approveConnection = roomId == "abc";
		callback(true, null, approveConnection, null, null);
	}

	public string GenerateRoomID()
	{
		string roomID = "";
		for (int i = 0; i < 12; i++)
		{
			roomID += _glyphs[Random.Range(0, _glyphs.Length)];
		}
		roomID = Regex.Replace(roomID.ToUpper(), ".{4}", "$0-");
		return roomID.Remove(roomID.Length - 1);
	}

	public void Ready()
	{
		if (NetworkManager.Singleton.IsHost)
		{
			_playerOneReadyText.text = "Ready";
		}
		else
		{
			_playerTwoReadyText.text = "Ready";
		}
		_readyButton.gameObject.SetActive(false);
		_cancelButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);
		ReadyServerRpc();
	}

	[ServerRpc]
	private void ReadyServerRpc()
	{
		if (NetworkManager.Singleton.IsHost)
		{
			_playerOneReadyText.text = "Ready";
		}
		else
		{
			_playerTwoReadyText.text = "Ready";
		}
		ReadyClientRpc();
	}

	[ClientRpc]
	private void ReadyClientRpc()
	{
		if (NetworkManager.Singleton.IsHost)
		{
			_playerOneReadyText.text = "Ready";
		}
		else
		{
			_playerTwoReadyText.text = "Ready";
		}
	}

	public void Cancel()
	{
		if (NetworkManager.Singleton.IsHost)
		{
			_playerOneReadyText.text = "Waiting";
		}
		else
		{
			_playerTwoReadyText.text = "Waiting";
		}
		_cancelButton.gameObject.SetActive(false);
		_readyButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_readyButton.gameObject);
	}

	public void Leave()
	{
		//NetworkManager.Singleton.Shutdown();
		//if (NetworkManager.Singleton.IsHost)
		//{

		//}
		//else if (NetworkManager.Singleton.IsClient)
		//{
		//	Host();
		//	NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnect;
		//	NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
		//}
	}

	public void CopyRoomId()
	{
		GUIUtility.systemCopyBuffer = _roomID.text.Substring(9);
	}
}
