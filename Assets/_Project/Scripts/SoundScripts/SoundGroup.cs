
using System;
using System.Linq;


[Serializable]
public class SoundGroup
{
    public string name;
    public int lastPlayedSoundIndex;
    public Sound[] sounds;


    public void PlayInOrder()
    {
        int index = lastPlayedSoundIndex;
        sounds[index].source.Play();
        lastPlayedSoundIndex++;
        if (lastPlayedSoundIndex >= sounds.Length)
        {
            lastPlayedSoundIndex = 0;
        }
    }

    public Sound PlayInRandomChance()
    {
        Sound randomSound = null;
        int chance = UnityEngine.Random.Range(0, 100);
        int cumulative = 0;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i] != null)
            {
                cumulative += sounds[i].chance;
                if (chance < cumulative)
                {
                    randomSound = sounds[i];
                    break;
                }
            }
        }
        if (randomSound == null)
            return null;
        randomSound.source.Play();
        return randomSound;
    }

    public Sound PlayInRandom()
    {
        Sound randomSound = sounds[UnityEngine.Random.Range(0, sounds.Length)];
        randomSound.source.Play();
        return randomSound;
    }

    public Sound Sound(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        return sound;
    }
}
