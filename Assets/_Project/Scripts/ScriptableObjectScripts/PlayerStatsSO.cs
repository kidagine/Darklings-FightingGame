using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Scriptable Objects/Player Stat", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
	[Header("Main")]
	public Sprite[] portraits;
	public string characterName;
	public float maxHealth = 3;
	[Header("Movement")]
	public float walkSpeed = 3;
	public float runSpeed = 5;
	public float jumpForce = 2;
	public float dashForce = 5;
	[Header("Arcana")]
	public float maxArcana = 2;
	public float arcanaRecharge = 1;
}
