using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : BaseMenu
{
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private AudioMixer _audioMixer = default;
    private GameObject _previousSelectable;

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

    private void OnDisable()
    {
        _inputManager.SetPrompts(_inputManager.PreviousPrompts);
        EventSystem.current.SetSelectedGameObject(HotBarToggle.PreviousSelected.gameObject);
    }
}
