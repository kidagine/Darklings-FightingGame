using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerLobby : MonoBehaviour
{
    [SerializeField] private NetworkPlayerLobby _networkPlayerLobby = null;
    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public int PlayersInRoom { get; private set; }

    // public override void OnClientConnect()
    // {
    //     base.OnClientConnect();
    //     OnClientConnected?.Invoke();
    // }

    // public override void OnClientDisconnect()
    // {
    //     base.OnClientDisconnect();
    //     OnClientDisconnected?.Invoke();
    // }
    // public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    // {
    //     PlayersInRoom++;
    // }
}
