using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveInDirection : MonoBehaviour
{
	[SerializeField] private Vector2 _direction = default;
	[SerializeField] private float _speed = 4.0f;
    private Rigidbody2D _rigidbody;


	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
		_rigidbody.velocity = new Vector2(transform.root.localScale.x, 0.0f) * _speed;
	}
}
