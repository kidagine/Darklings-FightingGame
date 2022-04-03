using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "Scriptable Objects/Music", order = 1)]
public class MusicSO : ScriptableObject
{
	public MusicTypeEnum[] songs;
}
