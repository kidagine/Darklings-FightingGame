using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Arcana", menuName = "Scriptable Objects/Arcana", order = 2)]
public class ArcanaSO : AttackSO
{
	[Header("Arcana")]
	public bool airOk;
	public bool reversal;
	[Header("Arcana Information")]
	public string arcanaName;
	[TextArea(5,7)]
	public string arcanaDescription;
	public VideoClip arcanaVideo;
}
