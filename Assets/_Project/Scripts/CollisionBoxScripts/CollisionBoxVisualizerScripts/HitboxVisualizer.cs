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

    void FixedUpdate()
    {
        if (TrainingSettings.ShowHitboxes)
        {
            transform.localPosition = new Vector2((float)_hitbox.Offset.x, (float)_hitbox.Offset.y);
            _spriteRenderer.size = new Vector2((float)_hitbox.Size.x, (float)_hitbox.Size.y);
            _spriteRenderer.enabled = _hitbox.enabled;
        }
        else
        {
            _spriteRenderer.enabled = false;
        }
    }
}
