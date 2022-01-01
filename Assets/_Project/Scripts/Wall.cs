using UnityEngine;

public class Wall : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.root.TryGetComponent(out PlayerMovement playerMovement))
		{
			playerMovement.IsInCorner = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.transform.root.TryGetComponent(out PlayerMovement playerMovement))
		{
			playerMovement.IsInCorner = false;
		}
	}
}
