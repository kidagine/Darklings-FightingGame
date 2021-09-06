using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack", order = 2)]
public class AttackSO : ScriptableObject
{
	public float travelDistance;
	public float hitStun;
	public float knockback;
	public string attackSound;
	public string impactSound;
	public GameObject hitEffect;
	public Vector2 hitEffectPosition;
	public float hitEfffectRotation;
}