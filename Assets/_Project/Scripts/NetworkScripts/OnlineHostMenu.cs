using System.Collections;
using System.Collections.Generic;
using Demonics.UI;
using UnityEngine;

public class OnlineHostMenu : BaseMenu
{
    [SerializeField] private NetworkManagerLobby _networkManager = default;
    [SerializeField] private GameObject[] _nameplates = default;


    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
    }

    private void HandleClientConnected()
    {
        Debug.Log(_networkManager.PlayersInRoom);
        for (int i = 0; i < _networkManager.PlayersInRoom; i++)
        {
            _nameplates[i].SetActive(true);
        }
    }
}
