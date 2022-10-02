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
            transform.localPosition = new Vector2(_hitbox.Offset.x, _hitbox.Offset.y);
            _spriteRenderer.size = _hitbox.Size;
            _spriteRenderer.enabled = _hitbox.enabled;
        }
        else
        {
            _spriteRenderer.enabled = false;
        }
    }
}
