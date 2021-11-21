using UnityEngine;

[CreateAssetMenu(fileName = "Assist Stats", menuName = "Scriptable Objects/Assist Stat", order = 1)]
public class AssistStatsSO : ScriptableObject
{
	[Header("Main")]
	public float assistRecharge = 1;
}
