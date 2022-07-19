using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Hurtbox : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider = default;
    [SerializeField] private GameObject _hurtboxResponderObject = default;
    private IHurtboxResponder _hurtboxResponder;

	public Color HurtboxColor { get; private set; } = Color.green;


    void Awake()
	{
        _hurtboxResponder = _hurtboxResponderObject.GetComponent<IHurtboxResponder>();
    }

	private void Update()
	{
        if (_hurtboxResponder.BlockingLow)
        {
            HurtboxColor = new Color(0.0f, 0.45f, 0.45f, 1.0f);
        }
        else if (_hurtboxResponder.BlockingHigh)
        {
            HurtboxColor = Color.cyan;
        }
        else if (_hurtboxResponder.BlockingMiddair)
        {
            HurtboxColor = new Color(0.65f, 0.18f, 0.18f, 1.0f);
        }
        else
        {
            HurtboxColor = Color.green;
        }
    }

	public bool TakeDamage(AttackSO attackSO)
    {
        return _hurtboxResponder.TakeDamage(attackSO);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_boxCollider.enabled)
        {
            Color color = HurtboxColor;
            color.a = 0.35f;
            Gizmos.color = color;
            Vector2 hurtboxPosition = new Vector2(transform.position.x + (_boxCollider.offset.x * transform.root.localScale.x), transform.position.y + (_boxCollider.offset.y * transform.root.localScale.y));
            Gizmos.matrix = Matrix4x4.TRS(hurtboxPosition, transform.rotation, transform.localScale);

            Vector2 gizmoPosition = new Vector2(_boxCollider.size.x, _boxCollider.size.y);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
        }
    }
#endif
}
