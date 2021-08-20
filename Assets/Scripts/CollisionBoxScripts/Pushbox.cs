#if UNITY_EDITOR
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Pushbox : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider = default;
    [SerializeField] private bool _showGizmo = true;
    private Color _pushboxColor = Color.blue;


	private void OnDrawGizmos()
    {
        if (_boxCollider.enabled && _showGizmo)
        {
            _pushboxColor.a = 0.6f;
            Gizmos.color = _pushboxColor;
            Vector2 pushboxPosition = new Vector2(transform.position.x + (_boxCollider.offset.x * transform.root.localScale.x), transform.position.y + (_boxCollider.offset.y * transform.root.localScale.y));
            Gizmos.matrix = Matrix4x4.TRS(pushboxPosition, transform.rotation, transform.localScale);

            Vector2 gizmoPosition = new Vector2(_boxCollider.size.x, _boxCollider.size.y);
            Gizmos.DrawCube(Vector3.zero, gizmoPosition);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
        }
    }
}
#endif