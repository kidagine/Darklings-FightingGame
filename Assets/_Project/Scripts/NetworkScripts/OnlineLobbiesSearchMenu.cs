using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class OnlineLobbiesSearchMenu : BaseMenu
{
    [SerializeField] private GameObject _lobbyPrefab = default;
    [SerializeField] private Transform _lobbiesGroup = default;
    [SerializeField] private GameObject _joiningGroup = default;
    [SerializeField] private GameObject _lobbiesSearchGroup = default;
    [SerializeField] private NetworkManagerLobby _networkManager = default;
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [SerializeField] private OnlineHostMenu _onlineHostMenu = default;
    private Selectable _firstSelectable;


    public void SetLobbies(Lobby[] lobbies)
    {
        foreach (Transform child in _lobbiesGroup)
            Destroy(child.gameObject);
        for (int i = 0; i < lobbies.Length; i++)
        {
            LobbyButton lobbyButton = Instantiate(_lobbyPrefab, _lobbiesGroup).GetComponent<LobbyButton>();
            lobbyButton.SetData($"{lobbies[i].Players[0].Data["DemonName"].Value}'s Lobby");
            string id = lobbies[i].Id;
            lobbyButton.GetComponent<Button>().onClick.AddListener(() => JoinLobby(id));
            if (i == 0)
                _firstSelectable = lobbyButton.GetComponent<Selectable>();
        }
    }

    private async void JoinLobby(string lobbyId)
    {
        _lobbiesSearchGroup.SetActive(false);
        _joiningGroup.SetActive(true);
        Lobby lobby = await _networkManager.JoinLobby(_onlineSetupMenu.DemonData, lobbyId);
        if (lobby == null)
        {
            _lobbiesSearchGroup.SetActive(true);
            _joiningGroup.SetActive(false);
            Hide();
            return;
        }
        List<DemonData> demonDatas = new();
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
        _lobbiesSearchGroup.SetActive(true);
        _joiningGroup.SetActive(false);
        _onlineHostMenu.OpenAsClient(demonDatas.ToArray());
        OpenMenuHideCurrent(_onlineHostMenu);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(ActivateCoroutine());
    }

    IEnumerator ActivateCoroutine()
    {
        yield return null;
        _firstSelectable.Select();
    }
}

