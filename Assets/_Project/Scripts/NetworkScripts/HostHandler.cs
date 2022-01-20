using Demonics.UI;
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
	[SerializeField] private OnlineSetupDemonMenu _onlineSetupDemonMenu = default;
	[SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
	private readonly string _glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
	private readonly string _ready = "Ready";
	private readonly string _waiting = "Waiting";

	private NetworkList<OnlinePlayerInfo> _onlinePlayersInfo;


	private void Awake()
	{
		_onlinePlayersInfo = new NetworkList<OnlinePlayerInfo>();
		_roomID.text = $"Room ID: {GenerateRoomID()}";
	}
	private void HandlePlayersStateChanged(NetworkListEvent<OnlinePlayerInfo> onlinePlayerState)
	{
		for (int i = 0; i < _playerNameplates.Length; i++)
		{
			if (_onlinePlayersInfo.Count > i)
			{
				_playerNameplates[i].SetData(_onlinePlayersInfo[i]);
			}
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
			NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnect;
			NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnect;
			foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
			{
				HandleClientConnect(client.ClientId);
			}
		}
	}

	void OnDisable()
	{
		_cancelButton.gameObject.SetActive(false);
		_readyButton.gameObject.SetActive(true);
		if (NetworkManager.Singleton)
		{
			NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnect;
			NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
		}
	}

	public override void OnDestroy()
	{
		base.OnDestroy();

		_onlinePlayersInfo.OnListChanged -= HandlePlayersStateChanged;

		if (NetworkManager.Singleton)
		{
			NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnect;
			NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnect;
		}
	}

	private void HandleClientConnect(ulong clientId)
	{
		PlayerData playerData = NetPortalManager.Instance.GetPlayerData(clientId);
		if (playerData != null)
		{
			_onlinePlayersInfo.Add(new OnlinePlayerInfo(
				clientId,
				playerData.PlayerName,
				_waiting,
				playerData.Assist,
				playerData.Color,
				playerData.Character
			));
		}
	}

	public void HandleClientDisconnect(ulong clientId)
	{
		for (int i = 0; i < _onlinePlayersInfo.Count; i++)
		{
			if (_onlinePlayersInfo[i].ClientId == clientId)
			{
				_onlinePlayersInfo.RemoveAt(i);
				break;
			}
		}
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
				_onlinePlayersInfo[i].Assist,
				_onlinePlayersInfo[i].Color,
				_onlinePlayersInfo[i].Character
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
		SceneSettings.PlayerOne = _onlinePlayersInfo[0].Character;
		SceneSettings.PlayerTwo = _onlinePlayersInfo[1].Character;
		SceneSettings.ColorOne = _onlinePlayersInfo[0].Color;
		SceneSettings.ColorTwo = _onlinePlayersInfo[1].Color;
		SceneSettings.AssistOne = _onlinePlayersInfo[0].Assist; ;
		SceneSettings.AssistTwo = _onlinePlayersInfo[1].Assist; ;
		SceneSettings.NameOne = _onlinePlayersInfo[0].PlayerName.ToString();
		SceneSettings.NameTwo = _onlinePlayersInfo[1].PlayerName.ToString();
		SceneSettings.ControllerOne = "ControllerOne";
		SceneSettings.ControllerTwo = "Keyboard";
		SceneSettings.SceneSettingsDecide = true;
	}

	private void StartGame()
	{
		SceneSettings.StageIndex = 0;
		SceneSettings.PlayerOne = _onlinePlayersInfo[0].Character;
		SceneSettings.PlayerTwo = _onlinePlayersInfo[1].Character;
		SceneSettings.ColorOne = _onlinePlayersInfo[0].Color;
		SceneSettings.ColorTwo = _onlinePlayersInfo[1].Color;
		SceneSettings.AssistOne = _onlinePlayersInfo[0].Assist; ;
		SceneSettings.AssistTwo = _onlinePlayersInfo[1].Assist; ;
		SceneSettings.NameOne = _onlinePlayersInfo[0].PlayerName.ToString();
		SceneSettings.NameTwo = _onlinePlayersInfo[1].PlayerName.ToString();
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
				_onlinePlayersInfo[i].Assist,
				_onlinePlayersInfo[i].Color,
				_onlinePlayersInfo[i].Character
				);
			}
		}
	}

	public void Leave()
	{
		_onlinePlayersInfo.Clear();
		NetworkManager.Singleton.ConnectionApprovalCallback -= _onlineSetupMenu.ApprovalCheck;
		NetPortalManager.Instance.ClearPlayerData();
		NetworkManager.Singleton.Shutdown();
	}

	public void CopyRoomId()
	{
		GUIUtility.systemCopyBuffer = _roomID.text.Substring(9);
	}
}
