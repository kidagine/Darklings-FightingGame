using SharedGame;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : BaseMenu
{
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private AudioMixer _audioMixer = default;
    [SerializeField] private PauseMenu _pauseMenu = default;
    [SerializeField] private PauseMenu _pauseTrainingMenu = default;
    public PauseMenu CurrentPauseMenu { get; private set; }
    private readonly Vector2Int[] _resolutions =
    { new(1920, 1080), new(1366, 768), new(1280, 720), new(1024,768), new(800,600), new(640,480)  };

    public void SetVFX(int value)
    {
        float parsedValue = ((float)value / 100) + 0.00001f;
        _audioMixer.SetFloat("VFXVolume", Mathf.Log10(parsedValue) * 20);
    }

    public void SetUI(int value)
    {
        float parsedValue = ((float)value / 100) + 0.00001f;
        _audioMixer.SetFloat("UIVolume", Mathf.Log10(parsedValue) * 20);
    }

    public void SetMusic(int value)
    {
        float parsedValue = ((float)value / 100) + 0.00001f;
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(parsedValue) * 20);
    }

    public void SetResolution(int value)
    {
        Screen.SetResolution(_resolutions[value].x, _resolutions[value].y, Screen.fullScreenMode);
    }

    public void SetDisplay(int value)
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, (FullScreenMode)value + 1);
    }

    private void OnDisable()
    {
        if (PreviousSelectable != null)
            PreviousSelectable.Select();
        if (_inputManager != null)
            _inputManager.SetPrompts(_inputManager.PreviousPrompts);
    }

    public void Back()
    {
        CurrentPauseMenu.Show();
        Hide();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (GameManager.Instance == null)
            return;
        if (GameplayManager.Instance.IsTrainingMode)
            CurrentPauseMenu = _pauseTrainingMenu;
        else
            CurrentPauseMenu = _pauseMenu;
    }
}
