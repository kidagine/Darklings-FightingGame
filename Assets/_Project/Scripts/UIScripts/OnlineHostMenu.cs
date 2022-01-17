using Demonics.UI;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnlineHostMenu : BaseMenu
{
	[SerializeField] private TextMeshProUGUI _roomID = default;
	[SerializeField] private PlayerNameplate[] _playerNameplates = default;
	[SerializeField] private BaseButton _readyButton = default;
	[SerializeField] private BaseButton _cancelButton = default;
	[SerializeField] private GameObject _playerOneNamePlate = default;
	[SerializeField] private GameObject _playerTwoNamePlate = default;
	private readonly string _glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
	private readonly string _ready = "Ready";
	private readonly string _waiting = "Waiting";

	private NetworkList<OnlinePlayerInfo> _onlinePlayersInfo;

	private void Awake()
	{
		_onlinePlayersInfo = new NetworkList<OnlinePlayerInfo>();
	}

	private void HandlePlayersStateChanged(NetworkListEvent<OnlinePlayerInfo> onlinePlayerState)
	{
		Debug.Log(_onlinePlayersInfo.Count);
		_playerNameplates[0].SetData(_onlinePlayersInfo[0]);
		if (_onlinePlayersInfo.Count > 1)
		{
			_playerNameplates[1].SetData(_onlinePlayersInfo[1]);
		}
	}

	void OnEnable()
	{
		Host();
		_roomID.text = $"Room ID: {GenerateRoomID()}";
		if (NetworkManager.Singleton.IsClient)
		{
			_onlinePlayersInfo.OnListChanged += HandlePlayersStateChanged;
		}
		if (NetworkManager.Singleton.IsServer)
		{
			_playerOneNamePlate.SetActive(true);
			foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
			{
				_onlinePlayersInfo.Add(new OnlinePlayerInfo(
					client.ClientId,
					"Damon1",
					_waiting,
					1
				));
			}
		}
		NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnect;
		NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
	}

	void OnDisable()
	{
		for (int i = 0; i < _playerNameplates.Length; i++)
		{
			if (_onlinePlayersInfo.Count > i)
			{
				_playerNameplates[i].ResetToDefault();
			}
		}
		_cancelButton.gameObject.SetActive(false);
		_readyButton.gameObject.SetActive(true);
		_playerTwoNamePlate.SetActive(false);
		NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnect;
		NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
	}

	private void HandleClientConnect(ulong clientId)
	{
		_onlinePlayersInfo.Add(new OnlinePlayerInfo(
		   clientId,
		   "Damon2",
		   _waiting,
		   2
		));
		_playerTwoNamePlate.gameObject.SetActive(true);
		if (clientId == NetworkManager.Singleton.LocalClientId)
		{
			_roomID.text = $"Room ID: {Encoding.ASCII.GetString(NetworkManager.Singleton.NetworkConfig.ConnectionData)}";
		}
	}

	public void HandleClientDisconnect(ulong clientId)
	{
		_playerTwoNamePlate.gameObject.SetActive(false);
	}


	private void Host()
	{
		NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
		NetworkManager.Singleton.StartHost();
	}

	private void ApprovalCheck(byte[] connnectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
	{
		string roomId = Encoding.ASCII.GetString(connnectionData);
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

		ToggleReadyServerRpc();
		_readyButton.gameObject.SetActive(false);
		_cancelButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);
	}

	[ServerRpc(RequireOwnership = false)]
	private void ToggleReadyServerRpc(ServerRpcParams serverRpcParams = default)
	{
		_onlinePlayersInfo[0] = new OnlinePlayerInfo(
			_onlinePlayersInfo[0].ClientId,
			_onlinePlayersInfo[0].PlayerName,
			_ready,
			2
		);
	}

	public void Cancel()
	{
		_cancelButton.gameObject.SetActive(false);
		_readyButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_readyButton.gameObject);
	}

	public void Leave()
	{
		NetworkManager.Singleton.Shutdown();
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
