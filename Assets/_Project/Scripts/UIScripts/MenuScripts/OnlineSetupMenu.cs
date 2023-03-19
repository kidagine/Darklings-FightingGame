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
    [SerializeField] private GameObject _colorsValues = default;
    [SerializeField] private Selectable _characterSelectable = default;
    [SerializeField] private Selectable _stageSelectable = default;
    [SerializeField] private TMP_InputField _nameInputField = default;
    [SerializeField] private TMP_InputField _nameChangeInputField = default;
    [SerializeField] private PromptsInput _characterPrompts = default;
    [SerializeField] private PromptsInput _namePrompts = default;
    [SerializeField] private GameObject _nameChangeGroup = default;
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private CharacterOnlineButton[] _characterSelectables = default;
    [SerializeField] private PlayerUIRender _playerUIRender = default;
    [SerializeField] private TextMeshProUGUI _playerName = default;
    [SerializeField] private TextMeshProUGUI _hpText = default;
    [SerializeField] private TextMeshProUGUI _arcanaText = default;
    [SerializeField] private TextMeshProUGUI _speedText = default;
    [Header("Stage")]
    [SerializeField] private Image _stageShowcaseImage = default;
    [SerializeField] private GameObject _selectorTextPrefab = default;
    [SerializeField] private Transform _stageSelectorValues = default;
    [SerializeField] private Transform _musicSelectorValues = default;
    [SerializeField] private MusicSO _musicSO = default;
    [SerializeField] private StageSO[] _stagesSO = default;
    private StageSO _currentStage;
    public DemonData DemonData { get; set; } = new DemonData() { demonName = "DemonFighter", character = 0, assist = 0, color = 0 };


    public void SetDemonName(string demonName)
    {
        DemonicsSaver.Save("playerName", demonName);
        DemonData.demonName = demonName;
    }

    public void SetAssist(int assist)
    {
        DemonData.assist = assist;
    }

    public void SetColor(int color)
    {
        _playerUIRender.SetSpriteLibraryAsset(color);
        DemonData.color = color;
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
        for (int i = 0; i < _stagesSO.Length; i++)
        {
            GameObject selector = Instantiate(_selectorTextPrefab, _stageSelectorValues);
            TextMeshProUGUI selectorText = selector.GetComponent<TextMeshProUGUI>();
            selectorText.text = _stagesSO[i].stageName;
            if (i == 0)
            {
                selector.SetActive(true);
            }
        }
    }
    public void SetMusicSelectorValues()
    {
        for (int i = 0; i < _musicSO.songs.Length; i++)
        {
            GameObject selector = Instantiate(_selectorTextPrefab, _musicSelectorValues);
            TextMeshProUGUI selectorText = selector.GetComponent<TextMeshProUGUI>();
            selectorText.text = _musicSO.songs[i].ToString();
            if (i == 0)
            {
                selector.SetActive(true);
            }
        }
    }
    public void SetStage(int index)
    {
        _currentStage = _stagesSO[index];
        if (SceneSettings.Bit1)
        {
            _stageShowcaseImage.sprite = _stagesSO[index].bit1Stage;
        }
        else
        {
            _stageShowcaseImage.sprite = _stagesSO[index].colorStage;
        }
        if (index == 0)
        {
            SceneSettings.OnlineStageRandom = true;
        }
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
        // foreach (Transform child in _colorsValues.transform)
        // {
        //     Destroy(child.gameObject);
        // }
        // for (int i = 0; i < playerStats._animation.spriteAtlas.Length; i++)
        // {
        //     GameObject selector = Instantiate(_selectorTextPrefab, _colorsValues.transform);
        //     TextMeshProUGUI selectorText = selector.GetComponent<TextMeshProUGUI>();
        //     selectorText.text = (i + 1).ToString();
        //     if (i == DemonData.assist)
        //     {
        //         selector.SetActive(true);
        //     }
        // }
    }

    public void SelectCharacterImage(PlayerStatsSO playerStats)
    {
        for (int i = 0; i < _characterSelectables.Length; i++)
        {
            _characterSelectables[i].Deactivate();
        }
        DemonicsSaver.Save("character", playerStats.characterIndex.ToString());
        SetCharacter(playerStats.characterIndex);
    }

    public void OpenChangeName()
    {
        _nameChangeInputField.text = _nameInputField.text;
        _nameChangeGroup.SetActive(true);
        _nameChangeInputField.Select();
        _inputManager.CurrentPrompts = _namePrompts;
    }

    public void CloseChangeName()
    {
        _nameInputField.text = _nameChangeInputField.text;
        _nameChangeGroup.SetActive(false);
        _characterSelectable.Select();
        _inputManager.CurrentPrompts = _characterPrompts;
    }

    public void SetCharacter(int index)
    {
        DemonData.character = index;
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
