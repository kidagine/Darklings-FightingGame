using UnityEngine;

public class CollisionVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer = default;


    public void ShowBox(ColliderNetwork collider)
    {
        if (TrainingSettings.ShowHitboxes)
        {
            transform.position = new Vector2((float)collider.position.x, (float)collider.position.y);
            _spriteRenderer.size = new Vector2((float)collider.size.x, (float)collider.size.y);
            _spriteRenderer.enabled = collider.active;
        }
        else
        {
            _spriteRenderer.enabled = false;
        }
    }
}

