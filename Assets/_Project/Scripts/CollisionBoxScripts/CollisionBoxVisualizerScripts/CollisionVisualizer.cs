using UnityEngine;

public class CollisionVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer = default;


    public void ShowBox(ColliderNetwork collider)
    {
        if (TrainingSettings.ShowHitboxes)
        {
            Vector2 fixedPosition = new Vector2Int(Mathf.FloorToInt((float)collider.position.x * 1) / 1, Mathf.FloorToInt((float)collider.position.y * 1) / 1);
            transform.position = fixedPosition;
            _spriteRenderer.size = new Vector2((float)collider.size.x, (float)collider.size.y);
            _spriteRenderer.enabled = collider.active;
        }
        else
        {
            _spriteRenderer.enabled = false;
        }
    }
}

