using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OnlineClientMenu : BaseMenu
{
    [SerializeField] private NetworkManagerLobby _networkManager = default;
    [SerializeField] private TMP_InputField _roomId = default;

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
    }

    public void JoinLobby()
    {
        string ipAddress = _roomId.text;
        _networkManager.networkAddress = "localhost";
        _networkManager.StartClient();
    }

    private void HandleClientConnected()
    {
        Hide();
    }
}
