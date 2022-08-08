using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
	[SerializeField] private Hitbox _hitbox = default;
	[SerializeField] private GameObject _dustPrefab = default;

	void Start()
	{
		_hitbox.OnGroundCollision += ()=> Destroy(gameObject);
	}

	void OnDestroy()
	{
		if (_hitbox.HitPoint != null)
		{
			if (_hitbox.HitPoint.gameObject.layer == 6)
			{
				Instantiate(_dustPrefab, transform.position, Quaternion.identity);
			}
		}
		_hitbox.OnGroundCollision -= ()=> Destroy(gameObject);
	}
}
