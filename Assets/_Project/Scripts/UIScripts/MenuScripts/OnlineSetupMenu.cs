using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnlineSetupMenu : BaseMenu
{
    [Header("Character")]
    [SerializeField] private Image _characterImage = default;
    [SerializeField] private Image _stageImage = default;
    [SerializeField] private TextMeshProUGUI _characterText = default;
    [SerializeField] private TextMeshProUGUI _stageText = default;
    [SerializeField] private GameObject _characterGroup = default;
    [SerializeField] private GameObject _stageGroup = default;
    [SerializeField] private Selectable _firstSelectable = default;
    [SerializeField] private Selectable _characterSelectable = default;
    [SerializeField] private Selectable _stageSelectable = default;
    [SerializeField] private TextMeshProUGUI _nameInputField = default;
    [SerializeField] private TMP_InputField _nameChangeInputField = default;
    [SerializeField] private PromptsInput _characterPrompts = default;
    [SerializeField] private PromptsInput _namePrompts = default;
    [SerializeField] private BaseMenu _nameChangeMenu = default;
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private CharacterOnlineButton[] _characterSelectables = default;
    [SerializeField] private PlayerUIRender _playerUIRender = default;
    [SerializeField] private TextMeshProUGUI _playerName = default;
    [SerializeField] private TextMeshProUGUI _hpText = default;
    [SerializeField] private TextMeshProUGUI _arcanaText = default;
    [SerializeField] private TextMeshProUGUI _speedText = default;
    [SerializeField] private HomeMenu _homeMenu = default;
    [SerializeField] private PlayerStatsSO[] _playerStatsSO = default;
    [Header("Stage")]
    [SerializeField] private Image _stageShowcaseImage = default;
    [SerializeField] private BaseSelector _stageSelector = default;
    [SerializeField] private BaseSelector _musicSelector = default;
    [SerializeField] private MusicSO _musicSO = default;
    [SerializeField] private StageSO[] _stagesSO = default;
    private StageSO _currentStage;
    public DemonData DemonData { get; set; } = new DemonData() { demonName = "Darkling", character = 0, assist = 0, color = 0 };


    public void SetDemonName(string demonName)
    {
        DemonicsSaver.Save("playerName", demonName);
        DemonData.demonName = demonName;
        _homeMenu.SetCharacter(DemonData.character, DemonData.color, DemonData.demonName);
    }

    public void SetAssist(int assist)
    {
        DemonData.assist = assist;
    }

    public void SetColor(int color)
    {
        _playerUIRender.SetSpriteLibraryAsset(color);
        DemonData.color = color;
        _homeMenu.SetCharacter(DemonData.character, DemonData.color, DemonData.demonName);
    }

    public override void Show()
    {
        base.Show();
        int index = int.Parse(DemonicsSaver.Load("character"));
        _characterSelectables[index].Activate();
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
        _playerUIRender.gameObject.SetActive(true);
    }

    public void SetStageSelectorValues()
    {
        List<string> stages = new();
        for (int i = 0; i < _stagesSO.Length; i++)
            stages.Add(_stagesSO[i].stageName);
        _stageSelector.SetValues(stages.ToArray());
    }

    public void SetMusicSelectorValues()
    {
        List<string> music = new();
        for (int i = 0; i < _musicSO.songs.Length; i++)
            music.Add(_musicSO.songs[i].ToString());
        _musicSelector.SetValues(music.ToArray());
    }

    public void SetStage(int index)
    {
        _currentStage = _stagesSO[index];
        if (SceneSettings.Bit1)
            _stageShowcaseImage.sprite = _stagesSO[index].bit1Stage;
        else
            _stageShowcaseImage.sprite = _stagesSO[index].colorStage;
        if (index == 0)
            SceneSettings.OnlineStageRandom = true;
        else
        {
            SceneSettings.OnlineStageRandom = false;
            SceneSettings.OnlineStageIndex = index - 1;
        }
    }

    public void Set1Bit(int index)
    {
        if (index == 0)
        {
            _stageShowcaseImage.sprite = _currentStage.colorStage;
            SceneSettings.OnlineBit1 = false;
        }
        else if (index == 1)
        {
            _stageShowcaseImage.sprite = _currentStage.bit1Stage;
            SceneSettings.OnlineBit1 = true;
        }
    }

    public void SetMusic(int index)
    {
        SceneSettings.OnlineMusicName = _musicSO.songs[index].ToString();
    }

    public void SetCharacterImage(PlayerStatsSO playerStats)
    {
        _playerUIRender.gameObject.SetActive(true);
        _playerName.text = Regex.Replace(playerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
        _playerUIRender.SetPlayerStat(playerStats);
        _playerUIRender.SetAnimator(playerStats._animation);
        _hpText.text = $"LV{playerStats.defenseLevel}";
        _arcanaText.text = $"LV{playerStats.arcanaLevel}";
        _speedText.text = $"LV{playerStats.speedLevel}";
        SetColorValues(playerStats);
    }

    private void SetColorValues(PlayerStatsSO playerStats)
    {
    }

    public void SelectCharacterImage(PlayerStatsSO playerStats)
    {
        for (int i = 0; i < _characterSelectables.Length; i++)
            _characterSelectables[i].Deactivate();
        DemonicsSaver.Save("character", playerStats.characterIndex.ToString());
        SetCharacter(playerStats.characterIndex);
        DemonData.character = playerStats.characterIndex;
        _homeMenu.SetCharacter(playerStats.characterIndex, DemonData.color, DemonData.demonName);
    }

    public void OpenChangeName()
    {
        _nameChangeInputField.text = _nameInputField.text;
        _nameChangeMenu.Show();
        _nameChangeInputField.Select();
        _inputManager.SetPrompts(_namePrompts);
    }

    public void CloseChangeName()
    {
        _nameInputField.text = _nameChangeInputField.text;
        _nameChangeMenu.Hide();
        _firstSelectable.Select();
        _inputManager.SetPrompts(_characterPrompts);
    }

    public void SetCharacter(int index)
    {
        _characterSelectables[index].Select();
        _playerUIRender.SetAnimator(_playerStatsSO[DemonData.character]._animation);
        _playerUIRender.SetSpriteLibraryAsset(DemonData.color);
        _playerName.text = Regex.Replace(_playerStatsSO[DemonData.character].characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
        _homeMenu.SetCharacter(DemonData.character, DemonData.color, DemonData.demonName);
    }

    public void SetDemonData(int character, int color, string playerName)
    {
        DemonData = new DemonData { character = character, demonName = playerName, color = color };
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
