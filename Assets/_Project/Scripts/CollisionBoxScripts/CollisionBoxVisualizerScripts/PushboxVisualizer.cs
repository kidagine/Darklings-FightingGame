using UnityEngine;

public class PushboxVisualizer : MonoBehaviour
{
	private BoxCollider2D _boxCollider;
	private SpriteRenderer _spriteRenderer;
	private Pushbox _pushbox;


	void Awake()
	{
		_pushbox = transform.parent.GetComponent<Pushbox>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_boxCollider = transform.parent.GetComponent<BoxCollider2D>();
	}

	void LateUpdate()
	{
		if (TrainingSettings.ShowHitboxes)
		{
			transform.localPosition = _boxCollider.offset;
			_spriteRenderer.size = _boxCollider.size;
			_spriteRenderer.color = _pushbox.PushboxColor;
			_spriteRenderer.enabled = _boxCollider.enabled;
		}
		else
		{
			_spriteRenderer.enabled = false;
		}
	}
}

