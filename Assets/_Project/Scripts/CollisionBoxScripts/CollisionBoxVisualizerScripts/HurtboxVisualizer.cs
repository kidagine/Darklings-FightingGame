using UnityEngine;

public class HurtboxVisualizer : MonoBehaviour
{
	private BoxCollider2D _boxCollider;
	private SpriteRenderer _spriteRenderer;
	private Hurtbox _hurtbox;


	void Awake()
	{
		_hurtbox = transform.parent.GetComponent<Hurtbox>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_boxCollider = transform.parent.GetComponent<BoxCollider2D>();
	}

	void LateUpdate()
	{
		if (TrainingSettings.ShowHitboxes)
		{
			transform.localPosition = _boxCollider.offset;
			_spriteRenderer.size = _boxCollider.size;
			_spriteRenderer.color = _hurtbox.HurtboxColor;
			_spriteRenderer.enabled = _boxCollider.enabled;
		}
		else
		{
			_spriteRenderer.enabled = false;
		}
	}
}
