using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicsCollider : MonoBehaviour
{
    [SerializeField] private bool _showGizmo = default;
    [SerializeField] private bool _ignoreCollision = default;
    [SerializeField] private Vector2 _offset = default;
    [SerializeField] private Vector2 _size = default;
    protected List<DemonicsCollider> _demonicsColliders = new List<DemonicsCollider>();


    public DemonicsPhysics _physics { get; set; }
    public Color GizmoColor { get; set; } = Color.green;
    public bool WasColliding { get; set; }
    public bool IgnoreCollision { get { return _ignoreCollision; } set { _ignoreCollision = value; } }
    public DemonicsVector2 Size { get { return new DemonicsVector2((DemonicsFloat)_size.x, (DemonicsFloat)_size.y); } set { _size = new Vector2((float)value.x, (float)value.y); } }
    public DemonicsVector2 Offset { get { return new DemonicsVector2((DemonicsFloat)_offset.x * (DemonicsFloat)transform.root.localScale.x, (DemonicsFloat)_offset.y); } set { _offset = new Vector2((float)value.x, (float)value.y); } }
    public DemonicsVector2 Position
    {
        get
        {
            if (_physics != null)
            {
                return new DemonicsVector2((_physics.Position.x + Offset.x), (_physics.Position.y + Offset.y));
            }
            else
            {
                return Offset;
            }
        }
        private set { }
    }


    private bool valueInRange(DemonicsFloat value, DemonicsFloat min, DemonicsFloat max)
    { return (value >= min) && (value <= max); }

    protected virtual bool Colliding(DemonicsCollider a, DemonicsCollider b)
    {
        bool xOverlap = valueInRange(a.Position.x - (a.Size.x / (DemonicsFloat)2), b.Position.x - (b.Size.x / (DemonicsFloat)2), b.Position.x + (b.Size.x / (DemonicsFloat)2)) ||
                    valueInRange(b.Position.x - (b.Size.x / (DemonicsFloat)2), a.Position.x - (a.Size.x / (DemonicsFloat)2), a.Position.x + (a.Size.x / (DemonicsFloat)2));
        bool yOverlap = valueInRange(a.Position.y - (a.Size.y / (DemonicsFloat)2), b.Position.y - (b.Size.y / (DemonicsFloat)2), b.Position.y + (b.Size.y / (DemonicsFloat)2)) ||
                    valueInRange(b.Position.y - (b.Size.y / (DemonicsFloat)2), a.Position.y - (a.Size.y / (DemonicsFloat)2), a.Position.y + (a.Size.y / (DemonicsFloat)2));
        return xOverlap && yOverlap;
    }

    protected virtual void Start()
    {
        _physics = transform.root.GetComponent<DemonicsPhysics>();
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
                        _physics.OnCollision(demonicsPhysics);
                    }
                }
                EnterCollision(_demonicsColliders[i]);
                return;
            }
        }
        if (WasColliding && !colliding)
        {
            ExitCollision();
        }
        _physics.OtherPhysics = null;
    }

    protected virtual void EnterCollision(DemonicsCollider collider)
    {
        if (!WasColliding)
        {
            WasColliding = true;
        }
    }

    protected virtual void ExitCollision()
    {
        WasColliding = false;
    }

    protected virtual void OnDisable()
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
