using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Hurtbox : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider = default;
    [SerializeField] private bool _damageable = true;
    [SerializeField] private GameObject _hurtboxResponderObject = default;
    private Color _hitboxDamageableColor = Color.green;
    private Color _hitboxNonDamageableColor = Color.green;
    private IHurtboxResponder _hurtboxResponder;


    void Awake()
	{
        if (_damageable)
        {
            _hurtboxResponder = _hurtboxResponderObject.GetComponent<IHurtboxResponder>();
        }
    }

    public void TakeDamage(int damage, float hitStun = 0, Vector2 knockbackDirection = default, float knockbackForce = default)
    {
        if (_damageable)
        {
            _hurtboxResponder.TakeDamage(damage, hitStun, knockbackDirection, knockbackForce);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_boxCollider.enabled)
        {
            if (_damageable)
            {
                _hitboxDamageableColor.a = 0.35f;
                Gizmos.color = _hitboxDamageableColor;
            }
            else
            {
                _hitboxNonDamageableColor.a = 0.4f;
                Gizmos.color = _hitboxNonDamageableColor;
            }
            Vector2 hurtboxPosition = new Vector2(transform.position.x + (_boxCollider.offset.x * transform.root.localScale.x), transform.position.y + (_boxCollider.offset.y * transform.root.localScale.y));
            Gizmos.matrix = Matrix4x4.TRS(hurtboxPosition, transform.rotation, transform.localScale);

            Vector2 gizmoPosition = new Vector2(_boxCollider.size.x, _boxCollider.size.y);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
        }
    }
#endif
}
