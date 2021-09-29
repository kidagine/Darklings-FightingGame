using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Scriptable Objects/Player Stat", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
	public Sprite portrait;
	public string name;
	public float walkSpeed = 3;
	public float runSpeed = 5;
	public float jumpForce = 2;
	public float maxHealth = 3;
	public float currentHealth = 3;
	public float dashForce = 5;
}
