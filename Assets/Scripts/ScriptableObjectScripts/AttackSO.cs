using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack", order = 2)]
public class AttackSO : ScriptableObject
{
	public float travelDistance;
	public float hitStun;
	public string attackSound;
	public string impactSound;
}