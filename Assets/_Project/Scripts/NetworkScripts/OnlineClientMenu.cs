using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
public class OnlineClientMenu : BaseMenu
{
    [SerializeField] private NetworkManagerLobby _networkManager = default;
    [SerializeField] private OnlineHostMenu _onlineHostMenu = default;
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [SerializeField] private TMP_InputField _lobbyIdInputField = default;
    [SerializeField] private GameObject _joiningLobbyGroup = default;
    [SerializeField] private GameObject _lobbyJoinGroup = default;


    public async void JoinLobby()
    {
        _joiningLobbyGroup.SetActive(true);
        _lobbyJoinGroup.SetActive(false);
        string lobbyId = _lobbyIdInputField.text;
        Lobby lobby = await _networkManager.JoinLobby(_onlineSetupMenu.DemonData, lobbyId);
        if (lobby == null)
        {
            _lobbyIdInputField.text = "";
            _joiningLobbyGroup.SetActive(false);
            _lobbyJoinGroup.SetActive(true);
            Hide();
            return;
        }
        List<DemonData> demonDatas = new List<DemonData>();
        foreach (var player in lobby.Players)
        {
            demonDatas.Add(new DemonData()
            {
                demonName = player.Data["DemonName"].Value,
                character = int.Parse(player.Data["Character"].Value),
                assist = int.Parse(player.Data["Assist"].Value),
                color = int.Parse(player.Data["Color"].Value)
            });
        }
        _onlineHostMenu.OpenAsClient(demonDatas.ToArray(), lobbyId);
        OpenMenuHideCurrent(_onlineHostMenu);
        _joiningLobbyGroup.SetActive(false);
        _lobbyJoinGroup.SetActive(true);
    }

    private void HandleClientConnected()
    {
        _onlineHostMenu.Show();
        gameObject.SetActive(false);
    }
}
