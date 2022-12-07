using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UNET;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using System.Net;
using System.Net.Sockets;

public class NetworkManagerLobby : MonoBehaviour
{
    [SerializeField] private int _maxPlayers = 2;
    private Lobby _hostLobby;
    private Lobby _clientLobby;
    private float _lobbyUpdateTimer;


    void Update()
    {
        // HandleLobbyPollForUpdates();
    }

    public static async void Authenticate()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in:" + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

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

    public async Task<Lobby> JoinLobby(DemonData demonData, string lobbyId)
    {
        JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
        {
            Player = GetPlayer(demonData)
        };
        NetworkManager.Singleton.StartClient();
        return await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyId, joinLobbyByCodeOptions);
    }
    public async void UpdateLobby()
    {
        //await LobbyManager.UpdatePlayerDataAsync(LobbyConverters.LocalToRemoteUserData(m_LocalUser));
    }

    public async void DeleteLobby()
    {
        await LobbyService.Instance.DeleteLobbyAsync(_hostLobby.Id);
    }

    public async void LeaveLobby()
    {
        await LobbyService.Instance.RemovePlayerAsync(_clientLobby.Id, AuthenticationService.Instance.PlayerId);
    }

    public void LoadScene()
    {
        //NetworkManager.Singleton.SceneManager.LoadScene();
    }

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
            }
        }
    }

    private Unity.Services.Lobbies.Models.Player GetPlayer(DemonData demonData)
    {
        return new Unity.Services.Lobbies.Models.Player
        {
            Data = new Dictionary<string, PlayerDataObject>{
                { "DemonName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.demonName)},
                { "Character", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.character.ToString())},
                { "Assist", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.assist.ToString())},
                { "Color", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, demonData.color.ToString())},
                { "Ip", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, GetLocalIPAddress())},
            }
        };
    }
    private string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
}
