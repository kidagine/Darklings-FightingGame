using UnityEngine;

public class HitboxVisualizer : MonoBehaviour
{
	private Hitbox _hitbox;
	private SpriteRenderer _spriteRenderer;


	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_hitbox = transform.parent.GetComponent<Hitbox>();
	}

	void LateUpdate()
	{
		if (TrainingSettings.ShowHitboxes)
		{
			transform.localPosition = _hitbox._offset;
			_spriteRenderer.size = _hitbox._hitboxSize;
			_spriteRenderer.enabled = _hitbox.enabled;
		}
		else
		{
			_spriteRenderer.enabled = false;
		}
	}
}
