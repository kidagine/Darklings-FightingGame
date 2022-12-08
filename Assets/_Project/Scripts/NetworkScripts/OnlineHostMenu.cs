using System;
using System.Collections;
using System.Collections.Generic;
using Demonics.UI;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class OnlineHostMenu : BaseMenu
{
    [SerializeField] private NetworkManagerLobby _networkManager = default;
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [SerializeField] private DemonNameplate[] _nameplates = default;
    [SerializeField] private TextMeshProUGUI _lobbyIdText = default;
    [SerializeField] private GameObject _creatingLobby = default;
    [SerializeField] private GameObject _lobbyCreated = default;
    [SerializeField] private GameObject _copyLobbyId = default;
    [SerializeField] private FadeHandler _fadeHandler = default;
    [SerializeField] private PlayerInput _playerInput = default;
    private string _lobbyId;
    public bool Hosting { get; set; } = true;

    async void Awake()
    {
        if (Hosting)
        {
            _nameplates[0].SetDemonData(_onlineSetupMenu.DemonData);
            _lobbyId = await _networkManager.CreateLobby(_onlineSetupMenu.DemonData);
            _lobbyIdText.text = $"Lobby ID: {_lobbyId}";
            _creatingLobby.SetActive(false);
            _lobbyCreated.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            _startingOption.Select();
            _networkManager.OnLobbyUpdate += UpdateLobby;
        }
        else
        {
            _creatingLobby.SetActive(false);
            _lobbyCreated.SetActive(true);
        }
    }

    private void UpdateLobby()
    {
        Lobby lobby = null;
        if (Hosting)
        {
            lobby = _networkManager.GetHostLobby();
        }
        else
        {
            lobby = _networkManager.GetClientLobby();
        }
        List<DemonData> demonDatas = new List<DemonData>();
        List<bool> readyList = new List<bool>();
        foreach (var player in lobby.Players)
        {
            demonDatas.Add(new DemonData()
            {
                demonName = player.Data["DemonName"].Value,
                character = int.Parse(player.Data["Character"].Value),
                assist = int.Parse(player.Data["Assist"].Value),
                color = int.Parse(player.Data["Color"].Value)
            });
            readyList.Add(bool.Parse(player.Data["Ready"].Value));
        }
        _nameplates[0].SetDemonData(demonDatas[0]);
        _nameplates[1].SetDemonData(demonDatas[1]);
        _nameplates[0].SetReady(readyList[0]);
        _nameplates[1].SetReady(readyList[1]);

        if (readyList[0] && readyList[1])
        {
            _networkManager.OnLobbyUpdate -= UpdateLobby;
            StartGame(demonDatas.ToArray());
        }
    }

    public void CopyLobbyId()
    {
        GUIUtility.systemCopyBuffer = _lobbyId;
    }

    public void OpenAsClient(DemonData[] demonDatas, string lobbyId)
    {
        _networkManager.OnLobbyUpdate += UpdateLobby;
        Hosting = false;
        _nameplates[0].SetDemonData(demonDatas[0]);
        _nameplates[1].SetDemonData(demonDatas[1]);
        _creatingLobby.SetActive(false);
        _lobbyCreated.SetActive(true);
        _copyLobbyId.SetActive(false);
        _lobbyIdText.text = $"Lobby ID: {lobbyId.ToUpper()}";
    }

    public void Ready()
    {
        if (Hosting)
        {
            bool ready = _nameplates[0].ToggleReady();
            _networkManager.UpdateLobbyReady(ready, true);
        }
        else
        {
            bool ready = _nameplates[1].ToggleReady();
            _networkManager.UpdateLobbyReady(ready, false);
        }
    }

    private void StartGame(DemonData[] demonDatas)
    {
        SceneSettings.IsOnline = true;
        if (Hosting)
        {
            SceneSettings.OnlineIndex = 0;
        }
        else
        {
            SceneSettings.OnlineIndex = 1;
        }
        SceneSettings.ControllerOne = _playerInput.devices[0];
        SceneSettings.ControllerTwo = _playerInput.devices[0];
        SceneSettings.ControllerOneScheme = "Keyboard";
        SceneSettings.ControllerTwoScheme = "Keyboard";
        SceneSettings.PlayerOne = demonDatas[0].character;
        SceneSettings.PlayerTwo = demonDatas[1].character;
        SceneSettings.AssistOne = demonDatas[0].assist;
        SceneSettings.AssistTwo = demonDatas[1].assist;
        SceneSettings.ColorOne = demonDatas[0].color;
        SceneSettings.ColorTwo = demonDatas[1].color;
        SceneSettings.SceneSettingsDecide = true;
        SceneSettings.StageIndex = 0;
        if (Hosting)
            _fadeHandler.onFadeEnd.AddListener(() => NetworkManager.Singleton.SceneManager.LoadScene("3. LoadingVersusScene", LoadSceneMode.Single));
        _fadeHandler.StartFadeTransition(true);
    }

    public void QuitLobby()
    {
        if (Hosting)
        {
            _networkManager.DeleteLobby();
        }
        else
        {
            _networkManager.LeaveLobby();
        }
    }
}
