using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveInDirection : MonoBehaviour
{
	[SerializeField] private float _speed = 4.0f;
    private Rigidbody2D _rigidbody;


	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
		_rigidbody.velocity = transform.right * _speed;
	}
}
