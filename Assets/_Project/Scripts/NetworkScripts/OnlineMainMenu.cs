using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnlineMainMenu : BaseMenu
{
    [SerializeField] private NetworkManagerLobby _networkManager = default;
    [SerializeField] private GameObject _connectingGroup = default;
    [SerializeField] private GameObject _connectedGroup = default;


    async void Awake()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            _connectingGroup.SetActive(false);
            _connectedGroup.SetActive(true);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
