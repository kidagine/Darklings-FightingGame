using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMenu : BaseMenu
{
    [SerializeField] private Selectable _startingSelectable;
    [SerializeField] private Selectable _1bitSelectable = default;
    [SerializeField] private GameObject _optionsGroup = default;
    [SerializeField] private GameObject _stagesGroup = default;
    [SerializeField] private GameObject _selectorTextPrefab = default;
    [SerializeField] private Transform _selectorValues = default;
    [SerializeField] private MusicSO _musicSO = default;
    [SerializeField] private BaseMenu _charactersSelectMenu = default;
    [SerializeField] private Image _stageImage = default;
    [SerializeField] private TextMeshProUGUI _stageName = default;
    private StageSO _stageSO;


    void Awake()
    {
        SceneSettings.Bit1 = false;
        SceneSettings.MusicName = "Random";
        SetMusicSelectorValues();
    }

    private void SetMusicSelectorValues()
    {
        for (int i = 0; i < _musicSO.songs.Length; i++)
        {
            GameObject selector = Instantiate(_selectorTextPrefab, _selectorValues);
            TextMeshProUGUI selectorText = selector.GetComponent<TextMeshProUGUI>();
            selectorText.text = _musicSO.songs[i].ToString();
            if (i == 0)
            {
                selector.SetActive(true);
            }
        }
    }

    public void SelectStageImage()
    {
        SceneSettings.SceneSettingsDecide = true;
        EventSystem.current.sendNavigationEvents = true;
        _optionsGroup.SetActive(true);
        _stagesGroup.SetActive(false);
        _1bitSelectable.Select();
    }

    public void SetStageImage(Sprite sprite)
    {
        _stageImage.sprite = sprite;
        _stageName.text = sprite.name;
    }

    public void SetStageImage(StageSO stageSO)
    {
        _stageSO = stageSO;
        _stageImage.sprite = stageSO.colorStage;
        _stageName.text = stageSO.stageName;
    }

    public void SetStageIndex(int index)
    {
        SceneSettings.StageIndex = index;
    }

    public void SetStageIndexRandom()
    {
        SceneSettings.RandomStage = true;
        SceneSettings.StageIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(StageTypeEnum)).Length - 1);
    }

    public void SetTrainingMode(bool state)
    {
        SceneSettings.IsTrainingMode = state;
    }

    public void Set1Bit(int index)
    {
        if (index == 0)
        {
            _stageImage.sprite = _stageSO.colorStage;
            SceneSettings.Bit1 = false;
        }
        else if (index == 1)
        {
            _stageImage.sprite = _stageSO.bit1Stage;
            SceneSettings.Bit1 = true;
        }
    }

    public void SetMusic(int index)
    {
        SceneSettings.MusicName = _musicSO.songs[index].ToString();
    }

    public void ConfirmStageMenu()
    {
        if (_optionsGroup.activeSelf)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void BackStageMenu()
    {
        if (_optionsGroup.activeSelf)
        {
            _optionsGroup.SetActive(false);
            _stagesGroup.SetActive(true);
            _startingSelectable.Select();
        }
        else
        {
            OpenMenuHideCurrent(_charactersSelectMenu);
        }
    }
}
