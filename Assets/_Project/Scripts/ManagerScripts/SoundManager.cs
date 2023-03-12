using Demonics.Manager;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioMixer))]
[RequireComponent(typeof(Audio))]
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioMixer _audioMixer = default;
    [SerializeField] private Audio _audio = default;
    private float _cachedMasterVolume;
    private float lerpRatio = 0.0f;
    private float startLerpValue;
    private float endLerpValue;
    private bool isLerping;
    private string _currentlyPlayingMusic;


    void Start()
    {
        _cachedMasterVolume = GetMasterVolume();
    }

    void Update()
    {
        if (isLerping)
        {
            float value = Mathf.Lerp(startLerpValue, endLerpValue, lerpRatio);
            lerpRatio += 0.2f * Time.deltaTime;
            if (lerpRatio > 1.0f)
            {
                isLerping = false;
                SetMasterVolume(endLerpValue, false);
            }
            SetMasterVolume(value, false);
        }
    }

    public void SetMusic(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            _audio.Sound(name).Play();
            _currentlyPlayingMusic = name;
        }
        else
        {
            if (!string.IsNullOrEmpty(_currentlyPlayingMusic))
            {
                _audio.Sound(_currentlyPlayingMusic).Stop();
            }
        }
    }

    public void FadeInMasterVolume()
    {
        startLerpValue = 0.00001f;
        endLerpValue = 1.0f;
        lerpRatio = 0.0f;
        isLerping = true;
    }

    public void FadeOutMasterVolume()
    {
        startLerpValue = 1.0f;
        endLerpValue = 0.00001f;
        lerpRatio = 0.0f;
        isLerping = true;
    }

    public void SetMasterVolume(float value, bool isCaching = true)
    {
        if (isCaching)
        {
            _cachedMasterVolume = GetMasterVolume();
        }
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    private float GetMasterVolume()
    {
        _audioMixer.GetFloat("MasterVolume", out float masterVolume);
        return masterVolume;
    }

    public void SetMasterVolumeToCached()
    {
        SetMasterVolume(_cachedMasterVolume);
    }

    public void SetMusicVolume(float value)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void SetVFXVolume(float value)
    {
        _audioMixer.SetFloat("VFXVolume", Mathf.Log10(value) * 20);
    }

    public void SetUIVolume(float value)
    {
        _audioMixer.SetFloat("UIVolume", Mathf.Log10(value) * 20);
    }

    public void ToggleMusicVolume()
    {
        _audioMixer.GetFloat("MusicVolume", out float musicVolume);
        if (musicVolume <= 0.01f)
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(0.01f) * 20);
        }
        else
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(0.3f) * 20);
        }
    }
}