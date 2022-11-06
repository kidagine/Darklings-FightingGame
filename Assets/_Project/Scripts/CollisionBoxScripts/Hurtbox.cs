using FixMath.NET;
using UnityEngine;

public class Hurtbox : DemonicsCollider
{
    [SerializeField] private GameObject _hurtboxResponderObject = default;
    private IHurtboxResponder _hurtboxResponder;

    public Color HurtboxColor { get; private set; } = Color.green;


    void Awake()
    {
        if (_hurtboxResponderObject != null)
            _hurtboxResponder = _hurtboxResponderObject.GetComponent<IHurtboxResponder>();
    }

    public void SetBox(Vector2 size, Vector2 offset)
    {
        Size = new FixVector2((Fix64)size.x, (Fix64)size.y);
        Offset = new FixVector2((Fix64)offset.x, (Fix64)offset.y);
    }

    public bool TakeDamage(AttackSO attackSO)
    {
        return _hurtboxResponder.TakeDamage(attackSO);
    }
}
