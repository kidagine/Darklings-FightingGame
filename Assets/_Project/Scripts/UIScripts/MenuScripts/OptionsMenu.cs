using Demonics.UI;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : BaseMenu
{
    [SerializeField] private AudioMixer _audioMixer = default;


    public void SetVFX(int value)
    {
        float parsedValue = ((float)value / 10) + 0.00001f;
        _audioMixer.SetFloat("VFXVolume", Mathf.Log10(parsedValue) * 20);   
    }

    public void SetUI(int value)
    {
        float parsedValue = ((float)value / 10) + 0.00001f;
        _audioMixer.SetFloat("UIVolume", Mathf.Log10(parsedValue) * 20);
    }

    public void SetMusic(int value)
    {
        float parsedValue = ((float)value / 10) + 0.00001f;
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(parsedValue) * 20);
    }
}
