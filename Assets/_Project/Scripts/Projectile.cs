using UnityEngine;

public class Projectile : MonoBehaviour, IHurtboxResponder
{
	[SerializeField] private Hitbox _hitbox = default;
	[SerializeField] private GameObject _dustPrefab = default;
	[SerializeField] private int _projectilePriority = default;
	[SerializeField] private float _speed = 4.0f;
	[SerializeField] private bool _isFixed = default;
	private Rigidbody2D _rigidbody;
	public int ProjectilePriority { get { return _projectilePriority; } private set { } }


	public Vector2 Direction { get; set; }
	public bool BlockingLow { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
	public bool BlockingHigh { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
	public bool BlockingMiddair { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_hitbox.OnCollision += () => gameObject.SetActive(false);
	}

	void Update()
	{
		if (_isFixed)
		{
			_rigidbody.velocity = (transform.right * transform.root.localScale.x) * _speed;
		}
		else
		{
			_rigidbody.velocity = Direction * _speed;
		}
	}

	void OnDisable()
	{
		if (_hitbox.HitPoint != null)
		{
			if (_hitbox.HitPoint.gameObject.layer == 6)
			{
				Instantiate(_dustPrefab, transform.position, Quaternion.identity);
			}
			_hitbox.OnCollision -= () => Destroy(gameObject);
		}
	}

	public bool TakeDamage(AttackSO attackSO)
	{
		return false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Projectile projectile))
		{
			if (projectile.ProjectilePriority > ProjectilePriority)
			{
				gameObject.SetActive(false);
				Instantiate(_dustPrefab, transform.position, Quaternion.identity);
			}
			else if (projectile.ProjectilePriority == ProjectilePriority)
			{
				gameObject.SetActive(false);
				projectile.gameObject.SetActive(false);
				Instantiate(_dustPrefab, transform.position, Quaternion.identity);
			}
		}
	}
}
