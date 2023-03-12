using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineSetupMenu : BaseMenu
{
    [SerializeField] private PlayerPreferences _playerPreferences = default;
    public DemonData DemonData { get; set; } = new DemonData() { demonName = "DemonFighter", character = 0, assist = 0, color = 0 };


    public void SetDemonName(string demonName)
    {
        DemonicsSaver.Save("playerName", demonName);
        DemonData.demonName = demonName;
    }

    public void SetCharacter(int character)
    {
        DemonData.character = character;
    }

    public void SetAssist(int assist)
    {
        DemonData.assist = assist;
    }

    public void SetColor(int color)
    {
        DemonData.color = color;
    }
}

public class DemonData
{
    public string demonName;
    public int character;
    public int assist;
    public int color;
    public string ip;
    public int port;
    public string privateIp;
}
