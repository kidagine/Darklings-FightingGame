
using System;

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

	public void PlayInRandom()
	{
		Sound randomSound = sounds[UnityEngine.Random.Range(0, sounds.Length)];
		randomSound.source.Play();
	}
}