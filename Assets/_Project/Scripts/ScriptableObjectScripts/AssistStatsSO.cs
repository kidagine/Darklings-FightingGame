using UnityEngine;

[CreateAssetMenu(fileName = "Assist Stats", menuName = "Scriptable Objects/Assist Stat", order = 1)]
public class AssistStatsSO : ScriptableObject
{
	[Header("Main")]
	public float assistRecharge = 1.0f;
	public float assistRotation = 0.0f;
	public Vector2 assistPosition = Vector2.zero;
	public AttackSO attackSO = default;
}
