using Demonics.UI;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnlineStartMenu : BaseMenu
{
	[SerializeField] private TextMeshProUGUI _roomID = default;
	[SerializeField] private TextMeshProUGUI _playerReadyText = default;
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
		NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnect;
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
		bool approveConnection = roomId == _roomID.text;
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
		_playerReadyText.text = "Ready";
		_readyButton.gameObject.SetActive(false);
		_cancelButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);
	}

	public void Cancel()
	{
		_playerReadyText.text = "Waiting";
		_cancelButton.gameObject.SetActive(false);
		_readyButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_readyButton.gameObject);
	}
}
