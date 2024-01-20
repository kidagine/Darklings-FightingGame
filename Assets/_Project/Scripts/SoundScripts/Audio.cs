using Demonics.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Audio : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup = default;
    [SerializeField] private Sound[] _sounds = default;
    [SerializeField] private Sound3D[] _sounds3D = default;
    [SerializeField] private List<SoundGroup> _soundGroups = default;
    [SerializeField] private SoundGroup3D[] _soundGroups3D = default;


    void Awake()
    {
        SetSounds();
        SetSounds3D();
        SetSoundGroups();
        SetSoundGroups3D();
    }

    private void SetSounds()
    {
        foreach (Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.outputAudioMixerGroup = _audioMixerGroup;
            if (sound.source.playOnAwake)
            {
                sound.source.Play();
            }
        }
    }

    private void SetSounds3D()
    {
        foreach (Sound3D sound3D in _sounds3D)
        {
            sound3D.source = gameObject.AddComponent<AudioSource>();
            sound3D.source.clip = sound3D.clip;

            sound3D.source.volume = sound3D.volume;
            sound3D.source.pitch = sound3D.pitch;
            sound3D.source.loop = sound3D.loop;
            sound3D.source.playOnAwake = sound3D.playOnAwake;
            sound3D.source.outputAudioMixerGroup = _audioMixerGroup;
            sound3D.source.spatialBlend = 1.0f;
            sound3D.source.rolloffMode = AudioRolloffMode.Linear;
            sound3D.source.maxDistance = sound3D.maxDistance;
            sound3D.source.minDistance = sound3D.minDistance;
            if (sound3D.source.playOnAwake)
            {
                sound3D.source.Play();
            }
        }
    }

    private void SetSoundGroups()
    {
        foreach (SoundGroup soundGroup in _soundGroups)
        {
            foreach (Sound sound in soundGroup.sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;

                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.playOnAwake = sound.playOnAwake;
                sound.source.outputAudioMixerGroup = _audioMixerGroup;
                if (sound.source.playOnAwake)
                {
                    sound.source.Play();
                }
            }
        }
    }

    private void SetSoundGroups3D()
    {
        foreach (SoundGroup3D soundGroup3D in _soundGroups3D)
        {
            foreach (Sound3D sound3D in soundGroup3D.sounds)
            {
                sound3D.source = gameObject.AddComponent<AudioSource>();
                sound3D.source.clip = sound3D.clip;

                sound3D.source.volume = sound3D.volume;
                sound3D.source.pitch = sound3D.pitch;
                sound3D.source.loop = sound3D.loop;
                sound3D.source.playOnAwake = sound3D.playOnAwake;
                sound3D.source.outputAudioMixerGroup = _audioMixerGroup;
                if (sound3D.source.playOnAwake)
                {
                    sound3D.source.Play();
                }
            }
        }
    }

    public void AddSoundGroup(SoundGroup soundGroup)
    {

        foreach (Sound sound in soundGroup.sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.outputAudioMixerGroup = _audioMixerGroup;
        }
        _soundGroups.Add(soundGroup);
    }

    public Sound Sound(string name)
    {
        Sound sound = Array.Find(_sounds, s => s.name == name);
        return sound;
    }

    public Sound3D Sound3D(string name)
    {
        Sound3D sound3D = Array.Find(_sounds3D, s => s.name == name);
        return sound3D;
    }

    public SoundGroup SoundGroup(string name)
    {
        SoundGroup soundGroup = Array.Find<SoundGroup>(_soundGroups.ToArray(), s => s.name == name);
        return soundGroup;
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(_sounds, s => s.name == name);
        sound.source.Play();
    }

    public void Play3DSound(string name)
    {
        Sound3D sound3D = Array.Find(_sounds3D, s => s.name == name);
        sound3D.source.Play();
    }

    public async void FadeOutNow(string name)
    {
        Sound sound = Array.Find(_sounds, s => s.name == name);

        bool isVolumeLowered = false;
        while (!isVolumeLowered)
        {
            sound.source.volume -= 0.04f;
            if (sound.source.volume <= 0.0f)
            {
                isVolumeLowered = true;
            }
            await UpdateTimer.WaitFor(0.05f);
        }
        sound.source.Stop();
    }

    public async void FadeOutHalfNow(string name)
    {
        Sound sound = Array.Find(_sounds, s => s.name == name);
        float halfVolume = sound.source.volume / 2;
        bool isVolumeLowered = false;
        while (!isVolumeLowered)
        {
            sound.source.volume -= 0.04f;
            if (sound.source.volume <= halfVolume)
            {
                isVolumeLowered = true;
            }
            await UpdateTimer.WaitFor(0.05f);
        }
    }

    public async void FadeInNow(string name)
    {
        Sound sound = Array.Find(_sounds, s => s.name == name);
        sound.source.Play();

        bool isVolumeIncreased = false;
        while (!isVolumeIncreased)
        {
            sound.source.volume += 0.04f;
            if (sound.source.volume >= 1.0f)
            {
                isVolumeIncreased = true;
            }
            await UpdateTimer.WaitFor(0.05f);
        }
    }
}
