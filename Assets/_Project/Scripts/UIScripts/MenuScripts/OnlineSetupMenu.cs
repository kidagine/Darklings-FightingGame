using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlineSetupMenu : BaseMenu
{
    [SerializeField] private PlayerPreferences _playerPreferences = default;
    [SerializeField] private Image _characterImage = default;
    [SerializeField] private Image _stageImage = default;
    [SerializeField] private TextMeshProUGUI _characterText = default;
    [SerializeField] private TextMeshProUGUI _stageText = default;
    [SerializeField] private GameObject _characterGroup = default;
    [SerializeField] private GameObject _stageGroup = default;
    [SerializeField] private Selectable _characterSelectable = default;
    [SerializeField] private Selectable _stageSelectable = default;

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

    public void RightPage()
    {
        _characterImage.color = Color.black;
        _characterText.color = Color.white;
        _stageImage.color = Color.white;
        _stageText.color = Color.black;
        _characterGroup.SetActive(false);
        _stageGroup.SetActive(true);
        _stageSelectable.Select();
    }

    public void LeftPage()
    {
        _characterImage.color = Color.white;
        _characterText.color = Color.black;
        _stageImage.color = Color.black;
        _stageText.color = Color.white;
        _characterGroup.SetActive(true);
        _stageGroup.SetActive(false);
        _characterSelectable.Select();
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
