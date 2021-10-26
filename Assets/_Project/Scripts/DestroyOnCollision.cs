using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
	[SerializeField] private Hitbox _hitbox = default;


	void Start()
	{
		_hitbox.OnCollision += ()=> Destroy(gameObject);
	}

	void OnDestroy()
	{
		_hitbox.OnCollision -= ()=> Destroy(gameObject);
	}
}
