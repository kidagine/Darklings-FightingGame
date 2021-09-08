using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack", order = 2)]
public class AttackSO : ScriptableObject
{
	[Header("Main")]
	public float travelDistance;
	public float hitStun;
	public float knockback;
	public bool canGuardBreak;
	[Header("Sounds")]
	public string attackSound;
	public string impactSound;
	[Header("Effects")]
	public GameObject hitEffect;
	public Vector2 hitEffectPosition;
	public float hitEffectRotation;
	public GameObject hurtEffect;
	public Vector2 hurtEffectPosition;
	public float hurtEffectRotation;
}