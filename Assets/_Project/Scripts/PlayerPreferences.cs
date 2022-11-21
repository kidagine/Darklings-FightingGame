using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
    [SerializeField] private bool _checkOnline = default;
    [SerializeField] private bool _checkStage = default;
    [SerializeField] private bool _checkOptions = default;
    [SerializeField] private bool _checkTraining = default;
    [SerializeField] private ChangeStageMenu _changeStageMenu = default;
    [Header("ONLINE")]
    [SerializeField] private string _playerNameInputFieldInitial = "Demon";
    [SerializeField] private TMP_InputField _playerNameInputField = default;
    [SerializeField] private BaseSelector _characterSelector = default;
    [SerializeField] private int _characterSelectorInitial = default;
    [SerializeField] private BaseSelector _charactersAssistSelector = default;
    [SerializeField] private int _characterAssistSelectorInitial = default;
    [SerializeField] private BaseSelector _characterColorSelector = default;
    [SerializeField] private int _characterColorSelectorInitial = default;
    [Header("STAGE")]
    [SerializeField] private BaseSelector _stageSelector = default;
    [SerializeField] private int _stageSelectorInitial = default;
    [SerializeField] private BaseSelector _stageMusicSelector = default;
    [SerializeField] private int _stageMusicSelectorInitial = default;
    [SerializeField] private BaseSelector _stageStyleSelector = default;
    [SerializeField] private int _stageStyleSelectorInitial = default;
    [Header("OPTIONS")]
    [Header("Audio")]
    [SerializeField] private BaseSelector _vfxSelector = default;
    [Range(0, 10)]
    [SerializeField] private int _vfxSelectorInitial = default;
    [SerializeField] private BaseSelector _uiSelector = default;
    [Range(0, 10)]
    [SerializeField] private int _uiSelectorInitial = default;
    [SerializeField] private BaseSelector _musicSelector = default;
    [Range(0, 10)]
    [SerializeField] private int _musicSelectorInitial = default;
    [Header("TRAINING")]
    [Header("General")]
    [SerializeField] private BaseSelector _healthSelector = default;
    [SerializeField] private int _healthSelectorInitial = default;
    [SerializeField] private BaseSelector _arcanaSelector = default;
    [SerializeField] private int _arcanaSelectorInitial = default;
    [SerializeField] private BaseSelector _assistSelector = default;
    [SerializeField] private int _assistSelectorInitial = default;
    [Header("CPU")]
    [SerializeField] private BaseSelector _cpuSelector = default;
    [SerializeField] private int _cpuSelectorInitial = default;
    [SerializeField] private BaseSelector _blockSelector = default;
    [SerializeField] private int _blockSelectorInitial = default;
    [SerializeField] private BaseSelector _onHitSelector = default;
    [SerializeField] private int _onHitSelectorInitial = default;
    [SerializeField] private BaseSelector _stanceSelector = default;
    [SerializeField] private int _stanceSelectorInitial = default;
    [Header("Misc")]
    [SerializeField] private BaseSelector _hitboxesSelector = default;
    [SerializeField] private int _hitboxesSelectorInitial = default;
    [SerializeField] private BaseSelector _framedataSelector = default;
    [SerializeField] private int _framedataSelectorInitial = default;
    [SerializeField] private BaseSelector _slowdownSelector = default;
    [SerializeField] private int _slowdownSelectorInitial = default;
    [SerializeField] private BaseSelector _inputSelector = default;
    [SerializeField] private int _inputSelectorInitial = default;
    [SerializeField] private BaseSelector _uiCanvasSelector = default;
    [SerializeField] private int _uiCanvasSelectorInitial = default;


    void Start()
    {
        if (_checkOnline)
        {
            LoadOnlinePreferences();
        }
        if (_checkStage)
        {
            _changeStageMenu.SetMusicSelectorValues();
            _changeStageMenu.SetStageSelectorValues();
            LoadStagePreferences();
        }
        if (_checkOptions)
        {
            LoadOptionPreferences();
        }
        if (_checkTraining)
        {
            if (GameManager.Instance.IsTrainingMode)
            {
                LoadTrainingPreferences();
            }
        }
    }

    private void LoadOnlinePreferences()
    {
        // _playerNameInputField.text = PlayerPrefs.GetString("playerName", _playerNameInputFieldInitial);
        _characterSelector.SetValue(DemonicsSaver.LoadInt("character", _characterSelectorInitial));
        _charactersAssistSelector.SetValue(DemonicsSaver.LoadInt("characterassist", _characterAssistSelectorInitial));
        _characterColorSelector.SetValue(DemonicsSaver.LoadInt("charactercolor", _characterColorSelectorInitial));
    }

    private void LoadStagePreferences()
    {
        _stageSelector.SetValue(DemonicsSaver.LoadInt("stage", _stageSelectorInitial));
        _stageMusicSelector.SetValue(DemonicsSaver.LoadInt("stagemusic", _stageMusicSelectorInitial));
        _stageStyleSelector.SetValue(DemonicsSaver.LoadInt("stagestyle", _stageStyleSelectorInitial));
    }


    private void LoadOptionPreferences()
    {
        _vfxSelector.SetValue(DemonicsSaver.LoadInt("vfx", _vfxSelectorInitial));
        _uiSelector.SetValue(DemonicsSaver.LoadInt("ui", _uiSelectorInitial));
        _musicSelector.SetValue(DemonicsSaver.LoadInt("music", _musicSelectorInitial));
    }

    private void LoadTrainingPreferences()
    {
        //General
        _healthSelector.SetValue(DemonicsSaver.LoadInt("health", _healthSelectorInitial));
        _arcanaSelector.SetValue(DemonicsSaver.LoadInt("arcana", _arcanaSelectorInitial));
        _assistSelector.SetValue(DemonicsSaver.LoadInt("assist", _assistSelectorInitial));

        //Cpu
        _cpuSelector.SetValue(DemonicsSaver.LoadInt("cpu", _cpuSelectorInitial));
        _blockSelector.SetValue(DemonicsSaver.LoadInt("block", _blockSelectorInitial));
        _onHitSelector.SetValue(DemonicsSaver.LoadInt("onhit", _onHitSelectorInitial));
        _stanceSelector.SetValue(DemonicsSaver.LoadInt("stance", _stanceSelectorInitial));

        //Misc
        _hitboxesSelector.SetValue(DemonicsSaver.LoadInt("hitboxes", _hitboxesSelectorInitial));
        _framedataSelector.SetValue(DemonicsSaver.LoadInt("framedata", _framedataSelectorInitial));
        _slowdownSelector.SetValue(DemonicsSaver.LoadInt("slowdown", _slowdownSelectorInitial));
        _inputSelector.SetValue(DemonicsSaver.LoadInt("input", _inputSelectorInitial));
        _uiCanvasSelector.SetValue(DemonicsSaver.LoadInt("uicanvas", _uiCanvasSelectorInitial));
    }

    public void SavePreference(string key, int value)
    {
        DemonicsSaver.Save(key, value.ToString());
    }

    public void RestoreToDefault()
    {
        PlayerPrefs.DeleteAll();
        LoadTrainingPreferences();
    }
}
