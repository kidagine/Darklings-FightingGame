using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioMixer _audioMixer = default;
    private bool _isMusicOn = true;


	public void ToggleMusic()
    {
        if (_isMusicOn)
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(0.01f) * 20);
        }
        else
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(0.3f) * 20);
        }
        _isMusicOn = !_isMusicOn;
    }
}
