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
    [SerializeField] private TextMeshProUGUI _readyText = default;
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
        _nameplates[0].gameObject.SetActive(false);
        _nameplates[1].gameObject.SetActive(false);
        for (int i = 0; i < lobby.Players.Count; i++)
        {
            demonDatas.Add(new DemonData()
            {
                demonName = lobby.Players[i].Data["DemonName"].Value,
                character = int.Parse(lobby.Players[i].Data["Character"].Value),
                assist = int.Parse(lobby.Players[i].Data["Assist"].Value),
                color = int.Parse(lobby.Players[i].Data["Color"].Value),
                ip = lobby.Players[i].Data["Ip"].Value
            });
            readyList.Add(bool.Parse(lobby.Players[i].Data["Ready"].Value));
            _nameplates[i].SetDemonData(demonDatas[i]);
            _nameplates[i].SetReady(readyList[i]);
        }
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
            if (ready)
            {
                _readyText.text = "Cancel";
            }
            else
            {
                _readyText.text = "Ready";
            }
            _networkManager.UpdateLobbyReady(ready, true);
        }
        else
        {
            bool ready = _nameplates[1].ToggleReady();
            if (ready)
            {
                _readyText.text = "Cancel";
            }
            else
            {
                _readyText.text = "Ready";
            }
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
        SceneSettings.NameOne = demonDatas[0].demonName;
        SceneSettings.NameTwo = demonDatas[1].demonName;
        SceneSettings.OnlineOneIp = demonDatas[0].ip;
        SceneSettings.OnlineTwoIp = demonDatas[1].ip;
        SceneSettings.ColorTwo = demonDatas[1].color;
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
        // _fadeHandler.onFadeEnd.AddListener(() => SceneManager.LoadScene("3. LoadingVersusScene", LoadSceneMode.Single));
        if (Hosting)
            _fadeHandler.onFadeEnd.AddListener(() => NetworkManager.Singleton.SceneManager.LoadScene("3. LoadingVersusScene", LoadSceneMode.Single));
        _fadeHandler.StartFadeTransition(true);
    }

    public void QuitLobby()
    {
        _creatingLobby.SetActive(true);
        _lobbyCreated.SetActive(false);
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
