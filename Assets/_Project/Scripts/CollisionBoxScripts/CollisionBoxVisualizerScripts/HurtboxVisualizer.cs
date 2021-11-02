using UnityEngine;

public class HurtboxVisualizer : MonoBehaviour
{
	private BoxCollider2D _boxCollider;
	private SpriteRenderer _spriteRenderer;


	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_boxCollider = transform.parent.GetComponent<BoxCollider2D>();
	}

	void LateUpdate()
	{
		if (TrainingSettings.ShowHitboxes)
		{
			transform.localPosition = _boxCollider.offset;
			transform.localScale = _boxCollider.size;
			_spriteRenderer.enabled = _boxCollider.enabled;
		}
		else
		{
			_spriteRenderer.enabled = false;
		}
	}
}
