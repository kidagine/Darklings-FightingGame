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
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [Header("ONLINE")]
    [SerializeField] private string _playerNameInputFieldInitial = "Demon";
    [SerializeField] private TMP_InputField _playerNameInputField = default;
    [SerializeField] private BaseSelector _characterSelector = default;
    [SerializeField] private int _characterSelectorInitial = default;
    [SerializeField] private BaseSelector _charactersAssistSelector = default;
    [SerializeField] private int _characterAssistSelectorInitial = default;
    [SerializeField] private BaseSelector _characterColorSelector = default;
    [SerializeField] private int _characterColorSelectorInitial = default;
    [SerializeField] private BaseSelector _stageOnlineSelector = default;
    [SerializeField] private int _stageOnlineInitial = default;
    [SerializeField] private BaseSelector _musicOnlineSelector = default;
    [SerializeField] private int _musicOnlineInitial = default;
    [SerializeField] private BaseSelector _stageStyleOnlineSelector = default;
    [SerializeField] private int _stageStyleOnlineInitial = default;
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
    [SerializeField] private BaseSelector _framedataMeterSelector = default;
    [SerializeField] private int framedataMeterSelectorInitial = default;
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
            _onlineSetupMenu.SetMusicSelectorValues();
            _onlineSetupMenu.SetStageSelectorValues();
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
            if (GameplayManager.Instance.IsTrainingMode)
            {
                LoadTrainingPreferences();
            }
        }
    }

    private void LoadOnlinePreferences()
    {
        _onlineSetupMenu.SetCharacter(int.Parse(DemonicsSaver.Load("character", _characterSelectorInitial.ToString())));
        _playerNameInputField.text = DemonicsSaver.Load("playerName", _playerNameInputFieldInitial.ToString());
        _charactersAssistSelector.SetValue(int.Parse(DemonicsSaver.Load("characterassist", _characterAssistSelectorInitial.ToString())));
        _characterColorSelector.SetValue(int.Parse(DemonicsSaver.Load("charactercolor", _characterColorSelectorInitial.ToString())));
    }

    private void LoadStagePreferences()
    {
        _stageOnlineSelector.SetValue(int.Parse(DemonicsSaver.Load("stageonline", _stageOnlineInitial.ToString())));
        _musicOnlineSelector.SetValue(int.Parse(DemonicsSaver.Load("stagemusiconline", _musicOnlineInitial.ToString())));
        _stageStyleOnlineSelector.SetValue(int.Parse(DemonicsSaver.Load("stagestyleonline", _stageStyleOnlineInitial.ToString())));

        _stageSelector.SetValue(int.Parse(DemonicsSaver.Load("stage", _stageSelectorInitial.ToString())));
        _stageMusicSelector.SetValue(int.Parse(DemonicsSaver.Load("stagemusic", _stageMusicSelectorInitial.ToString())));
        _stageStyleSelector.SetValue(int.Parse(DemonicsSaver.Load("stagestyle", _stageStyleSelectorInitial.ToString())));
    }


    private void LoadOptionPreferences()
    {
        _vfxSelector.SetValue(int.Parse(DemonicsSaver.Load("vfx", _vfxSelectorInitial.ToString())));
        _uiSelector.SetValue(int.Parse(DemonicsSaver.Load("ui", _uiSelectorInitial.ToString())));
        _musicSelector.SetValue(int.Parse(DemonicsSaver.Load("music", _musicSelectorInitial.ToString())));
    }

    private void LoadTrainingPreferences()
    {
        //General
        _healthSelector.SetValue(int.Parse(DemonicsSaver.Load("health", _healthSelectorInitial.ToString())));
        _arcanaSelector.SetValue(int.Parse(DemonicsSaver.Load("arcana", _arcanaSelectorInitial.ToString())));
        _assistSelector.SetValue(int.Parse(DemonicsSaver.Load("assist", _assistSelectorInitial.ToString())));

        //Cpu
        _cpuSelector.SetValue(int.Parse(DemonicsSaver.Load("cpu", _cpuSelectorInitial.ToString())));
        _blockSelector.SetValue(int.Parse(DemonicsSaver.Load("block", _blockSelectorInitial.ToString())));
        _onHitSelector.SetValue(int.Parse(DemonicsSaver.Load("onhit", _onHitSelectorInitial.ToString())));
        _stanceSelector.SetValue(int.Parse(DemonicsSaver.Load("stance", _stanceSelectorInitial.ToString())));

        //Misc
        _hitboxesSelector.SetValue(int.Parse(DemonicsSaver.Load("hitboxes", _hitboxesSelectorInitial.ToString())));
        _framedataSelector.SetValue(int.Parse(DemonicsSaver.Load("framedata", _framedataSelectorInitial.ToString())));
        _framedataMeterSelector.SetValue(int.Parse(DemonicsSaver.Load("framedata", framedataMeterSelectorInitial.ToString())));
        _slowdownSelector.SetValue(int.Parse(DemonicsSaver.Load("slowdown", _slowdownSelectorInitial.ToString())));
        _inputSelector.SetValue(int.Parse(DemonicsSaver.Load("input", _inputSelectorInitial.ToString())));
        _uiCanvasSelector.SetValue(int.Parse(DemonicsSaver.Load("uicanvas", _uiCanvasSelectorInitial.ToString())));
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
