using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeStageMenu : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private RebindMenu _rebindOneMenu = default;
    [SerializeField] private RebindMenu _rebindTwoMenu = default;
    [SerializeField] private Selectable _initialSelectable = default;
    [SerializeField] private GameObject _selectorTextPrefab = default;
    [SerializeField] private BaseSelector _stageSelector = default;
    [SerializeField] private BaseSelector _musicSelector = default;
    [SerializeField] private GameObject _backgroundDarken = default;
    [SerializeField] private GameObject _changeStagePrompts = default;
    [SerializeField] private TextMeshProUGUI _musicText = default;
    [SerializeField] private TextMeshProUGUI _styleText = default;
    [SerializeField] private Image _stageImage = default;
    [SerializeField] private MusicSO _musicSO = default;
    [SerializeField] private StageSO[] _stagesSO = default;
    private StageSO _currentStage;
    private Selectable _previousSelectable;
    private Animator _changeStageAnimator;
    private EventSystem _currentEventSystem;
    private bool _isCharacterSelectDeactivated;
    public bool IsOpen { get; private set; }
    public PromptsInput PreviousPromptsInput { get; set; }


    void Awake()
    {
        _changeStageAnimator = GetComponent<Animator>();
        _currentEventSystem = EventSystem.current;
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

    public void ChangeStageOpen()
    {
        if (!IsOpen && !_rebindOneMenu.gameObject.activeSelf && !_rebindTwoMenu.gameObject.activeSelf)
        {
            _isCharacterSelectDeactivated = !_currentEventSystem.sendNavigationEvents;
            _backgroundDarken.SetActive(true);
            PreviousPromptsInput = _inputManager.CurrentPrompts;
            _previousSelectable = _currentEventSystem.currentSelectedGameObject.GetComponent<Selectable>();
            _changeStagePrompts.SetActive(true);
            _changeStageAnimator.Play("ChangeStageOpen");
            IsOpen = true;
        }
    }

    public void ChangeStageOpenFinished()
    {
        _currentEventSystem.sendNavigationEvents = true;
        _initialSelectable.Select();
    }

    public void ChangeStageClose()
    {
        if (IsOpen)
        {
            _changeStagePrompts.SetActive(false);
            _backgroundDarken.SetActive(false);
            _currentEventSystem.currentSelectedGameObject.GetComponent<Animator>().Rebind();
            _previousSelectable.Select();
            _changeStageAnimator.Play("ChangeStageClose");
            if (_isCharacterSelectDeactivated)
            {
                _currentEventSystem.sendNavigationEvents = false;
            }
            _inputManager.SetPrompts(PreviousPromptsInput);
            IsOpen = false;
        }
    }

    public void SetStage(int index)
    {
        _currentStage = _stagesSO[index];
        if (SceneSettings.Bit1)
        {
            _stageImage.sprite = _stagesSO[index].bit1Stage;
        }
        else
        {
            _stageImage.sprite = _stagesSO[index].colorStage;
        }
        if (index == 0)
        {
            SceneSettings.RandomStage = true;
        }
        else
        {
            SceneSettings.RandomStage = false;
            SceneSettings.StageIndex = index - 1;
        }
    }

    public void Set1Bit(int index)
    {
        if (index == 0)
        {
            _styleText.text = "Normal";
            _stageImage.sprite = _currentStage.colorStage;
            SceneSettings.Bit1 = false;
        }
        else if (index == 1)
        {
            _styleText.text = "1 Bit";
            _stageImage.sprite = _currentStage.bit1Stage;
            SceneSettings.Bit1 = true;
        }
    }

    public void SetMusic(int index)
    {
        _musicText.text = _musicSO.songs[index].ToString();
        SceneSettings.MusicName = _musicSO.songs[index].ToString();
    }

    public void SetTrainingMode(bool state)
    {
        SceneSettings.IsTrainingMode = state;
    }

    void OnDisable()
    {
        ChangeStageClose();
    }
}
