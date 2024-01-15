using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OnlineHostMenu : BaseMenu
{
    [SerializeField] private NetworkManagerLobby _networkManager = default;
    [SerializeField] private BaseMenu _onlineMainMenu = default;
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [SerializeField] private DemonNameplate[] _nameplates = default;
    [SerializeField] private TextMeshProUGUI _readyText = default;
    [SerializeField] private TextMeshProUGUI _lobbyIdText = default;
    [SerializeField] private TextMeshProUGUI _currentControllerText = default;
    [SerializeField] private GameObject _creatingLobby = default;
    [SerializeField] private GameObject _lobbyCreated = default;
    [SerializeField] private GameObject _exitingLobby = default;
    [SerializeField] private BaseButton _readyButton = default;
    [SerializeField] private Button _copyButton = default;
    [SerializeField] private GameObject _copyLobbyId = default;
    [SerializeField] private FadeHandler _fadeHandler = default;
    [SerializeField] private PlayerInput _playerInput = default;
    private Button _readyButtonComponent;
    private string _lobbyId;
    private PlayerInput _storedPlayerInput = default;
    private bool _ready;
    public bool Hosting { get; set; } = true;

    async void OnEnable()
    {
        _readyButtonComponent = _readyButton.GetComponent<Button>();
        if (Hosting)
        {
            Debug.Log(_onlineSetupMenu.DemonData.demonName);
            _nameplates[0].SetDemonData(_onlineSetupMenu.DemonData);
            _lobbyId = await _networkManager.CreateLobby(_onlineSetupMenu.DemonData);
            if (_lobbyId == null)
            {
                _creatingLobby.SetActive(false);
                _lobbyCreated.SetActive(true);
                Hide();
                return;
            }
            _lobbyIdText.text = $"Room ID: {_lobbyId}";
            _creatingLobby.SetActive(false);
            _lobbyCreated.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            _startingOption.Select();
            _networkManager.OnLobbyUpdate += UpdateLobby;
            _readyButton.Deactivate();
        }
        else
        {
            _creatingLobby.SetActive(false);
            _lobbyCreated.SetActive(true);
        }
    }

    private void Update()
    {
        if (!_ready)
        {
            _storedPlayerInput = _playerInput;
            _currentControllerText.text = $"Match Controller: {_storedPlayerInput.currentControlScheme}";
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
        if (lobby.Players.Count >= 2)
        {
            _readyButton.Activate();
            Navigation navigation = _copyButton.navigation;
            navigation.selectOnDown = _readyButtonComponent;
            navigation.selectOnRight = _readyButtonComponent;
            _copyButton.navigation = navigation;
        }
        else
        {
            _readyButton.Deactivate();
        }
        for (int i = 0; i < lobby.Players.Count; i++)
        {
            demonDatas.Add(new DemonData()
            {
                demonName = lobby.Players[i].Data["DemonName"].Value,
                character = int.Parse(lobby.Players[i].Data["Character"].Value),
                assist = int.Parse(lobby.Players[i].Data["Assist"].Value),
                color = int.Parse(lobby.Players[i].Data["Color"].Value),
                ip = lobby.Players[i].Data["Ip"].Value,
                port = int.Parse(lobby.Players[i].Data["Port"].Value),
                privateIp = lobby.Players[i].Data["PrivateIp"].Value,
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

    public void OpenAsClient(DemonData[] demonDatas, string lobbyId = null)
    {
        _networkManager.OnLobbyUpdate += UpdateLobby;
        Hosting = false;
        _nameplates[0].SetDemonData(demonDatas[0]);
        _nameplates[1].SetDemonData(demonDatas[1]);
        _creatingLobby.SetActive(false);
        _lobbyCreated.SetActive(true);
        _copyLobbyId.SetActive(false);
        if (lobbyId == null)
            _lobbyIdText.text = "";
        else
            _lobbyIdText.text = $"Room ID: {lobbyId.ToUpper()}";
    }

    public void Ready()
    {
        if (Hosting)
        {
            _ready = _nameplates[0].ToggleReady();
            if (_ready)
            {
                _readyText.text = "Cancel";
            }
            else
            {
                _readyText.text = "Ready";
            }
            _networkManager.UpdateLobbyReady(_ready, true);
        }
        else
        {
            _ready = _nameplates[1].ToggleReady();
            if (_ready)
            {
                _readyText.text = "Cancel";
            }
            else
            {
                _readyText.text = "Ready";
            }
            _networkManager.UpdateLobbyReady(_ready, false);
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
        SceneSettings.PortOne = demonDatas[0].port;
        SceneSettings.PortTwo = demonDatas[1].port;
        SceneSettings.PrivateOneIp = demonDatas[0].privateIp;
        SceneSettings.PrivateTwoIp = demonDatas[1].privateIp;
        SceneSettings.ColorTwo = demonDatas[1].color;
        SceneSettings.ControllerOne = _storedPlayerInput.devices[0];
        SceneSettings.ControllerTwo = _storedPlayerInput.devices[0];
        SceneSettings.ControllerOneScheme = Hosting == true ? _storedPlayerInput.currentControlScheme : "Keyboard";
        SceneSettings.ControllerTwoScheme = Hosting == false ? _storedPlayerInput.currentControlScheme : "Keyboard";
        SceneSettings.PlayerOne = demonDatas[0].character;
        SceneSettings.PlayerTwo = demonDatas[1].character;
        SceneSettings.AssistOne = demonDatas[0].assist;
        SceneSettings.AssistTwo = demonDatas[1].assist;
        SceneSettings.ColorOne = demonDatas[0].color;
        SceneSettings.ColorTwo = demonDatas[1].color;
        SceneSettings.SceneSettingsDecide = true;
        SceneSettings.Bit1 = SceneSettings.OnlineBit1;
        if (SceneSettings.OnlineStageRandom)
            SceneSettings.StageIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(StageTypeEnum)).Length - 1);
        else
            SceneSettings.StageIndex = SceneSettings.OnlineStageIndex;
        SceneSettings.MusicName = SceneSettings.OnlineMusicName;
        _fadeHandler.onFadeEnd.AddListener(() => SceneManager.LoadScene(1, LoadSceneMode.Single));
        _fadeHandler.StartFadeTransition(true, 0.35f);
    }

    public async void QuitLobby()
    {
        _creatingLobby.SetActive(false);
        _lobbyCreated.SetActive(false);
        if (Hosting)
        {
            _exitingLobby.SetActive(true);
            await _networkManager.DeleteLobby();
            OpenMenuHideCurrent(_onlineMainMenu);
            _exitingLobby.SetActive(false);
            _creatingLobby.SetActive(true);
        }
        else
        {
            _exitingLobby.SetActive(true);
            await _networkManager.LeaveLobby();
            OpenMenuHideCurrent(_onlineMainMenu);
            _exitingLobby.SetActive(false);
            _creatingLobby.SetActive(true);
        }
    }
}
