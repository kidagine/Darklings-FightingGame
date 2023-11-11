using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
public class OnlineClientMenu : BaseMenu
{
    [SerializeField] private NetworkManagerLobby _networkManager = default;
    [SerializeField] private OnlineHostMenu _onlineHostMenu = default;
    [SerializeField] private OnlineLobbiesSearchMenu _onlineLobbiesSearchMenu = default;
    [SerializeField] private BaseMenu _roomIdMenu = default;
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [SerializeField] private TextMeshProUGUI _lobbyIdInputField = default;
    [SerializeField] private TMP_InputField _lobbyIdChangeInputField = default;
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private PromptsInput _searchPrompts = default;
    [SerializeField] private PromptsInput _idPrompts = default;
    [SerializeField] private GameObject _joiningLobbyGroup = default;
    [SerializeField] private GameObject _lobbyJoinGroup = default;


    public async void SearchLobbies()
    {
        _joiningLobbyGroup.SetActive(true);
        _lobbyJoinGroup.SetActive(false);
        string lobbyId = _lobbyIdInputField.text;
        if (lobbyId != "")
        {
            Lobby lobby = await _networkManager.JoinLobbyByCode(_onlineSetupMenu.DemonData, lobbyId);
            if (lobby == null)
            {
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
            _joiningLobbyGroup.SetActive(false);
            _lobbyJoinGroup.SetActive(true);
            _onlineHostMenu.OpenAsClient(demonDatas.ToArray(), lobbyId);
            OpenMenuHideCurrent(_onlineHostMenu);
        }
        else
        {
            Lobby[] lobbies = await _networkManager.SearchLobbies();
            if (lobbies == null)
            {
                _lobbyIdInputField.text = "";
                _joiningLobbyGroup.SetActive(false);
                _lobbyJoinGroup.SetActive(true);
                Hide();
                return;
            }
            _onlineLobbiesSearchMenu.SetLobbies(lobbies);
            OpenMenuHideCurrent(_onlineLobbiesSearchMenu);
            _joiningLobbyGroup.SetActive(false);
            _lobbyJoinGroup.SetActive(true);
        }
    }

    private void HandleClientConnected()
    {
        _onlineHostMenu.Show();
        gameObject.SetActive(false);
    }
    public void OpenChangeId()
    {
        _lobbyIdChangeInputField.text = _lobbyIdInputField.text;
        _roomIdMenu.Show();
        _lobbyIdChangeInputField.Select();
        _inputManager.SetPrompts(_idPrompts);
    }

    public void CloseChangeId()
    {
        _lobbyIdInputField.text = _lobbyIdChangeInputField.text;
        _roomIdMenu.Hide();
        _inputManager.SetPrompts(_searchPrompts);
        StartCoroutine(SelectOption());
    }

    IEnumerator SelectOption()
    {
        yield return null;
        if (_startingOption != null)
        {
            _startingOption.Select();
        }
    }
}
