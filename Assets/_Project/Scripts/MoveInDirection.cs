using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveInDirection : MonoBehaviour
{
	[SerializeField] private float _speed = 4.0f;
	[SerializeField] private bool _isFixed = default;
	private Rigidbody2D _rigidbody;

	public Vector2 Direction { get; set; }


	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
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
}
