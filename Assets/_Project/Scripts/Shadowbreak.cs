using UnityEngine;

public class Shadowbreak : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.name);
		if (collision.transform.root.TryGetComponent(out Player player))
		{
			player.PlayerStateManager.TryToKnockbackState();
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.collider.name);
	}
}
