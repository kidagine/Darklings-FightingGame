using UnityEngine;

public class Shadowbreak : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.root.TryGetComponent(out PlayerMovement playerMovement))
		{
			playerMovement.ShadowbreakKnockback();
		}
	}
}
