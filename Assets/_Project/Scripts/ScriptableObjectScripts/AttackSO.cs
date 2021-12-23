using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack", order = 2)]
public class AttackSO : ScriptableObject
{
	[Header("Main")]
	public float travelDistance;
	public float hitStun;
	public float knockback;
	public Vector2 knockbackDirection;
	public float selfKnockback;
	public float knockbackDuration;
	public AttackTypeEnum attackTypeEnum;
	[Range(0.0f, 1.0f)]
	public float hitstop;
	public bool isAirAttack;
	public bool causesKnockdown;
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