using UnityEngine;

public class CollisionVisualizer : MonoBehaviour
{
    private DemonicsCollider _collider;
    private SpriteRenderer _spriteRenderer;


    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = transform.parent.GetComponent<DemonicsCollider>();
    }

    void LateUpdate()
    {
        if (TrainingSettings.ShowHitboxes)
        {
            transform.position = new Vector2((float)_collider.Position.x, (float)_collider.Position.y);
            _spriteRenderer.size = new Vector2((float)_collider.Size.x, (float)_collider.Size.y);
            _spriteRenderer.color = _collider.GizmoColor;
            _spriteRenderer.enabled = _collider.enabled;
        }
        else
        {
            _spriteRenderer.enabled = false;
        }
    }
}

