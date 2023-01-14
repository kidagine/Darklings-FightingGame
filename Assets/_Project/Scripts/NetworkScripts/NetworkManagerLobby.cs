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
        CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
        {
            IsPrivate = false,
            Player = GetPlayer(demonData)
        };
        Lobby lobby = await LobbyService.Instance.CreateLobbyAsync("darklings", _maxPlayers, createLobbyOptions);
        _hostLobby = lobby;
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

    //Join the lobby given a lobby Id
    public async Task<Lobby> JoinLobby(DemonData demonData, string lobbyId)
    {
        JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
        {
            Player = GetPlayer(demonData)
        };
        Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyId, joinLobbyByCodeOptions);
        _clientLobby = lobby;
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
                try
                {
                    Lobby lobby = await LobbyService.Instance.GetLobbyAsync(_hostLobby.Id);
                    _hostLobby = lobby;
                    OnLobbyUpdate?.Invoke();
                }
                catch (System.Exception)
                {
                    throw new Exception($"Failed to get lobby with Id:{_hostLobby.Id}");
                }
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
        PublicIp(out address, out port);
        return new Unity.Services.Lobbies.Models.Player
        {
            Data = new Dictionary<string, PlayerDataObject>{
                { "DemonName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.demonName)},
                { "Character", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.character.ToString())},
                { "Assist", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.assist.ToString())},
                { "Color", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.color.ToString())},
                { "Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, "False")},
                { "Ip", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Private, address)},
                { "Port", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Private,port)},
                { "PrivateIp", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Private, PrivateIp())},
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
            throw new Exception("Failed to resolve STUN server address");

        STUNClient.ReceiveTimeout = 500;
        var queryResult = STUNClient.Query(stunEndPoint, STUNQueryType.ExactNAT, true, NATTypeDetectionRFC.Rfc3489);

        if (queryResult.QueryError != STUNQueryError.Success)
            throw new Exception("Query Error: " + queryResult.QueryError.ToString());

        address = queryResult.PublicEndPoint.Address.ToString();
        port = queryResult.PublicEndPoint.Port.ToString();
    }
}
