using Demonics.UI;
using System;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HostHandler : NetworkBehaviour
{
	[SerializeField] private TextMeshProUGUI _roomID = default;
	[SerializeField] private PlayerNameplate[] _playerNameplates = default;
	[SerializeField] private BaseButton _readyButton = default;
	[SerializeField] private BaseButton _cancelButton = default;
	[SerializeField] private OnlineSetupDemonMenu _onLineSetupDemonMenu = default;
	private readonly string _glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
	private readonly string _ready = "Ready";
	private readonly string _waiting = "Waiting";

	private NetworkList<OnlinePlayerInfo> _onlinePlayersInfo;


	private void Awake()
	{
		_onlinePlayersInfo = new NetworkList<OnlinePlayerInfo>();
		_roomID.text = $"Room ID: {GenerateRoomID()}";
		Host();
	}
	private void HandlePlayersStateChanged(NetworkListEvent<OnlinePlayerInfo> onlinePlayerState)
	{
		_playerNameplates[0].SetData(_onlinePlayersInfo[0]);
		if (_onlinePlayersInfo.Count > 1)
		{
			_playerNameplates[1].SetData(_onlinePlayersInfo[1]);
		}
	}

	public override void OnNetworkSpawn()
	{
		if (IsClient)
		{
			_onlinePlayersInfo.OnListChanged += HandlePlayersStateChanged;
		}

		if (IsServer)
		{
			_playerNameplates[0].gameObject.SetActive(true);
			foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
			{
				AddClient(new OnlinePlayerInfo(client.ClientId, "Demon1", _waiting, 0));
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
		_playerNameplates[1].gameObject.SetActive(false);
		if (NetworkManager.Singleton)
		{
			NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnect;
			NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
		}
	}

	private void HandleClientConnect(ulong clientId)
	{
		AddClient(new OnlinePlayerInfo(clientId, "Demon2", _waiting, 0));
		_playerNameplates[1].gameObject.SetActive(true);
		if (clientId == NetworkManager.Singleton.LocalClientId && NetworkManager.Singleton.IsClient)
		{
			string payload = Encoding.ASCII.GetString(NetworkManager.Singleton.NetworkConfig.ConnectionData);
			ConnectionPayload connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload);
			_roomID.text = $"Room ID:{connectionPayload.RoomId}";
		}
	}

	private void AddClient(OnlinePlayerInfo onlinePlayerInfo)
	{
		_onlinePlayersInfo.Add(onlinePlayerInfo);
	}

	public void HandleClientDisconnect(ulong clientId)
	{
		_playerNameplates[1].gameObject.SetActive(false);
	}

	private void Host()
	{
		NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
		NetworkManager.Singleton.StartHost();
	}

	private void ApprovalCheck(byte[] connnectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
	{
		string payload = Encoding.ASCII.GetString(connnectionData);
		ConnectionPayload connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload);

		bool approveConnection = connectionPayload.RoomId == "abc";
		callback(true, null, approveConnection, null, null);
	}

	public string GenerateRoomID()
	{
		string roomID = "";
		for (int i = 0; i < 12; i++)
		{
			roomID += _glyphs[UnityEngine.Random.Range(0, _glyphs.Length)];
		}
		roomID = Regex.Replace(roomID.ToUpper(), ".{4}", "$0-");
		return roomID.Remove(roomID.Length - 1);
	}

	public void Ready()
	{
		ReadyServerRpc();
		_readyButton.gameObject.SetActive(false);
		_cancelButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);
	}

	[ServerRpc(RequireOwnership = false)]
	private void ReadyServerRpc(ServerRpcParams serverRpcParams = default)
	{
		for (int i = 0; i < _onlinePlayersInfo.Count; i++)
		{
			if (_onlinePlayersInfo[i].ClientId == serverRpcParams.Receive.SenderClientId)
			{
				_onlinePlayersInfo[i] = new OnlinePlayerInfo(
				_onlinePlayersInfo[i].ClientId,
				_onlinePlayersInfo[i].PlayerName,
				_ready,
				_onlinePlayersInfo[i].Portrait
				);
			}
		}
		if (_onlinePlayersInfo[0].IsReady == _ready && _onlinePlayersInfo[1].IsReady == _ready)
		{
			StartGameClientRpc();
			StartGame();
		}
	}

	[ClientRpc]
	private void StartGameClientRpc()
	{
		SceneSettings.StageIndex = 0;
		SceneSettings.PlayerOne = 0;
		SceneSettings.PlayerTwo = 0;
		SceneSettings.ColorOne = 0;
		SceneSettings.ColorTwo = 0;
		SceneSettings.AssistOne = 0;
		SceneSettings.AssistTwo = 0;
		SceneSettings.ControllerOne = "ControllerOne";
		SceneSettings.ControllerTwo = "Keyboard";
		SceneSettings.SceneSettingsDecide = true;
	}

	private void StartGame()
	{
		SceneSettings.StageIndex = 0;
		SceneSettings.PlayerOne = 0;
		SceneSettings.PlayerTwo = 0;
		SceneSettings.ColorOne = 0;
		SceneSettings.ColorTwo = 0;
		SceneSettings.AssistOne = 0;
		SceneSettings.AssistTwo = 0;
		SceneSettings.ControllerOne = "Keyboard";
		SceneSettings.ControllerTwo = "ControllerOne";
		SceneSettings.SceneSettingsDecide = true;
		NetworkManager.Singleton.SceneManager.LoadScene("LoadingVersusScene", LoadSceneMode.Single);
	}

	public void Cancel()
	{
		CancelServerRpc();
		_cancelButton.gameObject.SetActive(false);
		_readyButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_readyButton.gameObject);
	}

	[ServerRpc(RequireOwnership = false)]
	private void CancelServerRpc(ServerRpcParams serverRpcParams = default)
	{
		for (int i = 0; i < _onlinePlayersInfo.Count; i++)
		{
			if (_onlinePlayersInfo[i].ClientId == serverRpcParams.Receive.SenderClientId)
			{
				_onlinePlayersInfo[i] = new OnlinePlayerInfo(
				_onlinePlayersInfo[i].ClientId,
				_onlinePlayersInfo[i].PlayerName,
				_waiting,
				_onlinePlayersInfo[i].Portrait
				);
			}
		}
	}

	public void Leave()
	{
		if (NetworkManager.Singleton.IsHost)
		{
			NetworkManager.Singleton.Shutdown();
		}
		else if (NetworkManager.Singleton.IsClient)
		{
		}
		_playerNameplates[1].gameObject.SetActive(false);
	}

	public void CopyRoomId()
	{
		GUIUtility.systemCopyBuffer = _roomID.text.Substring(9);
	}
}
