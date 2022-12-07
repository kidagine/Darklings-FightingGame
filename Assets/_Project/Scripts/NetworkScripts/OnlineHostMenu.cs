using System;
using System.Collections;
using System.Collections.Generic;
using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        }
        else
        {
            _creatingLobby.SetActive(false);
            _lobbyCreated.SetActive(true);
        }
    }

    public void CopyLobbyId()
    {
        GUIUtility.systemCopyBuffer = _lobbyId;
    }

    public void OpenAsClient(DemonData[] demonDatas, string lobbyId)
    {
        Hosting = false;
        _nameplates[0].SetDemonData(demonDatas[0]);
        _nameplates[1].SetDemonData(demonDatas[1]);
        _creatingLobby.SetActive(false);
        _lobbyCreated.SetActive(true);
        _copyLobbyId.SetActive(false);
        _lobbyIdText.text = $"Lobby ID: {lobbyId.ToUpper()}";
        StartGame(demonDatas);
    }

    public void Ready()
    {
        //_networkManager.JoinLobby();
    }

    private void StartGame(DemonData[] demonDatas)
    {
        SceneSettings.IsOnline = true;
        SceneSettings.OnlineIndex = 0;
        SceneSettings.ControllerOneScheme = "Keyboard";
        SceneSettings.ControllerTwoScheme = "Keyboard";
        SceneSettings.PlayerOne = demonDatas[0].character;
        SceneSettings.PlayerTwo = demonDatas[1].character;
        SceneSettings.AssistOne = demonDatas[0].assist;
        SceneSettings.AssistTwo = demonDatas[1].assist;
        SceneSettings.ColorOne = demonDatas[0].color;
        SceneSettings.ColorTwo = demonDatas[1].color;
        SceneSettings.SceneSettingsDecide = true;
        SceneSettings.StageIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(StageTypeEnum)).Length - 1);
        _fadeHandler.onFadeEnd.AddListener(() => SceneManager.LoadScene(2));
        _fadeHandler.StartFadeTransition(true);
    }
}
