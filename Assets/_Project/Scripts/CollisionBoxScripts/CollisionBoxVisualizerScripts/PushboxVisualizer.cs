using UnityEngine;

public class PushboxVisualizer : MonoBehaviour
{
	private DemonicsCollider _collider;
	private SpriteRenderer _spriteRenderer;


	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_collider = transform.parent.GetComponent<DemonicsCollider>();
	}

	void FixedUpdate()
	{
		if (TrainingSettings.ShowHitboxes)
		{
			transform.position = new Vector2((float)_collider.PositionX, (float)_collider.PositionY);
			_spriteRenderer.size = new Vector2((float)_collider.SizeX, (float)_collider.SizeY);
			_spriteRenderer.color = _collider.GizmoColor;
			_spriteRenderer.enabled = _collider.enabled;
		}
		else
		{
			_spriteRenderer.enabled = false;
		}
	}
}

