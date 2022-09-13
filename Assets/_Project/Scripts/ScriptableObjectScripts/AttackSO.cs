using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack", order = 2)]
public class AttackSO : ScriptableObject
{
	[Header("Main")]
	public float travelDistance;
	public Vector2 travelDirection;
	public int hitStun;
	[Range(0.0f, 16.0f)]
	public float knockback;
	public float bounce;
	public Vector2 knockbackDirection;
	[Range(0.0f, 1.0f)]
	public float knockbackDuration;
	public AttackTypeEnum attackTypeEnum;
	[Range(0, 50)]
	public int hitstop;
	public int damage;
	public bool isAirAttack;
	public bool isProjectile;
	public bool isArcana;
	public bool causesKnockdown;
	[Header("Sounds")]
	public string attackSound;
	public string impactSound;
	[Header("Effects")]
	public GameObject hitEffect;
	public Vector2 hitEffectPosition;
	public float hitEffectRotation;
	public GameObject hurtEffect;
	[HideInInspector] public Vector2 hurtEffectPosition;
	public float hurtEffectRotation;
	public CameraShakerSO cameraShaker;
	[Header("Framedata")]
	public int startUpFrames;
	public int activeFrames;
	public int recoveryFrames;
}