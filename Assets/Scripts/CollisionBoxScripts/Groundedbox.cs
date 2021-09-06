using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Groundedbox : MonoBehaviour
{
	[SerializeField] private GameObject _groundedBoxResponder = default;
	private BoxCollider2D _boxCollider;
	private IPushboxResponder _pushboxResponder;


	void Start()
	{
		if (_groundedBoxResponder.TryGetComponent(out IPushboxResponder pushboxResponder))
		{
			_pushboxResponder = pushboxResponder;
		}
		_boxCollider = GetComponent<BoxCollider2D>();
	}

	void FixedUpdate()
	{
		RaycastHit2D raycastHit2D = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), _boxCollider.bounds.size, 0f, Vector2.zero, .1f, LayerProvider.GetLayerMask(LayerMaskEnum.Ground));
		if (raycastHit2D.collider != null)
		{
			_pushboxResponder.OnGrounded();
		}
		else
		{
			_pushboxResponder.OnAir();
		}
	}
}
