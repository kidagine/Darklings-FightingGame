using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Scriptable Objects/Player Stat", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
	[Header("Main")]
	public int characterIndex;
	public Sprite[] portraits;
	public SpriteLibraryAsset[] spriteLibraryAssets;
	public RuntimeAnimatorController runtimeAnimatorController;
	public DialogueSO _dialogue;
	public CharacterTypeEnum characterName;
	[Header("Stats")]
	public int defenseLevel;
	public int arcanaLevel;
	public int speedLevel;
	public int jumpForce = 2;
	public int dashForce = 5;
	public bool canDoubleJump = true;
	public float arcanaRecharge = 1;
	[Header("Moves")]
	public AttackSO m2L;
	public AttackSO m5L;
	public AttackSO m2M;
	public AttackSO m5M;
	public AttackSO m2H;
	public AttackSO m5H;
	public AttackSO jL;
	public AttackSO jM;
	public AttackSO mThrow;
	public AttackSO mParry;
	public ArcanaSO m5Arcana;
	public ArcanaSO m2Arcana;
	public ArcanaSO jArcana;
	[HideInInspector] public int maxHealth = 10000;

	public int Arcana { get { return arcanaLevel; } set { } }
	public float Defense { get { return (defenseLevel - 1) * 0.05f + 0.95f; } set { } }
	public int SpeedWalk { get { return (speedLevel - 1) * 1 + 2; } set { } }
	public int SpeedRun { get { return (speedLevel - 1) * 1+ 7; } set { } }

}
