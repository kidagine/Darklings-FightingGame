using System.Collections;
using System.Collections.Generic;
using FixMath.NET;
using UnityEngine;

public class DemonicsCollider : MonoBehaviour
{
    [SerializeField] private bool _showGizmo = default;
    [SerializeField] private bool _ignoreCollision = default;
    [SerializeField] private Vector2 _offset = default;
    [SerializeField] private Vector2 _size = default;
    protected List<DemonicsCollider> _demonicsColliders = new List<DemonicsCollider>();
    private DemonicsPhysics _demonicsPhysics;


    public Color GizmoColor { get; set; } = Color.green;
    public bool WasColliding { get; set; }
    public bool IgnoreCollision { get { return _ignoreCollision; } set { _ignoreCollision = value; } }
    public FixVector2 Size { get { return new FixVector2((Fix64)_size.x, (Fix64)_size.y); } set { _size = new Vector2((float)value.x, (float)value.y); } }
    public FixVector2 Offset { get { return new FixVector2((Fix64)_offset.x * (Fix64)transform.root.localScale.x, (Fix64)_offset.y); } set { _offset = new Vector2((float)value.x, (float)value.y); } }
    public FixVector2 Position
    {
        get
        {
            if (_demonicsPhysics != null)
            {
                return new FixVector2((Fix64)(Fix64)_demonicsPhysics.Position.x + Offset.x, (Fix64)(Fix64)_demonicsPhysics.Position.y + Offset.y);
            }
            else
            {
                return Offset;
            }
        }
        private set { }
    }


    bool valueInRange(Fix64 value, Fix64 min, Fix64 max)
    { return (value >= min) && (value <= max); }

    public bool Colliding(DemonicsCollider a, DemonicsCollider b)
    {
        bool xOverlap = valueInRange(a.Position.x, b.Position.x, b.Position.x + b.Size.x) ||
                    valueInRange(b.Position.x, a.Position.x, a.Position.x + a.Size.x);

        bool yOverlap = valueInRange(a.Position.y, b.Position.y, b.Position.y + b.Size.y) ||
                    valueInRange(b.Position.y, a.Position.y, a.Position.y + a.Size.y);
        return xOverlap && yOverlap;
    }

    protected virtual void Start()
    {
        _demonicsPhysics = transform.root.GetComponent<DemonicsPhysics>();
        InitializeCollisionList();
    }

    protected virtual void InitializeCollisionList()
    {
        DemonicsCollider[] demonicsCollidersArray = FindObjectsOfType<DemonicsCollider>();
        for (int i = 0; i < demonicsCollidersArray.Length; i++)
        {
            if (!demonicsCollidersArray[i].transform.IsChildOf(transform.root))
            {
                _demonicsColliders.Add(demonicsCollidersArray[i]);
            }
        }
        _demonicsColliders.Remove(this);
    }

    void FixedUpdate()
    {
        bool colliding = false;
        for (int i = 0; i < _demonicsColliders.Count; i++)
        {
            if (Colliding(this, _demonicsColliders[i]))
            {
                colliding = true;
                if (!_ignoreCollision)
                {
                    if (_demonicsColliders[i].transform.root.TryGetComponent(out DemonicsPhysics demonicsPhysics))
                    {
                        _demonicsPhysics.OnCollision(demonicsPhysics);
                    }
                }
                if (!WasColliding)
                {
                    EnterCollision(_demonicsColliders[i]);
                }
                return;
            }
        }
        if (WasColliding && !colliding)
        {
            ExitCollision();
        }
    }

    protected virtual void EnterCollision(DemonicsCollider collider)
    {
        WasColliding = true;
    }

    protected virtual void ExitCollision()
    {
        WasColliding = false;
    }

    void OnDisable()
    {
        WasColliding = false;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (_showGizmo)
        {
            Color color = GizmoColor;
            color.a = 0.6f;
            Gizmos.color = color;
            Vector2 pushboxPosition = new Vector2((float)Position.x, (float)Position.y);
            Gizmos.matrix = Matrix4x4.TRS(pushboxPosition, transform.rotation, transform.localScale);

            Vector2 gizmoPosition = new Vector2((float)Size.x, (float)Size.y);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
            Gizmos.DrawWireCube(Vector3.zero, gizmoPosition);
        }
    }
#endif
}
