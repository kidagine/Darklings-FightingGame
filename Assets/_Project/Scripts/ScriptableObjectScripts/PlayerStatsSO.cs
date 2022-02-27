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
	public string characterName;
	public float maxHealth = 3;
	[Header("Movement")]
	public float walkSpeed = 3;
	public float runSpeed = 5;
	public float jumpForce = 2;
	public float dashForce = 5;
	public bool canDoubleJump = true;
	[Header("Arcana")]
	public float maxArcana = 2;
	public float arcanaRecharge = 1;
	[Header("Moves")]
	public AttackSO m2L;
	public AttackSO m5L;
	public AttackSO m2M;
	public AttackSO m5M;
	public AttackSO m2H;
	public AttackSO m5H;
	public AttackSO jumpL;
	public ArcanaSO arcana;
}
