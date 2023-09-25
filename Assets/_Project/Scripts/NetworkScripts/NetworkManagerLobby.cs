using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using System.Net;
using System.Net.Sockets;
using UnityEngine.SceneManagement;
using Unity.Netcode.Transports.UTP;
using STUN.Attributes;
using STUN;

public class NetworkManagerLobby : MonoBehaviour
{
    [SerializeField] private OnlineErrorMenu _onlineErrorMenu = default;
    [SerializeField] private int _maxPlayers = 2;
    private Lobby _hostLobby;
    private Lobby _clientLobby;
    private float _lobbyUpdateTimer;
    public Action OnLobbyUpdate;

    //Initialize the Unity services and authenticate the user anonymously
    //Will change when accounts are introduced
    public static async void Authenticate()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in:" + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    //Host a lobby
    public async Task<string> CreateLobby(DemonData demonData)
    {
        Lobby lobby;
        Unity.Services.Lobbies.Models.Player player = null;
        try
        {
            player = GetPlayer(demonData);
        }
        catch (System.Exception e)
        {
            _onlineErrorMenu.Show("Host:" + e.Message);
            return null;
        }
        CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
        {
            IsPrivate = false,
            Player = player,
            Data = new Dictionary<string, DataObject>
            {
                {
                    "Code", new DataObject(
                        visibility: DataObject.VisibilityOptions.Public,
                        value: "111111",
                        index: DataObject.IndexOptions.S1)
                }
            }
        };
        try
        {
            lobby = await LobbyService.Instance.CreateLobbyAsync("darklings", _maxPlayers, createLobbyOptions);
            _hostLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            _onlineErrorMenu.Show("Lobby:" + e.Reason.ToString());
            return null;
        }
        return lobby.LobbyCode;
    }

    public Lobby GetHostLobby()
    {
        return _hostLobby;
    }

    public Lobby GetClientLobby()
    {
        return _clientLobby;
    }


    //Quick join the first found lobby
    public async Task<Lobby> QuickJoinLobby(DemonData demonData)
    {
        Lobby lobby;
        Unity.Services.Lobbies.Models.Player player = null;
        try
        {
            player = GetPlayer(demonData);
        }
        catch (System.Exception e)
        {
            _onlineErrorMenu.Show(e.Message);
            return null;
        }
        QuickJoinLobbyOptions quickJoinLobbyByCodeOptions = new QuickJoinLobbyOptions
        {
            Player = GetPlayer(demonData)
        };
        try
        {
            lobby = await Lobbies.Instance.QuickJoinLobbyAsync(quickJoinLobbyByCodeOptions);
            _clientLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            _onlineErrorMenu.Show(e.Reason.ToString());
            return null;
        }
        return lobby;
    }
    //Search for lobbies
    public async Task<Lobby[]> SearchLobbies()
    {
        Lobby[] lobbies;
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions();
            queryLobbiesOptions.Count = 20;
            queryLobbiesOptions.Filters = new List<QueryFilter>();
            {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value: "0");
            }
            QueryResponse query = await Lobbies.Instance.QueryLobbiesAsync(queryLobbiesOptions);
            lobbies = query.Results.ToArray();
        }
        catch (LobbyServiceException e)
        {
            _onlineErrorMenu.Show(e.Reason.ToString());
            return null;
        }
        if (lobbies.Length == 0)
        {
            _onlineErrorMenu.Show("No rooms found");
            return null;
        }
        return lobbies;
    }

    //Join the lobby given a lobby Id
    public async Task<Lobby> JoinLobby(DemonData demonData, string lobbyId)
    {
        Lobby lobby;
        Unity.Services.Lobbies.Models.Player player = null;
        try
        {
            player = GetPlayer(demonData);
        }
        catch (System.Exception e)
        {
            _onlineErrorMenu.Show(e.Message);
            return null;
        }
        JoinLobbyByIdOptions joinLobbyByIdOptions = new JoinLobbyByIdOptions
        {
            Player = GetPlayer(demonData)
        };
        try
        {
            lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId, joinLobbyByIdOptions);
            _clientLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            _onlineErrorMenu.Show(e.Reason.ToString());
            return null;
        }
        return lobby;
    }
    //Join the lobby given a lobby code
    public async Task<Lobby> JoinLobbyByCode(DemonData demonData, string lobbyCode)
    {
        Lobby lobby;
        Unity.Services.Lobbies.Models.Player player = null;
        try
        {
            player = GetPlayer(demonData);
        }
        catch (System.Exception e)
        {
            _onlineErrorMenu.Show(e.Message);
            return null;
        }
        JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
        {
            Player = GetPlayer(demonData)
        };
        try
        {
            lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOptions);
            _clientLobby = lobby;
        }
        catch (LobbyServiceException e)
        {
            _onlineErrorMenu.Show(e.Reason.ToString());
            return null;
        }
        return lobby;
    }

    //Update when a player is ready
    public async void UpdateLobbyReady(bool ready, bool isHost)
    {
        string lobbyId;
        string playerId;
        if (isHost)
        {
            lobbyId = _hostLobby.Id;
            playerId = _hostLobby.Players[0].Id;
        }
        else
        {
            lobbyId = _clientLobby.Id;
            playerId = _clientLobby.Players[1].Id;
        }
        UpdateLobbyOptions updateLobbyOptions = new UpdateLobbyOptions();
        await LobbyService.Instance.UpdatePlayerAsync(lobbyId, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
        {
            Data = new Dictionary<string, PlayerDataObject>{
                { "Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, ready.ToString())}
            }
        });
    }

    //Delete lobby as a host
    public async Task DeleteLobby()
    {
        string id = _hostLobby.Id;
        _hostLobby = null;
        await LobbyService.Instance.DeleteLobbyAsync(id);
    }

    //Leave lobby as a client
    public async Task LeaveLobby()
    {
        await LobbyService.Instance.RemovePlayerAsync(_clientLobby.Id, AuthenticationService.Instance.PlayerId);
    }

    void Update()
    {
        HandleLobbyPollForUpdates();
    }

    //Poll for updates, because there is a certain limit given by Unity, we have to wait before we check for an update
    private async void HandleLobbyPollForUpdates()
    {
        if (_hostLobby != null)
        {
            _lobbyUpdateTimer -= Time.unscaledDeltaTime;
            if (_lobbyUpdateTimer < 0)
            {
                _lobbyUpdateTimer = 1.1f;
                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(_hostLobby.Id);
                _hostLobby = lobby;
                OnLobbyUpdate?.Invoke();
            }
        }
        else if (_clientLobby != null)
        {
            _lobbyUpdateTimer -= Time.unscaledDeltaTime;
            if (_lobbyUpdateTimer < 0)
            {
                _lobbyUpdateTimer = 1.1f;
                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(_clientLobby.Id);
                _clientLobby = lobby;
                OnLobbyUpdate?.Invoke();
            }
        }
    }

    //Get the Player data required for the lobby and P2P connection
    private Unity.Services.Lobbies.Models.Player GetPlayer(DemonData demonData)
    {
        string address = "";
        string port = "";
        try
        {
            PublicIp(out address, out port);
        }
        catch (Exception)
        {
            throw;
        }
        return new Unity.Services.Lobbies.Models.Player
        {
            Data = new Dictionary<string, PlayerDataObject>{
                { "DemonName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.demonName)},
                { "Character", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.character.ToString())},
                { "Assist", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.assist.ToString())},
                { "Color", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.color.ToString())},
                { "Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, "False")},
                { "Ip", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, address)},
                { "Port", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public,port)},
                { "PrivateIp", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, PrivateIp())},
            }
        };
    }

    //Use DNS to get the private Ip, this is done for LAN P2P connections
    private string PrivateIp()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "127.0.0.1";
    }

    //Use a STUN server for port forwarding, this is done for WAN P2P connections
    private void PublicIp(out string address, out string port)
    {
        if (!STUNUtils.TryParseHostAndPort("stun1.l.google.com:19302", out IPEndPoint stunEndPoint))
            throw new Exception("Failed to establish connection");

        STUNClient.ReceiveTimeout = 500;
        var queryResult = STUNClient.Query(stunEndPoint, STUNQueryType.ExactNAT, true, NATTypeDetectionRFC.Rfc3489);
        if (queryResult.QueryError != STUNQueryError.Success)
            throw new Exception("Connection Failed");


        address = queryResult.PublicEndPoint.Address.ToString();
        port = queryResult.PublicEndPoint.Port.ToString();
    }
}
