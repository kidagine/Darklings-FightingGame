using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetPortalManager : MonoBehaviour
{
    public static NetPortalManager Instance => instance;
    private static NetPortalManager instance;

    private Dictionary<string, PlayerData> clientData;
    private Dictionary<ulong, string> clientIdToGuid;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        clientData = new Dictionary<string, PlayerData>();
        clientIdToGuid = new Dictionary<ulong, string>();
    }

    public PlayerData GetPlayerData(ulong clientId)
    {
        if (clientIdToGuid.TryGetValue(clientId, out string clientGuid))
        {
            if (clientData.TryGetValue(clientGuid, out PlayerData playerData))
            {
                return playerData;
            }
            else
            {
                Debug.LogWarning($"No player data found for client id: {clientId}");
            }
        }
        else
        {
            Debug.LogWarning($"No client guid found for client id: {clientId}");
        }
        return null;
    }

    public void AddPlayerData(PlayerData playerData)
    {
        string clientGuid = Guid.NewGuid().ToString();
        clientData.Add(clientGuid, playerData);
        clientIdToGuid.Add(NetworkManager.Singleton.LocalClientId, clientGuid);
    }

    public void AddPlayerData(ulong clientId, PlayerData playerData)
    {
        string clientGuid = Guid.NewGuid().ToString();
        clientData.Add(clientGuid, playerData);
        clientIdToGuid.Add(clientId, clientGuid);
    }
}
