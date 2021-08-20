using UnityEngine;

public class Damager : MonoBehaviour, IHitboxResponder
{
	[SerializeField] private int _damage = 1;
	[SerializeField] private Vector2 _knockbackDirection = default;
	[SerializeField] private float _knockbackForce = default;
	[SerializeField] private UnityEngine.LayerMask _damageLayers = default;
	[SerializeField] private UnityEngine.LayerMask _impactLayers = default;
	[SerializeField] private bool _destroyOnImpact = default;
	[SerializeField] private GameObject _hitPrefab = default;
	private Audio _damagerAudio;
	private SpriteRenderer _spriteRenderer;
	private Hitbox _hitbox;
	private Rigidbody2D _rigidbody;

	void Awake()
	{
		_damagerAudio = GetComponent<Audio>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_hitbox = GetComponent<Hitbox>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public void HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null)
	{
		hurtbox.TakeDamage(_damage, _knockbackDirection, _knockbackForce);

		//if (hurtbox != null)
		//{
		//	if ((((1 << hit.collider.transform.root.gameObject.layer) & _damageLayers) != 0) || (((1 << hit.collider.gameObject.layer) & _damageLayers) != 0))
		//	{
		//		Vector2 hitEffectDirection = hit.transform.root.position - transform.root.position;
		//		if (hitEffectDirection.x > 0.0f)
		//		{
		//			hurtbox.TakeDamage(_damage, _knockbackDirection, _knockbackForce);
		//		}
		//		else
		//		{
		//			hurtbox.TakeDamage(_damage, new Vector2(_knockbackDirection.x * -1.0f, _knockbackDirection.y), _knockbackForce);
		//		}
		//	}
		//}
		//if ((((1 << hit.collider.transform.root.gameObject.layer) & _impactLayers) != 0) || (((1 << hit.collider.gameObject.layer) & _impactLayers) != 0))
		//{
		//	_hitbox._hasHit = true;
		//	Hit(hit);
		//}
	}

	private void Hit(RaycastHit2D hit)
	{
		_damagerAudio.Sound3D("Hit").Play();
		if (_destroyOnImpact)
		{
			_spriteRenderer.enabled = false;
			_hitbox.enabled = false;
		}
		//hitEffect.transform.up = hit.normal;
	}

	void OnEnable()
	{
		if (_destroyOnImpact)
		{
			_spriteRenderer.enabled = true;
			_hitbox.enabled = true;
			_rigidbody.velocity = Vector2.zero;
		}
	}
}