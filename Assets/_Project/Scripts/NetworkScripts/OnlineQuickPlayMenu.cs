using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
public class OnlineQuickPlayMenu : BaseMenu
{
    [SerializeField] private OnlineHostMenu _onlineHostMenu = default;
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [SerializeField] private NetworkManagerLobby _networkManager = default;

    public override void Show()
    {
        base.Show();
        QuickJoinLobby();
    }

    private async void QuickJoinLobby()
    {
        Lobby lobby = await _networkManager.QuickJoinLobby(_onlineSetupMenu.DemonData);
        if (lobby == null)
        {
            Hide();
            return;
        }
        List<DemonData> demonDatas = new List<DemonData>();
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
        _onlineHostMenu.OpenAsClient(demonDatas.ToArray());
        OpenMenuHideCurrent(_onlineHostMenu);
    }
}
