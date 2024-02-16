using Demonics.Utility;
using System;
using UnityEngine;


[Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;
    public AudioClip clip;
    [Range(0.0f, 1.0f)]
    public float volume;
    [Range(0.0f, 3.0f)]
    public float pitch;
    public string name;
    public bool loop;
    public bool playOnAwake;
    public bool playOneInstanceAtATime;
    [Range(0, 100)]
    public int chance;


    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Pause()
    {
        source.Pause();
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }

    public async void FadeOut()
    {
        volume = 1;
        loop = false;

        bool isVolumeLowered = false;
        while (!isVolumeLowered)
        {
            source.volume -= 0.04f;
            if (source.volume <= 0.0f)
            {
                isVolumeLowered = true;
            }
            await UpdateTimer.WaitFor(0.05f);
        }
        source.Stop();
    }
}
